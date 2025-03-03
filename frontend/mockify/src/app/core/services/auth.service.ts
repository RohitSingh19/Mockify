import { loadGapiInsideDOM, gapi } from 'gapi-script';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({  
    providedIn: 'root'
})
export class AuthService {
    
    constructor(private httpClient: HttpClient) {
        loadGapiInsideDOM().then(() => {
            gapi.load('auth2', () => {
              gapi.auth2.init({
                client_id: environment.googleClientId,
                cookiepolicy: 'single_host_origin',
              });
            });
          });
    }
    signInWithGoogle() {
        const auth2 = gapi.auth2.getAuthInstance();
        auth2.signIn().then((googleUser: any) => {
          const idToken = googleUser.getAuthResponse().id_token;
          // Send token to backend for verification
          console.log('Google ID Token:', idToken);
          this.sendGoogleTokenToBackend(idToken);
        });
    }

    sendGoogleTokenToBackend(token: string): void {
        this.httpClient.post(`${environment.apiUrl}auth/google`, { token }).subscribe((res: any) => {
            console.log('Backend response:', res);
        })
          
    }
}