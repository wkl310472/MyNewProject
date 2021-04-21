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

  constructor(private route: ActivatedRoute, private router: Router, private service: GameService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.service.get(params.get('id')).subscribe(game => {
        this.game = game;
      })
    });
  }


  navigateBack() {
    this.router.navigate(['/games']);
  }


}
