import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Category, Property } from '../../core/models/category.model';
import {MatIconModule} from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTableModule} from '@angular/material/table';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import { MockDataService } from '../../core/services/mock-data.service';
import { MatDialog } from '@angular/material/dialog';
import { CustomCategoryComponent } from '../custom-category/custom-category.component';


interface IHash {
  [key: string]: string;
}

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [MatMenuModule,MatButtonModule, MatTableModule, MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule, MatGridListModule, MatIconModule, MatTooltipModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent implements OnInit {

  categories: Category[] = [];
  proerties:  Property[] = [];
  originalJsonString: any;
  currentJsonString : any  
  displayedColumns: string[] = ['No', 'Name', 'Type', 'Action'];
  categoryIcons: IHash = {};
  isCategoryVisible = true;
  categoryVisibleIcon = 'visibility';
  categoryInvisibleIcon = 'visibility_off';

  constructor(private mockDataService: MockDataService, private dialog: MatDialog) {
      this.initializeIcons();
  }
  ngOnInit(): void {
    this.mockDataService.getCategories().subscribe((data) => {           
      data.map((category) => {
          category.icon = this.categoryIcons[category.category];
       });
      this.categories = data;
      this.pushCustomCategory();
  });
  }

  pushCustomCategory() {
    this.categories.push({
      category: 'Custom',
      properties: [],
      icon: this.categoryIcons['Custom'],
      endpointToGetMockData: ''
    });
  }

  onCategorySelect(event: any): void {
    const selectedCategory = event.value;
    if(selectedCategory === 'Custom') { 
      this.openDialog();
      return;
    }
    this.proerties = this.categories.find(x => x.category === selectedCategory)?.properties || [];  

    this.proerties.map((property) => {
      property.isVisible = true;
    });

    const endpoint = this.categories.find(x => x.category === selectedCategory)?.endpointToGetMockData || '';
    this.getMockDataForSelectedCategory(endpoint);
  }

  getMockDataForSelectedCategory(categoryAPIEndpoint: string): void {   
    this.mockDataService.getMockDataForSelectedCategory(categoryAPIEndpoint).subscribe((data: any) => {    
        this.originalJsonString = JSON.stringify(data, null, 2);
        this.sendJsonForPreview(data);
    });
  }

  sendJsonForPreview(json: any): void {
    this.mockDataService.setData(json);
  }

  openDialog(): void {
    this.dialog.open(CustomCategoryComponent, {
      width: '90vw',
      maxWidth: '90vw',
      height: '600px'
    });
  }

  initializeIcons(): void {
    this.categoryIcons['Internet'] = 'wifi';
    this.categoryIcons['Location'] = 'pin_drop';
    this.categoryIcons['Lorem'] = 'description';
    this.categoryIcons['Notification'] = 'notifications';
    this.categoryIcons['Payment'] = 'receipt_long';
    this.categoryIcons['User'] = 'person';
    this.categoryIcons['Vehicle'] = 'directions_car';
    this.categoryIcons['File System'] = 'save';
    this.categoryIcons['Randomizer'] = 'shuffle';
    this.categoryIcons['Custom'] = 'settings';
  }

  togglePropertyVisibility(property: any): void {
      property.isVisible = !property.isVisible;      
      this.togglePropertyFromJSONPreview();
  }

  togglePropertyFromJSONPreview(): void {
    const prpertiesToHide = this.proerties.filter((property) => !property.isVisible);
    const json = JSON.parse(this.originalJsonString);
    const updatedJson = json.map((obj: any) => {
      const newObj = { ...obj };
      prpertiesToHide.forEach((prop) => {
          delete newObj[prop.name];
      });
      return newObj;
   });

    this.sendJsonForPreview(updatedJson);
  }
}
