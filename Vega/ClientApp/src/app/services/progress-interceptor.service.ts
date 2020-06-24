import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpUploadProgressEvent, HttpProgressEvent } from "@angular/common/http/src/response";
import { tap } from "rxjs/operators";
import { ProgressService } from "./progress.service";

@Injectable()
export class ProgressInterceptorService implements HttpInterceptor {
  constructor(private service: ProgressService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.reportProgress) {
      return next.handle(req)
        .pipe(
          //filter(event => event.type == HttpEventType.UploadProgress),
          tap(
            (event: HttpUploadProgressEvent) => {
              this.service.notify(this.createProgress(event));
            },
            null,
            () => this.service.endTracking()
          )
        );
    }
    else
      return next.handle(req);
  }

  createProgress(event: HttpProgressEvent) {
    var prog = {
      total: event.total,
      percentage: Math.round(event.loaded / event.total * 100)
    };
    return prog;
  }
}
