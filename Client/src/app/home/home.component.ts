import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  Users: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.registerMode = true;
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/User').subscribe(response => {
      this.Users = response;
      console.log(this.Users);
    }, error => {
      console.log();
    })
  }

  cancelFunctionality(event){
    this.registerMode = event;
  }
}
