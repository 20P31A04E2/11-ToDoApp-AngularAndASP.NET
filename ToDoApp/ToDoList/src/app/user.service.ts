import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  private userApiUrl = 'https://api2do.azurewebsites.net/api/Users';

  signUp(user: Users): Observable<any> {
    return this.http.post(`${this.userApiUrl}/Signup`, user);
  }

  signIn(user: Users): Observable<any> {
    return this.http.post(`${this.userApiUrl}/Signin`, user);
  }
}

export interface Users{
  userId?:number,
  userName:string,
  password:string
}
