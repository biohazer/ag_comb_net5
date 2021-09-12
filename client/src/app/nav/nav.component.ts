import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastInjector, ToastrService } from 'ngx-toastr';
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

  constructor(
    public accountService:AccountService, 
    private router:Router, 
    private toastr:ToastrService
    ) { }

  ngOnInit(): void {
    // this.getCurrentUser();
    // this.currentUser$ = this.accountService.currentUser$  // 引入AC的依赖注入
  }

  login(){
    this.accountService.login(this.model).subscribe(res=>{
      console.log(res)
      // this.loggedIn = true
      this.router.navigateByUrl("/members")
    }
    // , error=>{
    //   console.log(error);
    //   this.toastr.error(error.error); // error contained in error property
    // }
    )
    console.log(this.accountService.currentUser$)
  }

  logout(){
    this.accountService.logout();
    // this.loggedIn = false
    console.log(this.accountService.currentUser$)
    this.router.navigateByUrl("/")
  }

  // getCurrentUser(){
  //   this.accountService.currentUser$.subscribe(user =>{
  //     this.loggedIn = !!user;  // !! turn obj to boolean. user null, boolean is false
  //   }, error => {
  //     console.log(error);
  //   })
  // }
}
