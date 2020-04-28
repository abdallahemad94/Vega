"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SaveVehicle_1 = require("../../models/SaveVehicle");
var AddVehicleComponent = /** @class */ (function () {
    function AddVehicleComponent(vehiclesService, route) {
        this.vehiclesService = vehiclesService;
        this.route = route;
        this.makes = [];
        this.features = [];
        this.vehicle = new SaveVehicle_1.SaveVehicle();
        this.subscriptions = [];
        this.isEditMode = false;
        this.vehicleId = 0;
    }
    AddVehicleComponent.prototype.ngOnInit = function () {
        var _this = this;
        var makesSubscription = this.vehiclesService.getMakes().subscribe(function (result) { return _this.makes = result; }, function (error) { return console.error(error); });
        var featuresSubscription = this.vehiclesService.getFeatures().subscribe(function (result) { return _this.features = result; }, function (error) { return console.error(error); });
        var routeSubscription = this.route.params.subscribe(function (params) {
            if (params["id"]) {
                _this.vehicleId = +params["id"];
                _this.isEditMode = true;
            }
        });
        if (this.isEditMode)
            this.loadVehicle();
        this.subscriptions.push(makesSubscription);
        this.subscriptions.push(featuresSubscription);
        this.subscriptions.push(routeSubscription);
    };
    AddVehicleComponent.prototype.ngOnDestroy = function () {
        this.subscriptions.forEach(function (subscription) { return subscription.unsubscribe(); });
    };
    AddVehicleComponent.prototype.OnSubmit = function (form) {
        var _this = this;
        this.features.forEach(function (f) {
            if (form.value.features[f.name])
                _this.vehicle.features.push(f.id);
        });
        console.log(this.vehicle);
        //this.vehiclesService.addVehicle(this.vehicle);
    };
    AddVehicleComponent.prototype.clearControls = function (form) {
        form.reset();
        this.selectedMake = undefined;
        this.vehicle = new SaveVehicle_1.SaveVehicle();
    };
    AddVehicleComponent.prototype.loadVehicle = function () {
        var _this = this;
        this.vehiclesService.getVehicle(this.vehicleId).subscribe(function (result) {
            _this.vehicle.id = result.id;
            _this.vehicle.isRegistered = result.isRegistered;
            _this.vehicle.modelId = result.model.id;
            _this.vehicle.contactInfo = result.contactInfo;
            result.features.forEach(function (f) { return _this.vehicle.features.push(f.id); });
        });
    };
    AddVehicleComponent = __decorate([
        core_1.Component({
            selector: "add-vehicle",
            templateUrl: './add-vehicle.component.html',
            styleUrls: ['./add-vehicle.component.css']
        })
    ], AddVehicleComponent);
    return AddVehicleComponent;
}());
exports.AddVehicleComponent = AddVehicleComponent;
//# sourceMappingURL=add-vehicle.component.js.map