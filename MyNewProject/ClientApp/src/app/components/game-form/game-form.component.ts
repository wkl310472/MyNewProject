import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/game.service';
import { ToastrService } from 'ngx-toastr';
import { PhotoService } from '../../services/photo.service';
import { IAppState } from '../../store/app.store';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { IGame, selectGame } from '../../store/entities/games.store';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.css']
})
export class GameFormComponent implements OnInit {

  game$: Observable<IGame>;
  game: any = {
    genres: [],
    platforms: []
  };
  genres;
  platforms;

  photos;

  previews;

  form = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(255)
    ]),
    developer: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(255)
    ]),
    release: new FormControl('', [
      Validators.required
    ]),
    price: new FormControl('', [
      Validators.min(0),
      Validators.max(999.99)
    ]),
    numberInStock: new FormControl('', [
      Validators.min(0),
      Validators.max(99)
    ])
  })

  constructor(private route: ActivatedRoute,
    private service: GameService,
    private photoService: PhotoService,
    private toastr: ToastrService,
    private store: Store<IAppState>) {
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
      this.photoService.upload(this.game.id, fileInput.files)
        .subscribe(photos => {
          console.log(photos);
          for (let photo of Object.values(photos)) {
            this.photos.push(photo);
          }
        });
      this.previews = [];
      this.toastr.success('Image has been saved!', 'Success');
    }
    else {
      console.log('no image added');
    }
  }

  delete(photo) {
    let index;
    if (photo.id) {
      index = this.photos.indexOf(photo);
      this.photos.splice(index, 1);

      this.photoService.delete(this.game.id, photo.id).subscribe(p => {
        console.log(p);
      });
    }
    else {
      index = this.previews.indexOf(photo);
      this.previews.splice(index, 1);
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


  get gameName() {
    return this.form.get('name');
  }

  get gameDeveloper() {
    return this.form.get('developer');
  }
  get gameRelease() {
    return this.form.get('release');
  }
  get gamePrice() {
    return this.form.get('price');
  }
  get gameNumberInStock() {
    return this.form.get('numberInStock');
  }
}

