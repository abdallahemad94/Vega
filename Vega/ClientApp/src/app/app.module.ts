import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastyModule } from "ng2-toasty";
import { DataTablesModule } from 'angular-datatables';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { AddVehicleComponent } from './components/add-vehicle/add-vehicle.component';
import { VehiclesService } from "./services/vehicle.service";
import { AppRoutingModule } from "./app-routing.module";
import { VehiclesListComponent } from "./components/vehicles-list/vehicles-list.component";
import { AppErrorHandler } from "./app.error-handler";
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddVehicleComponent,
    VehiclesListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    ToastyModule.forRoot(),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    DataTablesModule,
    ],
  providers: [VehiclesService, { provide: ErrorHandler, useClass: AppErrorHandler }],
  bootstrap: [AppComponent]
})
export class AppModule { }
