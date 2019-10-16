import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { environment } from '../environments/environment.prod';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { ReturnModule } from './OrderModule/return.module';
import { ReturnComponent } from './OrderModule/Returns/returns.component';
import { GreatOutdoorsDataService } from './InMemoryWebAPIService/greatoutdoors1-data.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    environment.production ? HttpClientInMemoryWebApiModule.forRoot(GreatOutdoorsDataService, { delay: 1000 }) : [],
    ReturnModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
