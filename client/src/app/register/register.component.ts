import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // @Input() usersFromHomeComponent: any;

  @Output() cancelRegister = new EventEmitter();

  model:any = {}

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register(){
    // alert(this.usersFromHomeComponent[0].userName)
    this.accountService.register(this.model).subscribe(response => {
      console.log(response);
      this.cancel();
      // here will do routing operator
    }, error=>{
      console.error(error);
    });
    
  }

  cancel(){
    console.log("cancel ed !!")
    this.cancelRegister.emit(false)
  }
}
