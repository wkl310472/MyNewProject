import { Component, OnInit } from '@angular/core';
import { GameService } from '../../services/game.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit {

  games;
  genres;
  platforms;
  displayedColumns: string[] = ['name', 'developer', 'release', 'action'];
  dataSource;
  opened: boolean;
  filter: any = {
    genres: [],
    platforms: []
  };

  constructor(private service: GameService) { }

  ngOnInit() {
    this.service.getAllGames().subscribe(games => {
      this.games = games;
      this.dataSource = new MatTableDataSource(this.games);
    });

    this.service.getGenres().subscribe(genres => {
      this.genres = genres;
    });

    this.service.getPlatforms().subscribe(platforms => {
      this.platforms = platforms;
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onFilterChange() {
    if (this.filter.genres.length > 0 || this.filter.platforms.length > 0) {
      let games = this.games;
      games = games.filter(g => this.isMatch(g));
      this.dataSource.data = games;
    }
  }

  onGenresChange(event, genreId: number) {
    if (event.checked) {
      this.filter.genres.push(genreId);
    }
    else {
      const index = this.filter.genres.indexOf(genreId);
      this.filter.genres.splice(index, 1);
    }
  }

  onPlatformsChange(event, platformId: number) {
    if (event.checked) {
      this.filter.platforms.push(platformId);
    }
    else {
      const index = this.filter.platforms.indexOf(platformId);
      this.filter.platforms.splice(index, 1);
    }
  }

  resetFilter() {
    this.filter = {
      genres:[],
      platforms:[]
    }
    this.dataSource.data = this.games;
  }

  isMatch (game: any){

    for (let genre of game.genres) {
      for (let platform of game.platforms) {
        if ((this.filter.genres.indexOf(genre.id) !== -1 || this.filter.genres.length === 0)
          && (this.filter.platforms.indexOf(platform.id) !== -1 || this.filter.platforms.length === 0)) {
          return true;
        }
      }
    }

    return false;
  }

}


