import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/game.service';
import { ToastrService } from 'ngx-toastr';
import { PhotoService } from '../../services/photo.service';

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

  photos;

  previews;

  constructor(private route: ActivatedRoute,
    private service: GameService,
    private photoService: PhotoService,
    private toastr: ToastrService) {
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

      this.photoService.getPhotos(this.game.id).subscribe(photos => {
        this.photos = photos;
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

  upload(fileInput) {
    if (fileInput) {
      this.photoService.upload(this.game.id, fileInput.files[0])
        .subscribe(photo => this.photos.push(photo));
      this.previews = [];
      this.toastr.success('Image has been saved!', 'Success');
    }
    else {
      console.log('no image added');
    }

  }

  previewPhotos(fileInput) {

    const files = fileInput.files;
    this.previews = [];

    for (let file of files) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.previews.push(reader.result as string);
      };
    }
  }
}

