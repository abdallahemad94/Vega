import { Component, OnDestroy, OnInit } from '@angular/core';
import { VehicalsService } from '../../services/vehicle.service';
import { Make } from '../../models/Make';
import { Vehicle } from '../../models/Vehicle';
import { NgForm } from "@angular/forms";
import { Feature } from "../../models/Feature";
import { Subscription } from "rxjs";

@Component({
  selector: 'add-vehicle',
  templateUrl: './add-vehicle.component.html',
  styleUrls: ['./add-vehicle.component.css']
})
export class AddVehicleComponent implements OnInit, OnDestroy {
  public makes: Make[];
  public vehicle: Vehicle;
  public features: Feature[] = [];
  private subscribtion: Subscription;

  constructor(private vehicalsService: VehicalsService) { }

  ngOnInit() {
    this.subscribtion = this.vehicalsService.getMakes().subscribe(
      (result: Make[]) => this.makes = result,
      error => console.log(error)
    );
    this.vehicle = new Vehicle(); console.log(this.makes);
    this.createFeatures();
  }

  ngOnDestroy() {
    this.subscribtion.unsubscribe();
  }

  createFeatures() {
    for (let i of [0, 1, 2, 3, 4, 5, 6])
      this.features.push({ id: i, name: "feature" + i, selected: i % 2 == 0 });
  }

  OnSubmit(form: NgForm): void {
    console.log(form.controls['make'].valid);
    this.vehicalsService.addVehicle(this.vehicle);
  }
}

