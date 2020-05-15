import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from "@angular/core";
import { Make } from "../models/Make";
import { Observable } from "rxjs";
import { SaveVehicle } from "../models/SaveVehicle";
import { Feature } from "../models/Feature";
import { Vehicle } from "../models/Vehicle";
import { ToastyService, ToastOptions } from "ng2-toasty";
import { error } from "@angular/compiler/src/util";
import { ProgressService } from "./progress.service";

@Injectable()
export class VehiclesService {
  private apiUrl: string = this.baseUrl + "api/vehicles/";

  constructor(
    private readonly http: HttpClient,
    @Inject("BASE_URL") private readonly baseUrl: string,
    private toastService: ToastyService,
  ) {
  }

  getMakes(): Observable<Make[]>
  {
    return this.http.get<Make[]>(this.apiUrl + "get/makes");
  }

  getFeatures(): Observable<Feature[]> {
    return this.http.get<Feature[]>(this.apiUrl + "get/features");
  }

  addVehicle(vehicle: SaveVehicle): Promise<SaveVehicle> {
    return this.http.post<SaveVehicle>(this.apiUrl + "add", vehicle).toPromise().then(
      success => {
        const toastOptions: ToastOptions = new ToastOptions();
        toastOptions.title = "Saved Successfully";
        toastOptions.msg = "Your Data has been saved successflly";
        toastOptions.showClose = true;
        toastOptions.theme = "bootstrap";
        this.toastService.success(toastOptions);
        return success;
      });
  }

  updateVehicle(vehicle: SaveVehicle): Promise<SaveVehicle> {
    if (vehicle.id <= 0 )
      return;

    this.http.put<SaveVehicle>(this.apiUrl + "update/" + vehicle.id, vehicle).toPromise().then(
      success => {
        const toastOptions: ToastOptions = new ToastOptions();
        toastOptions.title = "Saved Successfully";
        toastOptions.msg = "Your Data has been saved successflly";
        toastOptions.showClose = true;
        toastOptions.theme = "bootstrap";
        this.toastService.success(toastOptions);
        return success;
      });
  }

  deleteVehicle(id: number): Promise<void> {
    return this.http.delete(this.apiUrl + "delete/" + id).toPromise().then(
      success => {
        const toastOptions: ToastOptions = new ToastOptions();
        toastOptions.title = "Delete Successfully";
        toastOptions.msg = "Your Data has been deleted successflly";
        toastOptions.showClose = true;
        toastOptions.theme = "bootstrap";
        this.toastService.success(toastOptions);
      });
  }

  getVehicle(id: number): Observable<Vehicle> {
    return this.http.get<Vehicle>(this.apiUrl + "get/" + id);
  }

  getAllVehicles(): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(this.apiUrl + "get/all");
  }

  getPhotos(vehicleId: number): Observable<any[]>{
    if (vehicleId == null || vehicleId <= 0)
      throw error("Vehicle Id cannot be empty");

    return this.http.get<any[]>(this.apiUrl + `get/photos/${vehicleId}`);
  }

  addPhoto(vehicleId: number, image: File): any {
    let formdate = new FormData();
    formdate.append("file", image);
    return this.http.post<any>(this.apiUrl + `add/photos/${vehicleId}`, formdate, { reportProgress: true });
  }
}
