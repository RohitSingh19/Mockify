import { AfterViewInit, Component, OnInit } from '@angular/core';
import {MatCard} from '@angular/material/card';
import { CategoryComponent } from "./shared/category/category.component";
import { PreviewComponent } from "./shared/preview/preview.component";
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import {MatIconModule} from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthService } from './core/services/auth.service';
import { MatMenuModule } from '@angular/material/menu';


declare const google: any;
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PreviewComponent, MatCard, CategoryComponent, PreviewComponent, MatIconModule, MatToolbarModule, MatMenuModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements AfterViewInit, OnInit {

  title = 'mockify';
 

   
  constructor(private httpClient: HttpClient, private authService: AuthService) {
    
  }

  loginWithGoogle() {
    this.authService.signInWithGoogle();
  }
  ngOnInit(): void {
  }


  ngAfterViewInit(): void {
  }

  handleCredentialResponse(response: any): void {
    console.log('Credential response:', response);
    this.sendTokenToBackend(response.credential);
  }

  sendTokenToBackend(token: string): void {
    this.httpClient.post(`${environment.apiUrl}auth/google`, { token })
      .subscribe({
        next: (response) => console.log('Backend response:', response),
        error: (err) => console.error('Error:', err)
      });
  }
}
