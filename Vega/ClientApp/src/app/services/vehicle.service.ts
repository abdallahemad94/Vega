import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from "@angular/core";
import { Make } from "../models/Make";
import { Observable } from "rxjs";
import { SaveVehicle } from "../models/SaveVehicle";
import { Feature } from "../models/Feature";
import { Vehicle } from "../models/Vehicle";
import { ToastyService, ToastyConfig, ToastData, ToastOptions } from "ng2-toasty";

@Injectable()
export class VehiclesService {
  private apiUrl: string = this.baseUrl + "api/vehicles/";

  constructor(
    private readonly http: HttpClient,
    @Inject("BASE_URL") private readonly baseUrl: string,
    private toastService: ToastyService,
    private toastConfig: ToastyConfig
  ) {
  }

  getMakes(): Observable<Make[]>
  {
    return this.http.get<Make[]>(this.apiUrl + "get/makes");
  }

  getFeatures(): Observable<Feature[]> {
    return this.http.get<Feature[]>(this.apiUrl + "get/features");
  }

  addVehicle(vehicle: SaveVehicle): void {
    this.http.post<SaveVehicle>(this.apiUrl + "add", vehicle).subscribe(
      success => {
        const toastOptions: ToastOptions = new ToastOptions();
        toastOptions.title = "Saved Successfully";
        toastOptions.msg = "Your Data has been saved successflly";
        toastOptions.showClose = true;
        toastOptions.theme = "bootstrap";
        this.toastService.success(toastOptions);
      });
  }

  updateVehicle(vehicle: SaveVehicle): void {
    if (vehicle.id <= 0 )
      return;

    this.http.put<SaveVehicle>(this.apiUrl + "update/" + vehicle.id, vehicle).subscribe(
      success => {
        const toastOptions: ToastOptions = new ToastOptions();
        toastOptions.title = "Saved Successfully";
        toastOptions.msg = "Your Data has been saved successflly";
        toastOptions.showClose = true;
        toastOptions.theme = "bootstrap";
        this.toastService.success(toastOptions);
      });
  }

  deleteVehicle(id: number): void {
    this.http.delete(this.apiUrl + "delete/" + id).subscribe(
      success => {
        const toastOptions: ToastOptions = new ToastOptions();
        toastOptions.title = "Delete Successfully";
        toastOptions.msg = "Your Data has been deleted successflly";
        toastOptions.showClose = true;
        toastOptions.theme = "bootstrap";
        this.toastService.success(toastOptions);
      });
  }

  getVehicle(id: number): Observable<object> {
    return this.http.get<object>(this.apiUrl + "get/" + id);
  }

  getAllVehicles(): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(this.apiUrl + "get/all");
  }
}
