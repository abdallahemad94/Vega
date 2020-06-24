import { Injectable } from '@angular/core';
import { Subject } from "rxjs";

@Injectable()
export class ProgressService {
  private uploadProgress: Subject<any>;

  beginTracking() {
    this.uploadProgress = new Subject<any>();
    return this.uploadProgress;
  }

  notify(value: any) {
    this.uploadProgress.next(value);
  }

  endTracking() {
    this.uploadProgress.complete();
  }
}
