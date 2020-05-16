import { ErrorHandler, Inject, NgZone, Injectable, isDevMode} from "@angular/core";
import { ToastOptions, ToastyService } from "ng2-toasty";
import * as Sentry from "@sentry/browser";

Sentry.init({
  dsn: "https://b6be688daca74ee1a46974886ecfd11a@o382943.ingest.sentry.io/5212555"
});

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  constructor(
    private ngZone: NgZone,
    @Inject(ToastyService) private toastService: ToastyService
  ) { }

  handleError(error: any): void {
    this.ngZone.run(() => {
      const toastOptions: ToastOptions = new ToastOptions();
      toastOptions.title = `${error.rejection.name}: ${error.rejection.status}`;
      toastOptions.msg = error.rejection.error;
      toastOptions.showClose = true;
      toastOptions.theme = "bootstrap";
      this.toastService.error(toastOptions);
      if (!isDevMode())
      {
        const eventId = Sentry.captureException(error.originalError || error);
        Sentry.showReportDialog({ eventId });
      }
    });
    }
}
