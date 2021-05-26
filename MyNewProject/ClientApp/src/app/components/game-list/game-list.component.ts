import { Component, OnInit, ViewChild } from '@angular/core';
import { GameService } from '../../services/game.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { FormControl } from '@angular/forms';

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
    genreId: [],
    platformId: []
  };

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  tooltipShowDelay = new FormControl(500);

  pageNumber: number;
  pageNumbers: number[];


  constructor(private service: GameService) { }

  ngOnInit() {
    this.service.getGames(this.filter).subscribe(games => {
      this.games = games;
      this.dataSource = new MatTableDataSource(this.games);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.paginator.length = this.dataSource.data.length;
      this.updatePageNumbers();
    });

    this.service.getGenres().subscribe(genres => {
      this.genres = genres;
    });

    this.service.getPlatforms().subscribe(platforms => {
      this.platforms = platforms;
    });

  }

  applySearch(searchValue: string) {
    this.dataSource.filter = searchValue.trim().toLowerCase();
  }

  private populateGames() {
    this.service.getGames(this.filter).subscribe(games => {
      this.games = games;
      this.dataSource.data = this.games;
    });
  }

  onFilterChange() {
    this.populateGames();
    this.opened = false;
  }

  onGenresChange(event, genreId: number) {
    if (event.checked) {
      this.filter.genreId.push(genreId);
    }
    else {
      const index = this.filter.genreId.indexOf(genreId);
      this.filter.genreId.splice(index, 1);
    }
  }

  onPlatformsChange(event, platformId: number) {
    if (event.checked) {
      this.filter.platformId.push(platformId);
    }
    else {
      const index = this.filter.platformId.indexOf(platformId);
      this.filter.platformId.splice(index, 1);
    }
  }

  resetFilter() {
    if (this.filter.genreId.length > 0 || this.filter.platformId.length > 0) {

      this.opened = true;
    }
    else {
      this.opened = false;
    }
    this.filter = {
      genreId: [],
      platformId: []
    }
    this.populateGames();
  }

  goToPage() {
    this.paginator.pageIndex = this.pageNumber - 1;
    this.paginator.page.next({
      length: this.paginator.length,
      pageIndex: this.paginator.pageIndex,
      pageSize: this.paginator.pageSize
    });
  }

  updatePageNumbers() {
    this.pageNumber = (this.paginator.pageIndex || 0) + 1;
    this.pageNumbers = [];
    for (let i = 1; i <= Math.ceil(this.paginator.length / this.paginator.pageSize); i++) {
      this.pageNumbers.push(i);
    }
  }

}


