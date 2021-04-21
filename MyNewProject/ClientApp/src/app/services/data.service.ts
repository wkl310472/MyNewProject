import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class DataService {

  protected url: string;

  constructor(private http: HttpClient) {
    
  }

  getAll() {
    return this.http.get(this.url, { observe: 'body', responseType: 'json' });
  }

  get(id: any) {
    return this.http.get(this.url + '/' + id, { observe: 'body', responseType: 'json' });
  }


}
