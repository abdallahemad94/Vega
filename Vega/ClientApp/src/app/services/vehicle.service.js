"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ng2_toasty_1 = require("ng2-toasty");
var VehiclesService = /** @class */ (function () {
    function VehiclesService(http, baseUrl, toastService, toastConfig) {
        this.http = http;
        this.baseUrl = baseUrl;
        this.toastService = toastService;
        this.toastConfig = toastConfig;
        this.apiUrl = this.baseUrl + "api/vehicles/";
    }
    VehiclesService.prototype.getMakes = function () {
        return this.http.get(this.apiUrl + "get/makes");
    };
    VehiclesService.prototype.getFeatures = function () {
        return this.http.get(this.apiUrl + "get/features");
    };
    VehiclesService.prototype.addVehicle = function (vehicle) {
        var _this = this;
        this.http.post(this.apiUrl + "add", vehicle).subscribe(function (success) {
            var toastOptions = new ng2_toasty_1.ToastOptions();
            toastOptions.title = "Saved Successfully";
            toastOptions.msg = "Your Data has been saved successflly";
            toastOptions.showClose = true;
            toastOptions.theme = "bootstrap";
            _this.toastService.success(toastOptions);
        });
    };
    VehiclesService.prototype.updateVehicle = function (vehicle) {
        var _this = this;
        if (vehicle.id <= 0)
            return;
        this.http.put(this.apiUrl + "update/" + vehicle.id, vehicle).subscribe(function (success) {
            var toastOptions = new ng2_toasty_1.ToastOptions();
            toastOptions.title = "Saved Successfully";
            toastOptions.msg = "Your Data has been saved successflly";
            toastOptions.showClose = true;
            toastOptions.theme = "bootstrap";
            _this.toastService.success(toastOptions);
        });
    };
    VehiclesService.prototype.deleteVehicle = function (id) {
        var _this = this;
        this.http.delete(this.apiUrl + "delete/" + id).subscribe(function (success) {
            var toastOptions = new ng2_toasty_1.ToastOptions();
            toastOptions.title = "Delete Successfully";
            toastOptions.msg = "Your Data has been deleted successflly";
            toastOptions.showClose = true;
            toastOptions.theme = "bootstrap";
            _this.toastService.success(toastOptions);
        });
    };
    VehiclesService.prototype.getVehicle = function (id) {
        return this.http.get(this.apiUrl + "get/" + id);
    };
    VehiclesService.prototype.getAllVehicles = function () {
        return this.http.get(this.apiUrl + "get/all");
    };
    VehiclesService = __decorate([
        core_1.Injectable(),
        __param(1, core_1.Inject("BASE_URL"))
    ], VehiclesService);
    return VehiclesService;
}());
exports.VehiclesService = VehiclesService;
//# sourceMappingURL=vehicle.service.js.map