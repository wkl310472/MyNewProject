import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/game.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-game-info',
  templateUrl: './game-info.component.html',
  styleUrls: ['./game-info.component.css']
})
export class GameInfoComponent implements OnInit {

  game: any = {
    genres: [],
    platforms: []
  };

  constructor(private route: ActivatedRoute, private service: GameService, private toastr: ToastrService) {
    this.route.params.subscribe(params => {
      this.game.id = params['id'] ? +params['id'] : NaN;
    });
  }

  ngOnInit() {
    if (this.game.id) {
      this.service.getGame(this.game.id).subscribe(game => {
        this.game = game;
      });
    }
  }

}
