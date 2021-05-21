import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { endpoints } from './shared';

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

    return this.http.post(endpoints['games'] + '/' + gameId + '/photos', formData);
  }

  getPhotos(gameId) {
    return this.http.get(endpoints['games'] + '/' + gameId + '/photos');
  }

  delete(gameId, id) {
    return this.http.delete(endpoints['games'] + '/' + gameId + '/photos/' + id);
  }
}
