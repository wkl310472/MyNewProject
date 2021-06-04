import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { EffectsModule } from '@ngrx/effects';
import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { GameFormComponent } from './components/game-form/game-form.component';
import { GameInfoComponent } from './components/game-info/game-info.component';
import { GameListComponent } from './components/game-list/game-list.component';
import { HomeComponent } from './components/home/home.component';
import { LoadingSpinnerComponent } from './components/shared/loading-spinner/loading-spinner.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';

import { AuthService } from './services/auth.service';
import { GameService } from './services/game.service';
import { PhotoService } from './services/photo.service';

import { AppErrorHandler } from './app.error-handler';
import { LoginComponent } from './components/login/login.component';
import { environment } from '../environments/environment.prod';
import { appReducer } from './store/app.store';
import { AuthEffects } from './store/auth/auth.store';
import { GameEffects, gameReducer } from './store/entities/games.store';
import { GenreEffects } from './store/entities/genres.store';
import { PlatformEffects } from './store/entities/platforms.store';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    GameInfoComponent,
    GameFormComponent,
    GameListComponent,
    LoginComponent,
    LoadingSpinnerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    CommonModule,
    EffectsModule.forRoot([AuthEffects, GameEffects, GenreEffects, PlatformEffects]),
    HttpClientModule,
    FormsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatDividerModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatListModule,
    MatMomentDateModule,
    MatPaginatorModule,
    MatSelectModule,
    MatSidenavModule,
    MatProgressSpinnerModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatTooltipModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'games', component: GameListComponent },
      { path: 'games/new', component: GameFormComponent },
      { path: 'games/edit/:id', component: GameFormComponent },
      { path: 'games/info/:id', component: GameInfoComponent },
      { path: 'users/login', component: LoginComponent },
      { path: 'users/register', component: LoginComponent },
      { path: '**', redirectTo: '' },
    ]),
    StoreModule.forRoot(appReducer),
    StoreDevtoolsModule.instrument({
      maxAge: 100,
      logOnly: environment.production
    }),
    ToastrModule.forRoot()
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
    GameService,
    PhotoService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
