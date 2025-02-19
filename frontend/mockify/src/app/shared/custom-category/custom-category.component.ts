import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent } from '@angular/material/dialog';
import { MockDataService } from '../../core/services/mock-data.service';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { Category, CustomMockDataRequest, JsonEditorModel, Property } from '../../core/models/category.model';
import {MatTableModule} from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input'
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import {
  NuMonacoEditorComponent,  
  NuMonacoEditorEvent,
  NuMonacoEditorModel,
} from '@ng-util/monaco-editor';


@Component({
  selector: 'app-custom-category',
  standalone: true,
  imports: [NuMonacoEditorComponent,NgxJsonViewerModule, MatInputModule,CommonModule, MatDialogContent,MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule, MatTableModule],
  templateUrl: './custom-category.component.html',
  styleUrl: './custom-category.component.css'
})
export class CustomCategoryComponent implements OnInit {
  customCategory!: Category;
  properties:  Property[] = [];
  customCategoryForm = new FormControl('');
  selectedProperties: Property[] = [];
  customMockDataRequest: CustomMockDataRequest = { items: [] };
  jsonData: string = '';
  jsonEditorModel: JsonEditorModel[] = [];
  editorOptions: any;
  
  constructor(public dialogRef: MatDialogRef<CustomCategoryComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
             private mockDataService: MockDataService, private cdr: ChangeDetectorRef) 
             {
    
  }


  ngOnInit(): void {
    this.mockDataService.getCustomCategory().subscribe((data: any) => {
      this.customCategory = data;
      this.customCategory.properties.forEach((property: Property) => {  
        this.properties.push({
          label: property.label,
          name: property.name,
          type: property.type,
          description: property.description,
          value: property.value,
          isVisible: false,
        })
      });
    });
    this.intializeJsonEditor();
  }

  intializeJsonEditor() {
    this.jsonData = JSON.stringify({}, null, 2);
  
    this.editorOptions = {
      theme: 'hc-light',
      language: 'json', 
      automaticLayout: true,
    };
  }
  closeDialog(): void {
    this.dialogRef.close();
  }

  getSelectedProperties(propertyNames: string[]) {
      return this.properties.filter((property: Property) => propertyNames.includes(property.name));
  }

  refreshSelectedProperties() {

  }

  onCategorySelect(event: any) {
    const selectedPropertiesName = [...event.value];

    this.properties.forEach((property: Property) => {
      property.isVisible = selectedPropertiesName.includes(property.name);
    });
    this.showVisiblePropertiesInJsonEditor(this.properties);
  }
  onChange(event: any) {
    this.jsonEditorModel = JSON.parse(event);
  }
  showVisiblePropertiesInJsonEditor(properties: Property[]) {
    properties.forEach((property: Property) => 
    {
      if(property.isVisible && !this.jsonEditorModel.find((model: JsonEditorModel) => model.name === property.name)){
        this.jsonEditorModel.push({
          name: property.name,
          type: property.type,
          value: property.value
        });
      } else if(!property.isVisible && this.jsonEditorModel.find((model: JsonEditorModel) => model.name === property.name)) 
        {
        const existingPropertyFromJsonEditorModel = this.jsonEditorModel.find((model: JsonEditorModel) => model.name === property.name);
        //remove the existing json node from model
        if (existingPropertyFromJsonEditorModel) {
          this.jsonEditorModel = this.jsonEditorModel.filter((model: JsonEditorModel) => model.name !== existingPropertyFromJsonEditorModel.name);
         }
        }
    });
    this.jsonData = JSON.stringify(this.jsonEditorModel, null, 2);
  }

  generateCustomMockData() {
  }

  model: NgxJsonViewerModule = {

  }
}





