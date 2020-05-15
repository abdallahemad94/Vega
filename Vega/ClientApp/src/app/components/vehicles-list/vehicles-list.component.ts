import { Component, OnInit, ViewChild } from '@angular/core';
import { VehiclesService } from "../../services/vehicle.service";
import Swal from 'sweetalert2/dist/sweetalert2.js';
import { Observable } from "rxjs";
import * as wjcCore from 'wijmo/wijmo';
import * as wjcGrid from 'wijmo/wijmo.grid';
import { Vehicle } from "../../models/Vehicle";

@Component({
    selector: 'vehicles-list',
    templateUrl: './vehicles-list.component.html',
    styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent implements OnInit {
  @ViewChild('flex') flex: wjcGrid.FlexGrid;
  vehicles: Vehicle[] = [];
  collectionView: wjcCore.CollectionView = new wjcCore.CollectionView();
  pageSize: number = 10;
  constructor(private vehiclesService: VehiclesService) { }

  ngOnInit() {
    const sources: any[] = [
      this.vehiclesService.getAllVehicles(),
    ];

    Observable.forkJoin(sources)
      .subscribe(
        ([vehicles]) => {
          vehicles.forEach(v => v.lastUpdated = new Date(Date.parse(v.lastUpdated)));
          this.vehicles = vehicles;
          this.collectionView = new wjcCore.CollectionView(vehicles, { pageSize: this.pageSize });
        });
  }

  OnDelete(id: number) {
    this.vehiclesService.deleteVehicle(id).then(() => {
      let removedIndex = this.vehicles.findIndex((v) => v.id == id);
      this.vehicles.splice(removedIndex, 1);
      let currentPage = this.collectionView.pageIndex;
      this.collectionView = new wjcCore.CollectionView(this.vehicles, { pageSize: this.pageSize});
      this.flex.itemsSource = this.collectionView;
      this.flex.refresh(true);
      if (this.collectionView.pageCount > currentPage)
        this.collectionView.moveToPage(currentPage);
      else
        this.collectionView.moveToLastPage();
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
    }).then(result => { if (result.value) this.OnDelete(id) });
  }

  changePageCount() {
    this.collectionView = new wjcCore.CollectionView(this.vehicles, { pageSize: this.pageSize });
    this.flex.itemsSource = this.collectionView;
    this.flex.refresh(true);
  }
}
