import { Injectable, OnDestroy } from '@angular/core';
import { Subject, Observable, Subscription } from "rxjs";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpEventType, HttpProgressEvent, HttpDownloadProgressEvent } from "@angular/common/http";
import { HttpUploadProgressEvent } from "@angular/common/http/src/response";

@Injectable()
export class ProgressService {
  private uploadProgress: Subject<any>;

  createUploadProgress() {
    this.uploadProgress = new Subject<any>();
    return this.uploadProgress;
  }

  notify(value: any) {
    this.uploadProgress.next(value);
  }

  complete() {
    this.uploadProgress.complete();
  }
}

@Injectable()
export class HttpInterceptorWithProgress implements HttpInterceptor, OnDestroy {
  progressSubscription: Subscription;
  constructor(private service: ProgressService) { }

  ngOnDestroy(): void {
    this.progressSubscription.unsubscribe();
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.reportProgress)
      return next.handle(req).pipe((observ: Observable<HttpEvent<any>>): Observable<HttpEvent<any>> => {
        this.progressSubscription = observ.subscribe((event: HttpEvent<any>) => {
          if (event.type == HttpEventType.UploadProgress)
            this.service.notify(this.createProgress(<HttpUploadProgressEvent>event));
        },
          null,
          () => this.service.complete());
        return observ;
      });
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

