import { loadGapiInsideDOM, gapi } from 'gapi-script';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { Observable, of } from 'rxjs';

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
    // signInWithGoogle() : Observable<User | null> {
    //     //const auth2 = gapi.auth2.getAuthInstance();
    //     // auth2.signIn().then((googleUser: any) => {
    //     //   const idToken = googleUser.getAuthResponse().id_token;
    //     //   return this.sendGoogleTokenToBackend(idToken);
    //     // });
    //     return of(null);
    // }


    
    sendGoogleTokenToBackend(): Observable<User> {
        return new Observable<User>(observer => {
            const auth2 = gapi.auth2.getAuthInstance();
            auth2.signIn().then((googleUser: any) => {
                const idToken = googleUser.getAuthResponse().id_token;          
                this.httpClient.post<User>(`${environment.apiUrl}auth/google`, { token: idToken }).subscribe(
                    user => {
                        observer.next(user);
                        observer.complete();
                    },
                    err => observer.error(err)
                );
            });
        });
    }
}