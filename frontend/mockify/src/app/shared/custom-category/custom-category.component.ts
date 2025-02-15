import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent } from '@angular/material/dialog';
import { MockDataService } from '../../core/services/mock-data.service';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MatRadioButton } from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { Category, Property } from '../../core/models/category.model';
import {MatTableModule} from '@angular/material/table';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-custom-category',
  standalone: true,
  imports: [CommonModule,MatRadioButton, MatDialogContent,MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule, MatTableModule],
  templateUrl: './custom-category.component.html',
  styleUrl: './custom-category.component.css'
})
export class CustomCategoryComponent implements OnInit {

  customCategory!: Category;
  properties:  Property[] = [];
  customCategoryForm = new FormControl('');
  selectedProperties: Property[] = [];
  displayedColumns: string[] = ['Name', 'Type', 'RandomData', 'RandomDataValue'];
  constructor(public dialogRef: MatDialogRef<CustomCategoryComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
             private mockDataService: MockDataService) {
    
  }


  ngOnInit(): void {
    this.mockDataService.getCustomCategory().subscribe((data: any) => {
      this.customCategory = data;
      this.customCategory.properties.forEach((property: Property) => {  
        this.properties.push({
          label: property.label,
          name: property.name,
          type: property.type,
          isVisible: false,
          isRandomData: false,
          description: property.description
        })

      });
    });
  }

  
  closeDialog(): void {
    this.dialogRef.close();
  }

  onCategorySelect(event: any) {
    const selectedProperties = [...event.value];
    this.properties.map((property: Property) => {property.isVisible = false;});
    selectedProperties.forEach((propertyName: string) => {
      this.markPropertyAsVisible(propertyName);
     });
  }

  markPropertyAsVisible(propertyName: string) {
    for (let property of this.properties) {
      if (property.name === propertyName) {
         property.isVisible = !property.isVisible;
         break;
      }
    }
  }
}






