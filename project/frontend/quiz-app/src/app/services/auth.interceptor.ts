import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const token = auth.token;
  
  console.log('Interceptor: token =', token);
  
  if (token && !auth.isTokenExpired()) {
    req = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
    console.log('Added Authorization header');
  }
  
  return next(req);
};