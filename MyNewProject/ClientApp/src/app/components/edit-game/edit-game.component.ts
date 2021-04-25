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

  id: number;
  game: object;
  genres;
  platforms;
  genreIds;
  platformIds;


  constructor(private route: ActivatedRoute, private service: GameService, private toastr: ToastrService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.service.getGame(params.get('id')).subscribe(game => {
        this.game = game;
        this.genreIds = game['genres'].map(item => item['id']);
        this.platformIds = game['platforms'].map(item => item['id']);
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
      this.genreIds.push(genreId);
    }
    else {
      const index = this.genreIds.indexOf(genreId);
      this.genreIds.splice(index, 1);
    }
  }

  onPlatformsChange(event, platformId: number) {
    if (event.target.checked) {
      this.platformIds.push(platformId);
    }
    else {
      const index = this.platformIds.indexOf(platformId);
      this.platformIds.splice(index, 1);
    }
  }

  updateGame(game,id:number) {
    this.service.updateGame(game,id).subscribe(updatedGame => {
      console.log(updatedGame);
    }, err => {
      this.toastr.error('An unexpected error happend.', 'Error');
    });
  }


  notify() {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }

}
