import { AfterViewInit, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { User } from './core/models/user.model';
import { AuthService } from './core/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
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
export class AppComponent{
  title = 'mockify';
  loggedInUser: User | undefined | null;

  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,    
    private router: Router
  ) {
    this.authService.loggedInUser$.subscribe((user) => {
      this.loggedInUser = user;
      console.log('Logged in user:', this.loggedInUser);
    });
  }
  ngOnInit() {
    this.loggedInUser = this.authService.getUserFromLocalStorage();
  }

  loginWithGoogle() {
    this.authService.sendGoogleTokenToBackend().subscribe({
      next: (user) => {
        if (user) {          
          this.loggedInUser = user;
          this.snackBar.open('Welcome, ' + this.loggedInUser.name, 'Dismiss', {
            duration: 2000,
          });
        }
      },
      error: (err) => console.error('Error:', err),
    });
  }

  checkLoggedInUser() {   
    this.loginWithGoogle();
  }

  updateUser() {
    this.authService.loggedInUser$.subscribe((user) => {
      this.loggedInUser = user;
      console.log('Logged in user:', this.loggedInUser);
    });
  }

  logout() {
    this.authService.logout();
    this.authService.removeUserFromLocalStorage();
    this.snackBar.open('Logged out successfully', 'Dismiss', {
      duration: 2000,
    });
    this.router.navigate(['/']); // Navigate to the home screen
  }
}
