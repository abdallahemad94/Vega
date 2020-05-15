import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from "./components/home/home.component";
import { AddVehicleComponent } from "./components/add-vehicle/add-vehicle.component";
import { VehiclesListComponent } from "./components/vehicles-list/vehicles-list.component";
import { ViewVehicleComponent } from "./components/view-vehicle/view-vehicle.component";

const routes = [
  { path: '', redirectTo: "vehicles", pathMatch: 'full' },
  {
    path: 'vehicles', component: HomeComponent,
    children: [
      { path: "", component: VehiclesListComponent },
      { path: "new", component: AddVehicleComponent },
      { path: "view/:id", component: ViewVehicleComponent },
      { path: "edit/:id", component: AddVehicleComponent }
    ]
  },
  { path: '**', redirectTo: "vehicles", pathMatch: 'full' },
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
