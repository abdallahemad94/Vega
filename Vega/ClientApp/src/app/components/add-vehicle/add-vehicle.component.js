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
        this.features = [];
    }
    AddVehicleComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscribtion = this.vehicalsService.getMakes().subscribe(function (result) { return _this.makes = result; }, function (error) { return console.log(error); });
        this.vehicle = new Vehicle_1.Vehicle();
        console.log(this.makes);
        this.createFeatures();
    };
    AddVehicleComponent.prototype.ngOnDestroy = function () {
        this.subscribtion.unsubscribe();
    };
    AddVehicleComponent.prototype.createFeatures = function () {
        for (var _i = 0, _a = [0, 1, 2, 3, 4, 5, 6]; _i < _a.length; _i++) {
            var i = _a[_i];
            this.features.push({ id: i, name: "feature" + i, selected: i % 2 == 0 });
        }
    };
    AddVehicleComponent.prototype.OnSubmit = function (form) {
        console.log(form.controls['make'].valid);
        this.vehicalsService.addVehicle(this.vehicle);
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