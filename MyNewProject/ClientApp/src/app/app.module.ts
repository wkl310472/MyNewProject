import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { GameComponent } from './components/game/game.component';

import { GameService } from './services/game.service';
import { EditGameComponent } from './components/edit-game/edit-game.component';
import { NewGameComponent } from './components/new-game/new-game.component';
import { GameInfoComponent } from './components/game-info/game-info.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    GameComponent,
    EditGameComponent,
    NewGameComponent,
    GameInfoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'games', component: GameComponent },
      { path: 'games/edit/:id', component: EditGameComponent },
      { path: 'games/new', component: NewGameComponent },
      { path: 'games/info/:id', component: GameInfoComponent }
    ])
  ],
  providers: [GameService],
  bootstrap: [AppComponent]
})
export class AppModule { }
