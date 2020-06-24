import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from "@angular/common/http";
import { AuthService } from "./auth.service";
import { Observable, throwError } from "rxjs";
import { mergeMap } from "rxjs/operators";

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private auth: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.auth.loggedIn) {
      try {
        return this.auth.getTokenSilently$({ audience: "https://api.vega.com" }).pipe(
          mergeMap(token => {
            const tokenReq = req.clone({
              setHeaders: { Authorization: `Bearer ${token}` }
            });
            return next.handle(tokenReq);
          })
        );
      } catch (e) {
        try {
          return this.auth.getTokenWithPopup$({ audience: "https://api.vega.com" }).pipe(
            mergeMap(token => {
              const tokenReq = req.clone({
                setHeaders: { Authorization: `Bearer ${token}` }
              });
              return next.handle(tokenReq);
            })
          );
        } catch (e) {
          throwError(e);
          return next.handle(req);
        }
      }
    }
    else
      return next.handle(req);
  }
}
