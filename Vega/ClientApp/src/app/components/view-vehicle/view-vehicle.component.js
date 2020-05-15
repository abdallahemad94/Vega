"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var Vehicle_1 = require("../../models/Vehicle");
var sweetalert2_js_1 = require("sweetalert2/dist/sweetalert2.js");
var rxjs_1 = require("rxjs");
var ViewVehicleComponent = /** @class */ (function () {
    function ViewVehicleComponent(vehiclesService, route, router, progressService) {
        var _this = this;
        this.vehiclesService = vehiclesService;
        this.route = route;
        this.router = router;
        this.progressService = progressService;
        this.photsosTab = false;
        this.vehicleTab = true;
        this.vehicle = new Vehicle_1.Vehicle();
        this.photos = [];
        this.route.params.subscribe(function (params) {
            if (params["id"] && !isNaN(params["id"])) {
                _this.vehicleId = +params["id"];
            }
            else {
                _this.router.navigate([''], { relativeTo: _this.route.parent });
            }
        });
    }
    ViewVehicleComponent.prototype.ngOnInit = function () {
        var _this = this;
        var sources;
        if (!this.vehicleId)
            this.router.navigate([''], { relativeTo: this.route.parent });
        else
            sources = [
                this.vehiclesService.getVehicle(this.vehicleId),
                this.vehiclesService.getPhotos(this.vehicleId)
            ];
        var subscription = rxjs_1.forkJoin(sources).subscribe(function (_a) {
            var vehicle = _a[0], photos = _a[1];
            _this.vehicle = vehicle;
            _this.photos = photos;
        });
    };
    ViewVehicleComponent.prototype.opensweetalert = function (id) {
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
        }).then(function (result) {
            if (result.value)
                _this.OnDelete(id);
        });
    };
    ViewVehicleComponent.prototype.OnDelete = function (id) {
        var _this = this;
        this.vehiclesService.deleteVehicle(id).then(function () { return _this.router.navigate([''], { relativeTo: _this.route.parent }); });
    };
    ViewVehicleComponent.prototype.addPhoto = function () {
        var _this = this;
        var nativeElement = this.fileInput.nativeElement;
        this.progressService.downloadProgress.subscribe(function (progrss) { return console.log(progrss); });
        this.progressService.uploadProgress.subscribe(function (progrss) { return console.log(progrss); });
        this.vehiclesService.addPhoto(this.vehicleId, nativeElement.files[0]).subscribe(function (success) {
            _this.photos.push(success);
        });
    };
    __decorate([
        core_1.ViewChild("file")
    ], ViewVehicleComponent.prototype, "fileInput", void 0);
    ViewVehicleComponent = __decorate([
        core_1.Component({
            selector: 'view-vehicle',
            templateUrl: './view-vehicle.component.html',
            styleUrls: ['./view-vehicle.component.css']
        })
    ], ViewVehicleComponent);
    return ViewVehicleComponent;
}());
exports.ViewVehicleComponent = ViewVehicleComponent;
//# sourceMappingURL=view-vehicle.component.js.map