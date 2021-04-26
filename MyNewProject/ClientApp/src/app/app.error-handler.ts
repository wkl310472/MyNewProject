import { ErrorHandler, Inject, Injector } from "@angular/core";
import { ToastrService } from 'ngx-toastr';

export class AppErrorHandler implements ErrorHandler {
  constructor(@Inject(Injector) private readonly injector: Injector) {
  }


  handleError(error: any): void {
    console.log(error);
    this.toastr.error('An unexpected error happened.', 'Error', { onActivateTick: true });
  }

  private get toastr(): ToastrService {
    return this.injector.get(ToastrService);
  }

}
