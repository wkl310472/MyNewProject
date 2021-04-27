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
  displayedColumns: string[] = ['name', 'developer', 'release', 'action'];
  dataSource;

  constructor(private service: GameService) { }

  ngOnInit() {
    this.service.getAllGames().subscribe(games => {
      this.games = games;
      this.dataSource = new MatTableDataSource(this.games);
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}

