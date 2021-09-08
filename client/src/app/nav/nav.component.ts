import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}
  // loggedIn: boolean;
  // currentUser$: Observable<User>;

  constructor(public accountService:AccountService) { }

  ngOnInit(): void {
    // this.getCurrentUser();
    // this.currentUser$ = this.accountService.currentUser$  // 引入AC的依赖注入
  }

  login(){
    this.accountService.login(this.model).subscribe(res=>{
      console.log(res)
      // this.loggedIn = true
    }, error=>{
      console.log(error)
    })
    console.log(this.accountService.currentUser$)
  }

  logout(){
    this.accountService.logout();
    // this.loggedIn = false
    console.log(this.accountService.currentUser$)
  }

  // getCurrentUser(){
  //   this.accountService.currentUser$.subscribe(user =>{
  //     this.loggedIn = !!user;  // !! turn obj to boolean. user null, boolean is false
  //   }, error => {
  //     console.log(error);
  //   })
  // }
}
