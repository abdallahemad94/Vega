import { Make } from "./Make";
import { Model } from "./Model";
import { Feature } from "./Feature";

export class Vehicle {
  id: number = 0;
  make: Make;
  model: Model;
  isRegistered: boolean = false;
  features: Feature[] = [];
  contactName: string = "";
  contactPhone: string = "";
  contactEmail: string = "";
}
