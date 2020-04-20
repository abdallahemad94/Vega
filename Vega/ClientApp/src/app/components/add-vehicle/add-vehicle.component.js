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
var AddVehicleComponent = /** @class */ (function () {
    function AddVehicleComponent(vehicalsService) {
        this.vehicalsService = vehicalsService;
        this.makes = [];
        this.vehicle = new Vehicle_1.Vehicle();
        this.features = [];
        this.subscribtions = [];
    }
    AddVehicleComponent.prototype.ngOnInit = function () {
        var _this = this;
        var makesSubscribtion = this.vehicalsService.getMakes().subscribe(function (result) { return _this.makes = result; }, function (error) { return console.error(error); });
        var featuresSubscribtion = this.vehicalsService.getFeatures().subscribe(function (result) { return _this.features = result; }, function (error) { return console.error(error); });
        this.subscribtions.push(makesSubscribtion);
        this.subscribtions.push(featuresSubscribtion);
    };
    AddVehicleComponent.prototype.ngOnDestroy = function () {
        this.subscribtions.forEach(function (subscribtion) { return subscribtion.unsubscribe(); });
    };
    AddVehicleComponent.prototype.OnSubmit = function (form) {
        var _this = this;
        this.features.forEach(function (f) {
            if (form.value.features[f.name])
                _this.vehicle.features.push(f.id);
        });
        this.vehicalsService.addVehicle(this.vehicle);
    };
    AddVehicleComponent.prototype.clearControls = function (form) {
        form.reset();
        this.selectedMake = undefined;
        this.vehicle = new Vehicle_1.Vehicle();
    };
    AddVehicleComponent = __decorate([
        core_1.Component({
            selector: 'add-vehicle',
            templateUrl: './add-vehicle.component.html',
            styleUrls: ['./add-vehicle.component.css']
        })
    ], AddVehicleComponent);
    return AddVehicleComponent;
}());
exports.AddVehicleComponent = AddVehicleComponent;
//# sourceMappingURL=add-vehicle.component.js.map