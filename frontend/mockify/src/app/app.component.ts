import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from "./shared/header/header.component";
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { User } from './core/models/user.model';
import { AuthService } from './core/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LocalStorageService } from './core/services/local-storage.service';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule, MatIconModule, MatToolbarModule, MatMenuModule],  
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent{
  title = 'mockify'; 
  loggedInUser: User | undefined;
     
    constructor(private authService: AuthService, private snackBar: MatSnackBar, private localStorage: LocalStorageService) {
      this.checkLoggedInUser();
    }
  
    loginWithGoogle() {
      this.authService.sendGoogleTokenToBackend().subscribe({
        next: (user) => {
          if (user) {
            this.loggedInUser = user;
            this.snackBar.open("Welcome, "+this.loggedInUser.name, "Dismiss", {
              duration: 2000,
            });
            this.localStorage.removeItem('user');
            this.localStorage.setItem('user', JSON.stringify(this.loggedInUser));
          }
        },
        error: (err) => console.error('Error:', err)
      });
    }
  
    
  
    checkLoggedInUser() {
      const user = this.localStorage.getItem('user');
      if(!user) {
        return;
      }
      this.loggedInUser = JSON.parse(user);
    }
}
