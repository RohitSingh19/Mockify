import { HttpEvent, HttpEventType, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';

export const rateLimitInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError(error => {
      if (error.status === 429) {
        alert('You are sending requests too quickly. Please wait and try again.');
        // Optionally, you can implement retry logic here
      }
      throw error;
    })
  );
};
