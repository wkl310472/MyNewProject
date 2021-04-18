import { Component, OnInit } from '@angular/core';
import { GenreService } from '../../services/genre.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  genres: any[];
  platforms: any[];

  constructor(private service: GenreService) { }

  ngOnInit() {

    this.service.getAll().subscribe(genres => this.genres = Object.values(genres));
  }

}
