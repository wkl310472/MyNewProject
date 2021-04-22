import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GameService{

  constructor(private http: HttpClient) {

  }

  getAllGames() {
    return this.http.get('/api/games');
  }

  getOneGame(id) {
    return this.http.get('/api/games/'+id);
  }

  getGenres() {
    return this.http.get('/api/genres');
  }

  getPlatforms() {
    return this.http.get('/api/platforms');
  }
}
