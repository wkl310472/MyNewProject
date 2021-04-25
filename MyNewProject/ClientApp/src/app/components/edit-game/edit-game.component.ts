import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { GameService } from '../../services/game.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-game',
  templateUrl: './edit-game.component.html',
  styleUrls: ['./edit-game.component.css']
})
export class EditGameComponent implements OnInit {

  game: any = {
    genres: [],
    platforms: []
  };
  genres;
  platforms;

  constructor(private route: ActivatedRoute, private service: GameService, private toastr: ToastrService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.service.getGame(params.get('id')).subscribe(game => {
        this.game = game;
        this.game.genres = game['genres'].map(item => item['id']);
        this.game.platforms = game['platforms'].map(item => item['id']);
      });
    });

    this.service.getGenres().subscribe(genres => {
      this.genres = genres;
    });

    this.service.getPlatforms().subscribe(platforms => {
      this.platforms = platforms;
    });
  }

  onGenresChange(event,genreId: number) {
    if (event.target.checked) {
      this.game.genres.push(genreId);
    }
    else {
      const index = this.game.genres.indexOf(genreId);
      this.game.genres.splice(index, 1);
    }
  }

  onPlatformsChange(event, platformId: number) {
    if (event.target.checked) {
      this.game.platforms.push(platformId);
    }
    else {
      const index = this.game.platforms.indexOf(platformId);
      this.game.platforms.splice(index, 1);
    }
  }

  updateGame(release) {

    this.game.release = release;
    this.service.updateGame(this.game,this.game.id).subscribe(updatedGame => {
      console.log(updatedGame);
    });
  }

  notify() {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }

}
