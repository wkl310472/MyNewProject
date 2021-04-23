import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GameService } from '../../services/game.service';

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


  constructor(private route: ActivatedRoute, private router: Router, private service: GameService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.service.getOneGame(params.get('id')).subscribe(game => {
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


  navigateBack() {
    this.router.navigate(['/games']);
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
    });

  }

}
