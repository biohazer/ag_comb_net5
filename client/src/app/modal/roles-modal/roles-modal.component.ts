import { Component, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';  // need manual
import { User } from 'src/app/_models/user';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css']
})
export class RolesModalComponent implements OnInit {
  // title: string;
  // list: any[] = [];
  // closeBtnName: string;
  @Input() updateSelectedRoles = new EventEmitter();
  user: User;
  roles: any;

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  updateRoles() {
    this.updateSelectedRoles.emit(this.roles);  // here error
    this.bsModalRef.hide();
  }

}
