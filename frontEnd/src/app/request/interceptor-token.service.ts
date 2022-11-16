import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AutorServiceService } from './autor-service.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorTokenService implements HttpInterceptor{
  token = new HttpHeaders().append("Authorization", `Bearer ${this.tk.getToken()}`);
  constructor(private tk: AutorServiceService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    let tokenRed = req.clone({
      setHeaders:{
        Authorization: 'Bearer ' + this.tk.getToken()
      }
    })

    return next.handle(tokenRed);
  }
}