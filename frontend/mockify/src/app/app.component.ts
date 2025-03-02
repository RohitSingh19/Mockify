import { AfterViewInit, Component } from '@angular/core';
import {MatCard} from '@angular/material/card';
import { CategoryComponent } from "./shared/category/category.component";
import { PreviewComponent } from "./shared/preview/preview.component";
import { GoogleLoginProvider, SocialLoginModule, SocialAuthServiceConfig, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

declare const google: any;
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PreviewComponent, MatCard, CategoryComponent, PreviewComponent, SocialLoginModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [SocialAuthService, {
    provide: 'SocialAuthServiceConfig',    
    useValue: {
      autoLogin: false,
      lang: 'en',
      providers: [
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider('---') // Replace with your Client ID
        }
      ],
      onError: (err) => console.error(err)
    } as SocialAuthServiceConfig
  }]
})
export class AppComponent implements AfterViewInit {
  user: SocialUser | null = null;
  title = 'mockify';
  constructor(private socialAuthService: SocialAuthService, private httpClient: HttpClient) {
    this.socialAuthService.authState.subscribe((user) => {
      console.log('User:', user);
    });
  }


  ngAfterViewInit(): void {
    google.accounts.id.initialize({
      client_id: '----', // Replace with your Client ID
      callback: this.handleCredentialResponse.bind(this),
      auto_select: true, // Disable auto-sign-in for FedCM compatibility
      use_fedcm_for_prompt: false // Opt-in to FedCM (experimental)
    });

    google.accounts.id.renderButton(
      document.getElementById('google-signin-button'),
      {
        type: 'standard',
        size: 'large',
        text: 'signin_with',
        width: 300
      }
    );

    // Disable One Tap UI (optional, as it’s deprecated)
    //google.accounts.id.disableAutoSelect();
  }

  handleCredentialResponse(response: any): void {
    console.log('Credential response:', response);
    this.sendTokenToBackend(response.credential);
  }

  signOut(): void {
    this.socialAuthService.signOut();
    this.user = null;
    google.accounts.id.disableAutoSelect();
  }

  signInWithGoogle(): void {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  sendTokenToBackend(token: string): void {
    this.httpClient.post(`${environment.apiUrl}auth/google`, { token })
      .subscribe({
        next: (response) => console.log('Backend response:', response),
        error: (err) => console.error('Error:', err)
      });
  }
}
