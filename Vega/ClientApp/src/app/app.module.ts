import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastyModule } from "ng2-toasty";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { AddVehicleComponent } from './components/add-vehicle/add-vehicle.component';
import { VehiclesService } from "./services/vehicle.service";
import { AppRoutingModule } from "./app-routing.module";
import { VehiclesListComponent } from "./components/vehicles-list/vehicles-list.component";
import { AppErrorHandler } from "./app.error-handler";
import { ViewVehicleComponent } from "./components/view-vehicle/view-vehicle.component";
import { ProgressService } from "./services/progress.service";
import { WjGridModule } from "wijmo/wijmo.angular2.grid";
import { WjInputModule } from "wijmo/wijmo.angular2.input";
import { WjGridFilterModule } from 'wijmo/wijmo.angular2.grid.filter';
import { WjGridSearchModule } from 'wijmo/wijmo.angular2.grid.search';
import { WjGridGrouppanelModule } from 'wijmo/wijmo.angular2.grid.grouppanel';
import { ProgressInterceptorService } from "./services/progress-interceptor.service";
import { AuthInterceptorService } from "./services/auth-interceptor.service";
import { AuthService } from "./services/auth.service";
import { UserProfileComponent } from "./components/user-profile/user-profile.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


const Http_Interceptors = [
  { provide: HTTP_INTERCEPTORS, useClass: ProgressInterceptorService, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true },
];


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddVehicleComponent,
    VehiclesListComponent,
    ViewVehicleComponent,
    UserProfileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    ToastyModule.forRoot(),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    WjGridModule,
    WjInputModule,
    WjGridFilterModule,
    WjGridSearchModule,
    WjGridGrouppanelModule,
    FontAwesomeModule
    ],
  providers: [
    VehiclesService,
    ProgressService,
    { provide: ErrorHandler, useClass: AppErrorHandler },
    Http_Interceptors,
    AuthService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
