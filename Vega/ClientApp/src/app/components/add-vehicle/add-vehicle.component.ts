import { Component, OnDestroy, OnInit } from '@angular/core';
import { VehiclesService } from '../../services/vehicle.service';
import { Make } from '../../models/Make';
import { NgForm } from "@angular/forms";
import { Feature } from "../../models/Feature";
import { Subscription, Observable } from "rxjs";
import { SaveVehicle } from "../../models/SaveVehicle";
import { ActivatedRoute } from "@angular/router";
import "rxjs/add/observable/forkJoin";
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Component({
  selector: "add-vehicle",
  templateUrl: './add-vehicle.component.html',
  styleUrls: ['./add-vehicle.component.css']
})
export class AddVehicleComponent implements OnInit, OnDestroy {
  public makes: Make[] = [];
  public features: Feature[] = [];
  public vehicle: SaveVehicle = new SaveVehicle();
  public selectedMake: Make;
  private subscriptions: Subscription[] = [];
  private isEditMode: boolean = false;
  private vehicleId: number = 0; 
  private markedFeatures: {} = {};

  constructor(private readonly vehiclesService: VehiclesService, private route: ActivatedRoute)
  {
    this.route.params.subscribe(params => {
      if (params["id"] && !isNaN(params["id"])) {
        this.vehicleId = +params["id"];
        this.isEditMode = true;
      }
    });
  }

  ngOnInit() {
    const sources: any[] = [
      this.vehiclesService.getMakes(),
      this.vehiclesService.getFeatures()
    ];

    if (this.isEditMode)
      sources.push(this.vehiclesService.getVehicle(this.vehicleId));

    const subscription = Observable.forkJoin(sources).subscribe(
      ([makes, features, vehicle]) => {
        this.makes = makes;
        this.features = features;
        this.features.forEach(f => this.markedFeatures[f.id] = false);
        if (this.isEditMode && vehicle)
          this.loadVehicle(vehicle);
      });

    this.subscriptions.push(subscription);
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  OnSubmit(form: NgForm): void {
    this.vehicle.features = [];
    this.features.forEach(f => {
      if (this.markedFeatures[f.id])
        this.vehicle.features.push( f.id );
    });
    if (this.isEditMode)
      this.vehiclesService.updateVehicle(this.vehicle);
    else
      this.vehiclesService.addVehicle(this.vehicle);
  }

  clearControls(form: NgForm) {
    form.reset();
    this.selectedMake = undefined;
    this.vehicle = new SaveVehicle();
    this.markedFeatures = {};
  }

  loadVehicle(vehicle) {
    this.vehicle.id = vehicle.id;
    this.vehicle.isRegistered = vehicle.isRegistered;
    this.vehicle.modelId = vehicle.model.id;
    this.vehicle.contactInfo = vehicle.contactInfo;
    vehicle.features.forEach(f => this.markedFeatures[f.id] = true);
    this.selectedMake = this.makes.find(m => m.id == vehicle.make.id);
  }

  OnDelete(id: number) {
    this.vehiclesService.deleteVehicle(id);
  }

  opensweetalert(id: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: "Once deleted you can't get it back.",
      icon: 'warning',
      showCloseButton: true,
      showCancelButton: true,
      focusConfirm: false,
      confirmButtonColor: "#d33",
      confirmButtonText: "Delete"
    }).then(result => { if (result.value) this.OnDelete(id) });
  }
}

