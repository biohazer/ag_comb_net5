import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { NgxSpinnerModule } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;

  constructor(private spinnerService: NgxSpinnerModule) { }

  // busy() {
  //   this.busyRequestCount++; 
  //   this.spinnerService.show(undefined, {
  //     type: "line-scale-party",
  //     bdColor: "rgba(255, 255, 255, 0)",
  //     color: "#333333"
  //   });
  // }


}
