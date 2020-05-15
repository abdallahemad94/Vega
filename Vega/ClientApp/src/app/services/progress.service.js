"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var rxjs_1 = require("rxjs");
var http_1 = require("@angular/http");
var ProgressService = /** @class */ (function () {
    function ProgressService() {
        this.uploadProgress = new rxjs_1.Subject();
        this.downloadProgress = new rxjs_1.Subject();
    }
    ProgressService = __decorate([
        core_1.Injectable()
    ], ProgressService);
    return ProgressService;
}());
exports.ProgressService = ProgressService;
var BrowserXhrWithProgressService = /** @class */ (function (_super) {
    __extends(BrowserXhrWithProgressService, _super);
    function BrowserXhrWithProgressService(progressService) {
        var _this = _super.call(this) || this;
        _this.progressService = progressService;
        return _this;
    }
    BrowserXhrWithProgressService.prototype.build = function () {
        var _this = this;
        var xhr = _super.prototype.build.call(this);
        xhr.onprogress = function (event) {
            _this.progressService.downloadProgress.next(_this.createProgress(event));
        };
        xhr.upload.onprogress = function (event) {
            _this.progressService.uploadProgress.next(_this.createProgress(event));
        };
        return xhr;
    };
    BrowserXhrWithProgressService.prototype.createProgress = function (event) {
        var prog = {
            total: event.total,
            percentage: Math.round(event.loaded / event.total * 100)
        };
        console.log(prog);
        return prog;
    };
    BrowserXhrWithProgressService = __decorate([
        core_1.Injectable()
    ], BrowserXhrWithProgressService);
    return BrowserXhrWithProgressService;
}(http_1.BrowserXhr));
exports.BrowserXhrWithProgressService = BrowserXhrWithProgressService;
//# sourceMappingURL=progress.service.js.map