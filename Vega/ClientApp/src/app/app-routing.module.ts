import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from "./components/home/home.component";
import { AddVehicleComponent } from "./components/add-vehicle/add-vehicle.component";
import { VehiclesListComponent } from "./components/vehicles-list/vehicles-list.component";
import { ViewVehicleComponent } from "./components/view-vehicle/view-vehicle.component";
import { UserProfileComponent } from "./components/user-profile/user-profile.component";
import { AuthGuardService } from "./services/auth-guard.service";

const routes: Routes = [
  { path: '', redirectTo: "vehicles", pathMatch: 'full' },
  {
    path: 'vehicles', component: HomeComponent,
    children: [
      { path: "", component: VehiclesListComponent },
      { path: "new", component: AddVehicleComponent, canActivate: [AuthGuardService] },
      { path: "view/:id", component: ViewVehicleComponent },
      { path: "edit/:id", component: AddVehicleComponent, canActivate: [AuthGuardService] }
    ]
  },
  { path: 'profile', component: UserProfileComponent, canActivate: [AuthGuardService]},
  { path: '**', redirectTo: "vehicles", pathMatch: 'full' },
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
