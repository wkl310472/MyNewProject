import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GameService{

  private readonly gamesEndpoint = '/api/games';
  private readonly genresEndpoint = '/api/genres';
  private readonly platformsEndpoint = '/api/platforms';
  private readonly httpOptions :any = {
    headers: { 'Content-Type': 'application/json' },
    observe: 'body',
    responseType: 'json'
  }

  constructor(private http: HttpClient) {

  }

  getGames(filter) {
    return this.http.get(this.gamesEndpoint + '?' + this.toQueryString(filter));
  }

  toQueryString(obj) {
    let parts = [];
    for (let prop in obj) {
      let value = obj[prop];
      if (value.length>0) {
        for (let id of value) {
          parts.push(prop + '=' + id);
        }
      }
    }
    return parts.join('&');
  }

  getGame(id) {
    return this.http.get(this.gamesEndpoint+ '/' + id);
  }

  getGenres() {
    return this.http.get(this.genresEndpoint);
  }

  getPlatforms() {
    return this.http.get(this.platformsEndpoint);
  }

  updateGame(game: any, id: number) {
    return this.http.put(this.gamesEndpoint + '/' + id, JSON.stringify(game), this.httpOptions);
  }

  createGame(game: any) {
    return this.http.post(this.gamesEndpoint, JSON.stringify(game), this.httpOptions);
  }
}
