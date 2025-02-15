import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7244/api/Auth/login'; 
  private http = inject(HttpClient);

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(this.apiUrl, { email, password });
  }

  logout(): void {
    localStorage.removeItem('token'); 
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  register(userData: any): Observable<any> {
    return this.http.post<any>('https://localhost:7244/api/User', userData);
  }
    
}
