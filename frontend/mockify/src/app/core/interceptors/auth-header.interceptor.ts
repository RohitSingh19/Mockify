import { HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

export const authHeaderInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> => {
  if(req.url.includes('auth/google') || req.url.includes('api/v1/categories')) {
    return next(req);
  }

  const user = localStorage.getItem('user');
  if(!user) {
    throw new Error('User not found in local storage');
  }
  const token = user ? JSON.parse(user).token : '';
  const newReq = req.clone({
    setHeaders: {
      Authorization: token,
      'Content-Type': 'application/json'
    }
  })
  return next(newReq);
};
