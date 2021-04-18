import { Component, OnInit } from '@angular/core';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  games: object[];

  constructor(private service: GameService) {

  }

  ngOnInit() {
    this.service.getAll().subscribe(games => this.games = Object.values(games));
  }

}
