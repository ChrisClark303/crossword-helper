import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable()
export class HttpApiKeyInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const isApiRequest = request.url.startsWith(environment.serviceUrl);

    if (isApiRequest) {
      request = request.clone({
        setHeaders: {
          'X-Api-Key': environment.apiKey
        }
      });
    }

    return next.handle(request);
  }
}
