import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReturnComponent } from './Returns/returns.component';


/*
  
*
* Developer Name: Sourav Maji
  * Use Case - 1. Return a product from Orders which has been delivered
2. Cancel a product from Orders which has been placed but not deliverd
Creation Date - 10 / 10 / 2019
Last Modified Date - 12 / 10 / 2019

*/



const routes: Routes = [

  { path: "returns", component: ReturnComponent },
  { path: "**", redirectTo: 'returns', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReturnRoutingModule {
}
