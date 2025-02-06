import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCard} from '@angular/material/card';
import { HttpClient } from '@angular/common/http';
import { Category } from './core/models/category.model';



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule, MatGridListModule, MatCard],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  categories: Category[] = [];
  constructor(private httpClient: HttpClient) {
      this.httpClient.get<Category[]>('https://localhost:7048/api/v1/getCategories').subscribe((data) => {
          this.categories = data;
      });  
  }

  title = 'mockify';
}
