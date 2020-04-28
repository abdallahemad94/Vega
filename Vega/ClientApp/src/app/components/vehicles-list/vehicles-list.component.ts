import { Component, OnInit, ViewChild } from '@angular/core';
import { VehiclesService } from "../../services/vehicle.service";
import { Vehicle } from "../../models/Vehicle";
import Swal from 'sweetalert2/dist/sweetalert2.js';
import { Subject, forkJoin, Observable } from "rxjs";
import { Make } from "../../models/Make";
import { DataTableDirective } from "angular-datatables";

@Component({
    selector: 'vehicles-list',
    templateUrl: './vehicles-list.component.html',
    styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent implements OnInit {
  @ViewChild(DataTableDirective) datatableElement: DataTableDirective;
  vehicles: Vehicle[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  constructor(private vehiclesService: VehiclesService) {
  }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      columnDefs: [{ orderable: false, targets: [0, 3, 4, 6] }]
    };

    const sources: any[] = [
      this.vehiclesService.getAllVehicles(),
      this.vehiclesService.getMakes()
    ];

    Observable.forkJoin(sources)
      .subscribe(
        ([vehicles, makes]) => {
          this.vehicles = vehicles;
          this.dtTrigger.next();
      });
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
