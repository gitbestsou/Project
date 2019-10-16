import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReturnComponent } from './OrderModule/Returns/returns.component';

const routes: Routes = [

  { path: "returns", component: ReturnComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
