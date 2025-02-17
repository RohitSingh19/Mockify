import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent } from '@angular/material/dialog';
import { MockDataService } from '../../core/services/mock-data.service';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { Category, CustomMockDataRequest, Property } from '../../core/models/category.model';
import {MatTableModule} from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input'
import { NgxJsonViewerModule } from 'ngx-json-viewer';



@Component({
  selector: 'app-custom-category',
  standalone: true,
  imports: [NgxJsonViewerModule, MatInputModule,CommonModule, MatDialogContent,MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule, MatTableModule],
  templateUrl: './custom-category.component.html',
  styleUrl: './custom-category.component.css'
})
export class CustomCategoryComponent implements OnInit {

  customCategory!: Category;
  properties:  Property[] = [];
  customCategoryForm = new FormControl('');
  selectedProperties: Property[] = [];
  displayedColumns: string[] = ['Name', 'Type', 'CustomDataValue'];
  customMockDataRequest: CustomMockDataRequest = { items: [] };
  constructor(public dialogRef: MatDialogRef<CustomCategoryComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
             private mockDataService: MockDataService, private cdr: ChangeDetectorRef) {
    
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
          description: property.description,
          CustomDataValue: ''
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

  generateCustomMockData() {
    this.properties.forEach((property: Property) => {
      if(property.isVisible) {
        this.customMockDataRequest.items.push({ filedName: property.name, customValue: property.CustomDataValue.length > 0 ? property.CustomDataValue : null })
      };
    });
    console.log(this.customMockDataRequest);
  }
}





