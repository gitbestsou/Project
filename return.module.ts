import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ReturnComponent } from './Returns/returns.component';
import { ReturnRoutingModule } from './return-routing.module';


/*
  
*
* Developer Name: Sourav Maji
  * Use Case - 1. Return a product from Orders which has been delivered
2. Cancel a product from Orders which has been placed but not deliverd
Creation Date - 10 / 10 / 2019
Last Modified Date - 12 / 10 / 2019

*/



@NgModule({
  declarations: [

    ReturnComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ReturnRoutingModule
  ],
  exports: [
    ReturnRoutingModule,
    ReturnComponent
  ]
})
export class ReturnModule { }

