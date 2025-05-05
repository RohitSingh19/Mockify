import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent } from '@angular/material/dialog';
import { MockDataService } from '../../core/services/mock-data.service';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { Category, CustomMockDataRequest, JsonEditorModel, Property } from '../../core/models/category.model';
import {MatTableModule} from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input'
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { MatCardModule } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';


import {
  NuMonacoEditorComponent,  
} from '@ng-util/monaco-editor';
import { ClipboardModule, Clipboard } from '@angular/cdk/clipboard';
import {MatIconModule} from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import {MatButtonModule} from '@angular/material/button';
import { ApiResponse } from '../../core/models/api-response.model';
import { TemplateService } from '../../core/services/template-service';
import { Observable } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';



@Component({
  selector: 'app-custom-category',
  standalone: true,
  imports: [MatButtonModule, ClipboardModule,MatIconModule, NuMonacoEditorComponent, 
    NgxJsonViewerModule, MatInputModule,CommonModule, MatDialogContent,MatFormFieldModule,
     MatSelectModule, FormsModule, ReactiveFormsModule, MatTableModule, MatCardModule, MatTooltipModule],
  templateUrl: './custom-category.component.html',
  styleUrl: './custom-category.component.css'
})
export class CustomCategoryComponent implements OnInit {
  customCategory!: Category;
  properties:  Property[] = [];
  customCategoryForm = new FormControl<string[]>([]);
  selectedProperties: Property[] = [];
  customMockDataRequest: CustomMockDataRequest = { items: [] };
  jsonData: string = '';
  jsonEditorModel: JsonEditorModel[] = [];
  editorOptions: any;
  customJsonResponse: any;
  isJsonValid = false;
  showTooltip = false;
  isUpdateTemplate = false;
  oldTemplateName: string = '';
  form: FormGroup;

  constructor(public dialogRef: MatDialogRef<CustomCategoryComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
             private mockDataService: MockDataService, private authService: AuthService,
             private clipboard: Clipboard, private snackBar: MatSnackBar, private templateService: TemplateService) 
             {

              this.form = new FormGroup({
                templateName: new FormControl('', [
                  Validators.required,
                  Validators.minLength(3),
                  Validators.maxLength(30),
                  Validators.pattern('^[a-zA-Z]+$')
                ]),
              });
             }


  ngOnInit(): void {
    this.mockDataService.getCustomCategory().subscribe((data: ApiResponse<any>) => {
      if(data.success && data.statusCode === 200) { 

      this.customCategory = data.data;
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
        this.intializeJsonEditor();
        if(this.data) {
          this.jsonData = this.data.content;
          this.form.value['templateName'] = this.data.name;
          this.oldTemplateName = this.data.name;
          this.isUpdateTemplate = true;
        }
      }

    });
    
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

  onCategorySelect(event: any) {
    const selectedPropertiesName = [...event.value];

    this.properties.forEach((property: Property) => {
      property.isVisible = selectedPropertiesName.includes(property.name);
    });
    this.showVisiblePropertiesInJsonEditor(this.properties);
  }

  onChange(event: any) {
    try
    {
      this.jsonEditorModel = JSON.parse(event);
      this.customCategoryForm.setValue(this.jsonEditorModel.map((model: JsonEditorModel) => model.name));
      this.isJsonValid = true;
    }
    catch (error) {
      this.snackBar.open("Invalid JSON", "Dismiss", {
        duration: 4000,
      });
      this.isJsonValid = false;
    }
    
  }

  showVisiblePropertiesInJsonEditor(properties: Property[]) {
    properties.forEach((property: Property) => 
    {
      if(property.isVisible && !this.jsonEditorModel.find((model: JsonEditorModel) => model.name === property.name)){
        this.jsonEditorModel.push({
          name: property.name,
          value: ''
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

  generateMockData() {
    this.customMockDataRequest.items = this.jsonEditorModel.map((model: JsonEditorModel) => {
      return {
        fieldName: model.name,
        customValue: model.value,
      };
    });
    this.mockDataService.generateMockDataForCustomJson(this.customMockDataRequest).subscribe((data: ApiResponse<any>) => {
      if(data.statusCode == 201 && data.success) {
        const jsonResponse = data.data;
        this.customJsonResponse = JSON.parse(jsonResponse);
        this.snackBar.open("JSON generated, you can either copy or download", "Dismiss", {
          duration: 2000,
        });
      }
    });
  }

  copyToClipboard() {
    this.clipboard.copy(JSON.stringify(this. customJsonResponse));
    this.snackBar.open("JSON copied to clipboard", "Dismiss", {
      duration: 2000,
    });
  }

  downloadJson() {
    const jsonString = JSON.stringify(this. customJsonResponse, null, 2);
    const blob = new Blob([jsonString], { type: "application/json" });
    const url = URL.createObjectURL(blob);
    
    const a = document.createElement("a");
    a.href = url;
    a.download = "mockify-mock-data.json";
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  }

  saveTemplate() {    
      //show error message
      if (this.form.invalid) {
        this.snackBar.open("Invalid template name. Ensure it is 3-30 characters long and contains only alphabets.", "Dismiss", {
          duration: 4000,
        });
        return;
      }
    

    /*
     This entire logic can be improved by checking if the user is present in local storage then checking the token expiration 
     and then calling the API to save the template. If the token is expired then we can call the login API again and then save the template.
     But for now we are just checking if the user is present in local storage and then calling the API to save the template.
    */
    const user = localStorage.getItem('user');
    if(!user) {
      this.snackBar.open("Login to save the template", "Dismiss", {
        duration: 2000,
      });
      return;
    }
    const templateName = this.form.value['templateName'];
    const action: Observable<any> = this.isUpdateTemplate ?
                                    this.templateService.updateTemplate(templateName, this.jsonEditorModel, this.oldTemplateName) :
                                    this.templateService.saveTemplate(templateName, this.jsonEditorModel);
    
    action.subscribe((data) => {
      if(data.success && data.statusCode === 200) {
        this.snackBar.open(`${templateName} template ${this.isUpdateTemplate ? 'updated' : 'saved'}`, "Dismiss", {
          duration: 2000,
        });
      } else {
        this.snackBar.open('Error in saving template', "Dismiss", {
          duration: 2000,
        });
      }
    }, error  => {
      if(error && error.error && error.error.Message) {
        this.snackBar.open(error.error.Message, "Dismiss", {
          duration: 2000,
        });
      }
    });
  }
}





