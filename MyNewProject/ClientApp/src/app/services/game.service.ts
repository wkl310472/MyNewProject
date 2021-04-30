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

  getGame(id) {
    return this.http.get('/api/games/'+id);
  }

  getGenres() {
    return this.http.get('/api/genres');
  }

  getPlatforms() {
    return this.http.get('/api/platforms');
  }

  updateGame(game: any,id:number) {
    return this.http.put('/api/games/' + id, JSON.stringify(game),
      {headers: {'Content-Type': 'application/json'}, observe: 'body', responseType: 'json' });
  }

  createGame(game: any) {
    return this.http.post('/api/games', JSON.stringify(game),
      { headers: { 'Content-Type': 'application/json' }, observe: 'body', responseType: 'json' });
  }
}
