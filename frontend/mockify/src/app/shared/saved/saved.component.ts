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
    MatTooltipModule],
  templateUrl: './saved.component.html',
  styleUrl: './saved.component.css'
})
export class SavedComponent {
  displayedColumns: string[] = ['No', 'Name', 'Action'];
  templates: any[] = [];

  constructor(private templateServive: TemplateService) {
    this.getAllTemplates();
  }

  deleteTemplate(templateName: string) {

  }

  getAllTemplates() {
    this.templateServive.getTemplates().subscribe((data) => { 
      if(data.success && data.statusCode === 200) {
        this.templates = data.data;
        console.log(this.templates);
      } else {
        this.templates = [];
      }
    });
  }
}
