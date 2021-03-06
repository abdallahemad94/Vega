import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { VehiclesService } from "../../services/vehicle.service";
import { ActivatedRoute, Router } from "@angular/router";
import Swal from 'sweetalert2/dist/sweetalert2.js';
import { forkJoin } from "rxjs";
import { ProgressService } from "../../services/progress.service";
import { Location } from "@angular/common";
import { AuthService } from "../../services/auth.service";
import { faUpload } from '@fortawesome/free-solid-svg-icons';
import { Vehicle } from "../../models/Vehicle";


@Component({
    selector: 'view-vehicle',
    templateUrl: './view-vehicle.component.html',
    styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  faUpload = faUpload;
  @ViewChild("fileInput") fileInput: ElementRef;
  photsosTab: boolean = false;
  vehicleTab: boolean = true;
  vehicleId: number;
  vehicle: Vehicle = new Vehicle();
  photos: any[] = [];
  progress: any = null;

  constructor(
    private vehiclesService: VehiclesService,
    private route: ActivatedRoute,
    private router: Router,
    private progressService: ProgressService,
    private location: Location,
    private auth: AuthService) {
    this.route.params.subscribe(params => {
      if (params["id"] && !isNaN(params["id"])) {
        this.vehicleId = +params["id"];
      }
      else {
        this.router.navigate([''], { relativeTo: this.route.parent });
      }
    });
  }

  ngOnInit() {
    let sources: any[];
    if (!this.vehicleId)
      this.router.navigate([''], { relativeTo: this.route.parent });
    else
      sources = [
        this.vehiclesService.getVehicle(this.vehicleId),
        this.vehiclesService.getPhotos(this.vehicleId)
      ];

    const subscription = forkJoin(sources).subscribe(
      ([vehicle, photos]: [Vehicle, any[]]) => {
        this.vehicle = vehicle;
        this.photos = photos;
      });
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
    }).then(result => {
      if (result.value)
        this.OnDelete(id);
      
    })
  }

  OnDelete(id: number) {
    this.vehiclesService.deleteVehicle(id).then(() => this.router.navigate([''], { relativeTo: this.route.parent }));
  }

  uploadPhoto() {
    this.progressService.beginTracking().subscribe(progrss => this.progress = progrss, null, () => { this.progress = null; this.fileInput.nativeElement.value = ""; });
    let files = this.fileInput.nativeElement.files;
    this.vehiclesService.addPhoto(this.vehicleId, files[0]).subscribe(
      success => {
        this.photos.push(success);
      });
  }

  fileInputChange() { }

  back() {
    this.location.back();
  }
}
