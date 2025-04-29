import { loadGapiInsideDOM, gapi } from 'gapi-script';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { BehaviorSubject, Observable, of } from 'rxjs';

@Injectable({  
    providedIn: 'root'
})
export class AuthService {

    private loggedInUser = new BehaviorSubject<User>(null!);
    loggedInUser$ = this.loggedInUser.asObservable();
    
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
    
    setLoggedInUser(user: User) {
        this.loggedInUser.next(user);
    }

    sendGoogleTokenToBackend(): Observable<User> {
        return new Observable<User>(observer => {
            const auth2 = gapi.auth2.getAuthInstance();
            auth2.signIn().then((googleUser: any) => {
                const idToken = googleUser.getAuthResponse().id_token;          
                this.httpClient.post<User>(`${environment.apiUrl}auth/google`, { token: idToken }).subscribe(
                    user => {
                        observer.next(user);
                        observer.complete();
                        this.setLoggedInUser(user);
                        localStorage.setItem('user', JSON.stringify(user));
                    },
                    err => observer.error(err)
                );
            });
        });
    }

    saveUserInLocalStorage() {                
        localStorage.setItem('user', JSON.stringify(this.loggedInUser));
    }

    removeUserFromLocalStorage() {
        localStorage.removeItem('user');
    }

    getUserFromLocalStorage(): User | null {
        const user = localStorage.getItem('user');
        return user ? JSON.parse(user) : null;
    }

    logout() {
        const auth2 = gapi.auth2.getAuthInstance();
        auth2.signOut().then(() => {
            this.removeUserFromLocalStorage();
            this.setLoggedInUser(null!);
        });
    }
}