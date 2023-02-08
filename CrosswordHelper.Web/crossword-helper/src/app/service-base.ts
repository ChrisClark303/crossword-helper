import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
export class ServiceBase {

    private serviceUrl: string = 'http://localhost:8080';
    private httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };

    constructor(private httpClient: HttpClient) { }

    protected get<T>(url:string): Observable<T[]> {
        var absUrl = `${this.serviceUrl}${url}`;
        console.log("Sending request to " + absUrl)
        return this.httpClient.get<T[]>(absUrl);
      }

     protected async add(url:string)  {
        var absUrl = `${this.serviceUrl}${url}`;
        console.log("Sending request to " + absUrl)
        this.httpClient.post(absUrl, this.httpOptions).subscribe(
          res => {
             console.log(res);
          },
          err => {
             console.log('Error occured');
          });
      }
}
