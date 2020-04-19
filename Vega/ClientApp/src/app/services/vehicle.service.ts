import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from "@angular/core";
import { Make } from "../models/Make";
import { Observable } from "rxjs";
import { Vehicle } from "../models/Vehicle";

@Injectable()
export class VehicalsService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getMakes(): Observable<Make[]>
  {
    return this.http.get<Make[]>(this.baseUrl + 'api/makes');
  }

  addVehicle(vehicle: Vehicle): void {
    this.http.post<Vehicle>(this.baseUrl + "/api/add/vehicle", vehicle).subscribe(
      success => console.log(success),
      error => console.log(error)
    );
  }
}
