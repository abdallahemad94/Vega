import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from "./components/home/home.component";
import { AddVehicleComponent } from "./components/add-vehicle/add-vehicle.component";

const routs = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'add-vehicle', component: AddVehicleComponent },
]

@NgModule({
  imports: [
    RouterModule.forRoot(routs),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
