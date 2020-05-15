"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var sweetalert2_js_1 = require("sweetalert2/dist/sweetalert2.js");
var rxjs_1 = require("rxjs");
var angular_datatables_1 = require("angular-datatables");
var VehiclesListComponent = /** @class */ (function () {
    function VehiclesListComponent(vehiclesService) {
        this.vehiclesService = vehiclesService;
        this.vehicles = [];
        this.dtOptions = {};
        this.dtTrigger = new rxjs_1.Subject();
    }
    VehiclesListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.dtOptions = {
            pagingType: 'full_numbers',
            pageLength: 5,
            columnDefs: [{ orderable: false, targets: [0, 3, 4, 6] }]
        };
        var sources = [
            this.vehiclesService.getAllVehicles(),
            this.vehiclesService.getMakes()
        ];
        rxjs_1.Observable.forkJoin(sources)
            .subscribe(function (_a) {
            var vehicles = _a[0], makes = _a[1];
            _this.vehicles = vehicles;
            _this.dtTrigger.next();
        });
    };
    VehiclesListComponent.prototype.OnDelete = function (id) {
        this.vehiclesService.deleteVehicle(id);
    };
    VehiclesListComponent.prototype.opensweetalert = function (id) {
        var _this = this;
        sweetalert2_js_1.default.fire({
            title: 'Are you sure?',
            text: "Once deleted you can't get it back.",
            icon: 'warning',
            showCloseButton: true,
            showCancelButton: true,
            focusConfirm: false,
            confirmButtonColor: "#d33",
            confirmButtonText: "Delete"
        }).then(function (result) { if (result.value)
            _this.OnDelete(id); });
    };
    __decorate([
        core_1.ViewChild(angular_datatables_1.DataTableDirective)
    ], VehiclesListComponent.prototype, "datatableElement", void 0);
    VehiclesListComponent = __decorate([
        core_1.Component({
            selector: 'vehicles-list',
            templateUrl: './vehicles-list.component.html',
            styleUrls: ['./vehicles-list.component.css']
        })
    ], VehiclesListComponent);
    return VehiclesListComponent;
}());
exports.VehiclesListComponent = VehiclesListComponent;
//# sourceMappingURL=vehicles-list.component.js.map