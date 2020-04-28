import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from "./components/home/home.component";
import { AddVehicleComponent } from "./components/add-vehicle/add-vehicle.component";
import { VehiclesListComponent } from "./components/vehicles-list/vehicles-list.component";

const routes = [
  { path: '', redirectTo: "vehicles", pathMatch: 'full' },
  {
    path: 'vehicles', component: HomeComponent,
    children: [
      { path: "", component: VehiclesListComponent },
      { path: "new", component: AddVehicleComponent },
      { path: ":id", component: AddVehicleComponent }
    ]
  },
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
