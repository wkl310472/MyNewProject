import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


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



}
