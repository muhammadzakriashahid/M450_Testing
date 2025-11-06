import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:7124/api/Auth';

  constructor(private http: HttpClient) {}

  login(username: string, password: string) {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, { username, password })
      .pipe(
        tap(res => {
          console.log('Login response:', res);
          localStorage.setItem('token', res.token);
          console.log('Token stored');
        })
      );
  }

  logout() {
    localStorage.removeItem('token');
  }

  get token() {
    const t = localStorage.getItem('token');
    console.log('Getting token:', t);
    return t;
  }

  isLoggedIn() {
    return !!this.token && !this.isTokenExpired();
  }

  isTokenExpired(): boolean {
    const token = this.token;
    if (!token) return true;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const exp = payload.exp;
      return Date.now() >= exp * 1000;
    } catch {
      return true;
    }
  }
}