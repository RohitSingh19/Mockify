import { Component } from '@angular/core';
import {MatCard} from '@angular/material/card';
import { CategoryComponent } from "../category/category.component";
import { PreviewComponent } from "../preview/preview.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatCard, CategoryComponent, PreviewComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
