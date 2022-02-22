import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Interactive App';
  Users: any;

  /**
   *
   */
  constructor(private http: HttpClient) {}


  ngOnInit() {
    this.getUsers();
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/User').subscribe(response => {
      this.Users = response;
    }, error => {
      console.log();
    })
  }
}
