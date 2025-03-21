import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { User } from '../../core/models/user.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../core/services/auth.service';
import { CommonModule } from '@angular/common';
import { LocalStorageService } from '../../core/services/local-storage.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatIconModule, MatToolbarModule, MatMenuModule,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
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
