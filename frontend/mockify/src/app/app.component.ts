import { AfterViewInit, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { User } from './core/models/user.model';
import { AuthService } from './core/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LocalStorageService } from './core/services/local-storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatIconModule,
    MatToolbarModule,
    MatMenuModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements AfterViewInit{
  title = 'mockify';
  loggedInUser: User | undefined;

  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private localStorage: LocalStorageService,
    private router: Router
  ) {}

  ngAfterViewInit(): void {
    
    // setTimeout(() => {
    //   this.checkLoggedInUser();
    // }, 3000); // Delay of 3 seconds
  }

  loginWithGoogle() {
    this.authService.sendGoogleTokenToBackend().subscribe({
      next: (user) => {
        if (user) {
          this.loggedInUser = user;
          this.snackBar.open('Welcome, ' + this.loggedInUser.name, 'Dismiss', {
            duration: 2000,
          });
          this.localStorage.removeItem('user');
          this.localStorage.setItem('user', JSON.stringify(this.loggedInUser));
        }
      },
      error: (err) => console.error('Error:', err),
    });
  }

  checkLoggedInUser() {   
    this.loginWithGoogle();
  }

  logout() {
    this.localStorage.removeItem('user');
    this.loggedInUser = undefined;
    this.snackBar.open('Logged out successfully', 'Dismiss', {
      duration: 2000,
    });

    this.router.navigate(['/']); // Navigate to the home screen
  }
}
