import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Toast, ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelFunction = new EventEmitter()
  model: any = {};
  

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register(){
    this.accountService.registerUser(this.model).subscribe(response => {
      console.log(response);
      this.toastr.success("Hurrey!!!");
      this.cancel();  
    }, error => {
      this.toastr.error(error.error);
    });
    
  }

  cancel(){
    this.cancelFunction.emit(false);
  }

}
