import { Component, OnDestroy, OnInit } from '@angular/core';
import { VehicalsService } from '../../services/vehicle.service';
import { Make } from '../../models/Make';
import { Vehicle } from '../../models/Vehicle';
import { NgForm } from "@angular/forms";
import { Feature } from "../../models/Feature";
import { Subscription } from "rxjs";
import { HttpErrorResponse } from "@angular/common/http";
import { forEach } from "@angular/router/src/utils/collection";

@Component({
  selector: 'add-vehicle',
  templateUrl: './add-vehicle.component.html',
  styleUrls: ['./add-vehicle.component.css']
})
export class AddVehicleComponent implements OnInit, OnDestroy {
  public makes: Make[] = [];
  public vehicle: Vehicle = new Vehicle();
  public features: Feature[] = [];
  public selectedMake: Make;
  private subscribtions: Subscription[] = [];

  constructor(private vehicalsService: VehicalsService) { }

  ngOnInit() {
    let makesSubscribtion = this.vehicalsService.getMakes().subscribe(
      (result: Make[]) => this.makes = result,
      (error: HttpErrorResponse) => console.error(error)
    );

    let featuresSubscribtion = this.vehicalsService.getFeatures().subscribe(
      (result: Feature[]) => this.features = result,
      (error: HttpErrorResponse) => console.error(error)
    );

    this.subscribtions.push(makesSubscribtion);
    this.subscribtions.push(featuresSubscribtion);

  }

  ngOnDestroy() {
    this.subscribtions.forEach(subscribtion => subscribtion.unsubscribe());
  }

  OnSubmit(form: NgForm): void {
    this.features.forEach(f => {
      if (form.value.features[f.name])
        this.vehicle.features.push(f.id);
      });
    this.vehicalsService.addVehicle(this.vehicle);
  }

  clearControls(form: NgForm) {
    form.reset();
    this.selectedMake = undefined;
    this.vehicle = new Vehicle();
  }
}

