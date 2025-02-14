import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogActions, MatDialogContent } from '@angular/material/dialog';
import { MockDataService } from '../../core/services/mock-data.service';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { Category, Property } from '../../core/models/category.model';

@Component({
  selector: 'app-custom-category',
  standalone: true,
  imports: [MatDialogActions, MatDialogContent,MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule],
  templateUrl: './custom-category.component.html',
  styleUrl: './custom-category.component.css'
})
export class CustomCategoryComponent implements OnInit {

  customCategory!: Category;
  proerties:  Property[] = [];
  customCategoryForm = new FormControl('');


  constructor(public dialogRef: MatDialogRef<CustomCategoryComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private mockDataService: MockDataService) {
    
  }


  ngOnInit(): void {
    this.mockDataService.getCustomCategory().subscribe((data: any) => {
      this.customCategory = data;
      this.proerties = this.customCategory.properties;
    });
  }

  
  closeDialog(): void {
    this.dialogRef.close();
  }
}






