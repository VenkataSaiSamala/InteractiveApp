import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';
import { map, ReplaySubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseurl = "https://localhost:5001/api/";
  private currentUserSource = new ReplaySubject<User>(1);

  constructor(private http: HttpClient) { }
  currentUser$ = this.currentUserSource.asObservable();

  login(model:any){
    
    return this.http.post(this.baseurl + 'account/login', model).pipe(
      map((response: User) => {
          const user = response;
          if(user){

            console.log(user);
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
          }
      })
    );
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  registerUser(model:any){
    return this.http.post(this.baseurl + 'account/Register', model).pipe(
      map((response: any) => {
          const user = response;
          console.log(response)
          if(user){
            console.log(user);
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
          }
      })
    );
  }
}
