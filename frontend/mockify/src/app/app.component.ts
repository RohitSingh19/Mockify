import { Component } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCard} from '@angular/material/card';
import { CategoryComponent } from "./shared/category/category.component";
import { PreviewComponent } from "./shared/preview/preview.component";



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PreviewComponent, MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule, MatGridListModule, MatCard, CategoryComponent, PreviewComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'mockify';
}
