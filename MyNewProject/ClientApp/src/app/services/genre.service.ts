import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class GenreService extends DataService {

  constructor(http: HttpClient) {
    super(http);
    this.url = "https://localhost:44369/api/genres";
  }


}
