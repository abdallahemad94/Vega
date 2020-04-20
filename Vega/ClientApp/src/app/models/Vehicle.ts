import { Make } from "./Make";
import { Model } from "./Model";
import { Feature } from "./Feature";

export class Vehicle {
  id: number = 0;
  modelId: number;
  isRegistered: boolean = false;
  features: number[] = [];
  contactInfo: any = { name: "", phone: "", email: "" }
}
