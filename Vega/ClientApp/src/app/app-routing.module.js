"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var core_1 = require("@angular/core");
var home_component_1 = require("./components/home/home.component");
var add_vehicle_component_1 = require("./components/add-vehicle/add-vehicle.component");
var vehicles_list_component_1 = require("./components/vehicles-list/vehicles-list.component");
var routs = [
    { path: '', redirectTo: "vehicles", pathMatch: 'full' },
    {
        path: 'vehicles', component: home_component_1.HomeComponent,
        children: [
            { path: "", component: vehicles_list_component_1.VehiclesListComponent },
            { path: "new", component: add_vehicle_component_1.AddVehicleComponent },
            { path: ":id", component: add_vehicle_component_1.AddVehicleComponent }
        ]
    },
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        core_1.NgModule({
            imports: [
                router_1.RouterModule.forRoot(routs),
            ],
            exports: [router_1.RouterModule]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
exports.AppRoutingModule = AppRoutingModule;
//# sourceMappingURL=app-routing.module.js.map