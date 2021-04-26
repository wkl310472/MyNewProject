"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppErrorHandler = void 0;
/*import { ToastrService } from "ngx-toastr";*/
var AppErrorHandler = /** @class */ (function () {
    function AppErrorHandler(ngZone /*, @Inject(ToastrService) private toastr: ToastrService*/) {
        this.ngZone = ngZone;
    }
    AppErrorHandler.prototype.handleError = function (error) {
        this.ngZone.run(function () {
            console.log("ERROR");
            /*this.toastr.error('An unexpected error happened.', 'Error');*/
        });
    };
    return AppErrorHandler;
}());
exports.AppErrorHandler = AppErrorHandler;
//# sourceMappingURL=app.error-handler.js.map