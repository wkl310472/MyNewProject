import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { endpoints, httpOptions, toQueryString } from './shared';

@Injectable({
  providedIn: 'root'
})
export class GameService{

  constructor(private http: HttpClient) {

  }

  getGames(filter) {
    return this.http.get(endpoints['games'] + '?' + toQueryString(filter,true));
  }

  getGame(id) {
    return this.http.get(endpoints['games'] + '/' + id);
  }

  getGenres() {
    return this.http.get(endpoints['genres']);
  }

  getPlatforms() {
    return this.http.get(endpoints['platforms']);
  }

  updateGame(game: any, id: number) {
    return this.http.put(endpoints['games'] + '/' + id, JSON.stringify(game), httpOptions);
  }

  createGame(game: any) {
    return this.http.post(endpoints['games'], JSON.stringify(game), httpOptions);
  }
}
