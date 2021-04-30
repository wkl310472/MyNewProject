import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private http: HttpClient) { }

  upload(gameId, photo) {
    let formData = new FormData();

    formData.append('file', photo);

    return this.http.post('/api/games/' + gameId + '/photos', formData);

  }
}
