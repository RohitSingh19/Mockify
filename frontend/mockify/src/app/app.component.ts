import { AfterViewInit, Component, OnInit } from '@angular/core';
import {MatCard} from '@angular/material/card';
import { CategoryComponent } from "./shared/category/category.component";
import { PreviewComponent } from "./shared/preview/preview.component";
import {MatIconModule} from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthService } from './core/services/auth.service';
import { MatMenuModule } from '@angular/material/menu';
import { User } from './core/models/user.model';
import { CommonModule } from '@angular/common';
import {MatSnackBar} from '@angular/material/snack-bar';
import { RouterModule } from '@angular/router';


declare const google: any;
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PreviewComponent, MatCard, CategoryComponent, PreviewComponent, MatIconModule, MatToolbarModule, MatMenuModule, CommonModule, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements AfterViewInit, OnInit {

  title = 'mockify';
  loggedInUser: User | undefined;
   
  constructor(private authService: AuthService, private snackBar: MatSnackBar) {
    
  }

  loginWithGoogle() {
    this.authService.sendGoogleTokenToBackend().subscribe({
      next: (user) => {
        if (user) {
          this.loggedInUser = user;
          this.snackBar.open("Welcome, "+this.loggedInUser.name, "Dismiss", {
            duration: 2000,
          });
        }
      },
      error: (err) => console.error('Error:', err)
    });
  }
  ngOnInit(): void {
  }


  ngAfterViewInit(): void {
  }
}
