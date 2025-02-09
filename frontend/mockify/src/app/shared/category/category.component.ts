import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Category, Property } from '../../core/models/category.model';
import { HttpClient } from '@angular/common/http';
import {MatIconModule} from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTableModule} from '@angular/material/table';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import { MockDataService } from '../../core/services/mock-data.service';


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

  jsonString : any
  
  displayedColumns: string[] = ['No', 'Name', 'Type', 'Action'];
  categoryIcons: IHash = {};
  

  constructor(private httpClient: HttpClient, private mockDataService: MockDataService) {
      this.initializeIcons();
  }
  ngOnInit(): void {
    this.httpClient.get<Category[]>('https://localhost:7048/api/v1/getCategories').subscribe((data) => {           
      data.map((category) => {
          category.icon = this.categoryIcons[category.category];
       });
      this.categories = data;
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

  }

  onCategorySelect(event: any): void {
    const selectedCategory = event.value;
    this.proerties = this.categories.find(x => x.category === selectedCategory)?.properties || [];  
    const endpoint = this.categories.find(x => x.category === selectedCategory)?.endPoint || '';
    this.httpClient.get(`https://localhost:7048/api/v1/${endpoint}/10`).subscribe((data) => {
        this.sendJsonForPreview(data);
    });
  }

  sendJsonForPreview(json: any): void {
    this.mockDataService.setData(json);
  }
}
