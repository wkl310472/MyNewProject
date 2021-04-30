import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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

  @ViewChild('fileInput', { static: false }) fileInput: ElementRef;
  game: any = {
    genres: [],
    platforms: []
  };

  constructor(private route: ActivatedRoute,
    private gameService: GameService,
    private photoService: PhotoService,
    private toastr: ToastrService) {
    this.route.params.subscribe(params => {
      this.game.id = params['id'] ? +params['id'] : NaN;
    });
  }

  ngOnInit() {
    if (this.game.id) {
      this.gameService.getGame(this.game.id).subscribe(game => {
        this.game = game;
      });
    }
  }

  uploadPhoto() {
    let nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    this.photoService.upload(this.game.id, nativeElement.files[0])
      .subscribe(res => console.log(res));
  }

}
