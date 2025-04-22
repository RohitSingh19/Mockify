import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { TemplateService } from '../../core/services/template-service';
import { Template } from '../../core/models/template-model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { CustomCategoryComponent } from '../custom-category/custom-category.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-saved',
  standalone: true,
  imports: [MatMenuModule,MatButtonModule, 
    MatTableModule, 
    MatFormFieldModule, 
    MatSelectModule,
    MatInputModule,
    MatGridListModule,
    MatIconModule,
    MatTooltipModule,
    CommonModule],
  templateUrl: './saved.component.html',
  styleUrl: './saved.component.css'
})
export class SavedComponent {
  displayedColumns: string[] = ['No', 'Name', 'Action 1', 'Action 2'];
  templates: any[] = [];
  isLoading: boolean = false; // Loading state variable
  isCategoryVisible = true;

  constructor(private templateService: TemplateService, private dialog: MatDialog, private snackBar: MatSnackBar) {
    this.getAllTemplates();
  }

  deleteTemplate(template: Template) {
    alert(template.name);
    this.templateService.deleteTemplate(template.name).subscribe((data) => {
      if(data.success && data.statusCode === 200) {
        this.getAllTemplates();
        this.snackBar.open(`${template.name} deleted`, "Dismiss", {
          duration: 2000,
        });
      } else {
        console.log('Error in deleting template');
      }
    });
  }

  getAllTemplates() {
    this.isLoading = true; // Set loading to true before API call
    this.templateService.getTemplates().subscribe((data) => {
      this.isLoading = false; // Set loading to false after API call
      if (data.success && data.statusCode === 200) {
        this.templates = data.data;
        console.log(this.templates);
      } else {
        this.templates = [];
      }
    }, (error) => {
      this.isLoading = false; // Handle error and stop loading
      console.error('Error fetching templates:', error);
    });
  }

  editTemplate(template: Template) {    
    this.dialog.open(CustomCategoryComponent, {
          width: '90vw',
          maxWidth: '90vw',
          height: '600px',
          data: template
        });
  }
}
