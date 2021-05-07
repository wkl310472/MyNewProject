import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private http: HttpClient) { }

  upload(gameId, photos) {
    let formData = new FormData();

    for (let photo of photos) {
      formData.append('file', photo);
    }

    return this.http.post('/api/games/' + gameId + '/photos', formData);
  }

  getPhotos(gameId) {
    return this.http.get('/api/games/' + gameId + '/photos');
  }

  delete(gameId, id) {
    return this.http.delete('/api/games/' + gameId + '/photos/' + id);
  }
}