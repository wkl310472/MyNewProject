import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GameService } from '../../services/game.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.css']
})
export class GameFormComponent implements OnInit {

  game: any = {
    genres: [],
    platforms: []
  };
  genres;
  platforms;

  constructor(private route: ActivatedRoute, private router: Router, private service: GameService, private toastr: ToastrService) {
    this.route.params.subscribe(params => {
      this.game.id = params['id'] ? +params['id'] : NaN;
    });
  }

  ngOnInit() {
    if (this.game.id) {
      this.service.getGame(this.game.id).subscribe(game => {
        this.game = game;
        this.game.genres = game['genres'].map(item => item['id']);
        this.game.platforms = game['platforms'].map(item => item['id']);
      });
    }

    this.service.getGenres().subscribe(genres => {
      this.genres = genres;
    });

    this.service.getPlatforms().subscribe(platforms => {
      this.platforms = platforms;
    });
  }

  onGenresChange(event, genreId: number) {
    if (event.checked) {
      this.game.genres.push(genreId);
    }
    else {
      const index = this.game.genres.indexOf(genreId);
      this.game.genres.splice(index, 1);
    }
  }

  onPlatformsChange(event, platformId: number) {
    if (event.checked) {
      this.game.platforms.push(platformId);
    }
    else {
      const index = this.game.platforms.indexOf(platformId);
      this.game.platforms.splice(index, 1);
    }
  }

  submit() {
    if (this.game.id) {
      this.service.updateGame(this.game, this.game.id).subscribe(updatedGame => {
        console.log(updatedGame);
        this.toastr.success('Game has been updated!', 'Success');
      });
    }
    else {
      delete this.game.id;
      this.service.createGame(this.game).subscribe(newGame => {
        console.log(newGame);
        this.toastr.success('Game has been created!', 'Success');
      });
    }
  }

  reset() {
    this.game.genres = [];
    this.game.platforms = [];
  }

}
