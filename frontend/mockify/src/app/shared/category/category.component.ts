import { Component } from '@angular/core';
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


@Component({
  selector: 'app-category',
  standalone: true,
  imports: [MatMenuModule,MatButtonModule, MatTableModule, MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule, MatGridListModule, MatIconModule, MatTooltipModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {

  categories: Category[] = [];
  proerties:  Property[] = [];
  
  displayedColumns: string[] = ['No', 'Name', 'Type', 'Action'];
  

  constructor(private httpClient: HttpClient) {
      this.httpClient.get<Category[]>('https://localhost:7048/api/v1/getCategories').subscribe((data) => {
          this.categories = data;
      });  
  }
  

  onCategorySelect(event: any): void {
    const selectedCategory = event.value;
    this.proerties = this.categories.find(x => x.category === selectedCategory)?.properties || [];  
    const endpoint = this.categories.find(x => x.category === selectedCategory)?.endPoint || '';
    this.httpClient.get(`https://localhost:7048/api/v1/${endpoint}/10`).subscribe((data) => {
        console.log(data);
    });
  }
}
