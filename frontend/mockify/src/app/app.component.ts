import { Component } from '@angular/core';
import {MatCard} from '@angular/material/card';
import { CategoryComponent } from "./shared/category/category.component";
import { PreviewComponent } from "./shared/preview/preview.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PreviewComponent, MatCard, CategoryComponent, PreviewComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'mockify';
}
