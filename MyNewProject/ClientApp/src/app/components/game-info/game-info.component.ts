import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/game.service';
import { ToastrService } from 'ngx-toastr';
import { PhotoService } from '../../services/photo.service';

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

  photos;

  constructor(private route: ActivatedRoute,
    private gameService: GameService,
    private photoService: PhotoService) {
    this.route.params.subscribe(params => {
      this.game.id = params['id'] ? +params['id'] : NaN;
    });
  }

  ngOnInit() {
    this.photoService.getPhotos(this.game.id).subscribe(photos => {
      this.photos = photos;
    });

    this.gameService.getGame(this.game.id).subscribe(game => {
      this.game = game;
    });
  }

  onUpload(event) {
    this.photoService.upload(this.game.id, event.target.files[0])
      .subscribe(photo => this.photos.push(photo));
  }
}
