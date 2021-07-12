import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { FormControl } from '@angular/forms';
import { State, Store } from '@ngrx/store';
import { IAppState } from '../../store/app.store';
import { filterReset, filterSideNavClosed, filterSideNavOpened, genreFilterAdded, genreFilterRemoved, IFilter, loadingStarted, platformFilterAdded, platformFilterRemoved, selectFilter, selectShowFilter, selectPagination, IPagination, paginationChanged, selectLoading } from '../../store/ui/ui.store';
import { Observable, Subscription } from 'rxjs';
import { gamesLoadingStarted, IGame, selectGameList } from '../../store/entities/games.store';
import { genresLoadingStarted, selectGenreList } from '../../store/entities/genres.store';
import { platformsLoadingStarted, selectPlatformList } from '../../store/entities/platforms.store';
import { IKeyValuePair } from '../../store/entities/shared';
import { switchMap, take,map } from 'rxjs/operators';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit, OnDestroy, AfterViewInit {

  games$: Observable<IGame[]>;
  genres$: Observable<IKeyValuePair[]>;
  platforms$: Observable<IKeyValuePair[]>;
  displayedColumns: string[] = ['name', 'developer', 'release', 'action'];
  dataSource$: Observable<MatTableDataSource<IGame>>;
  dataSource: MatTableDataSource<IGame>;
  showFilter$: Observable<boolean>;
  filter$: Observable<IFilter>;
  pagination$: Observable<IPagination>;
  pageNumbers$: Observable<number[]>;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  tooltipShowDelay = new FormControl(500);

  

  private filterSubscription: Subscription;
  private gamesSubscription: Subscription;

  constructor(private store: Store<IAppState>) { }

  ngOnInit() {
    this.games$ = this.store.select(selectGameList);
    this.genres$ = this.store.select(selectGenreList);
    this.platforms$ = this.store.select(selectPlatformList);
    this.filter$ = this.store.select(selectFilter);
    this.showFilter$ = this.store.select(selectShowFilter);
    this.pagination$ = this.store.select(selectPagination);

    this.dataSource = new MatTableDataSource<IGame>();
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;

    setTimeout(() => { this.store.dispatch(loadingStarted()); });

    this.store.dispatch(genresLoadingStarted());
    this.store.dispatch(platformsLoadingStarted());

    

    this.filterSubscription = this.filter$.subscribe((filter) => {
      this.store.dispatch(gamesLoadingStarted({ payload: filter }));
    });

    this.gamesSubscription = this.games$.subscribe((games) => {
      this.dataSource.data = games;
    });

    this.pageNumbers$ = this.games$.pipe(switchMap(games => {
      return this.pagination$.pipe(map(pagination => {
        const pageNumbers = [];
        for (let i = 1; i <= Math.ceil(games.length / pagination.pageSize); i++) {
          pageNumbers.push(i);
        }
        return pageNumbers;
      }));
    }));
  }

  ngAfterViewInit() {

    setTimeout(() => {
      this.pagination$.pipe(take(1)).subscribe(pagination => {
        this.goToPage({ value: pagination.pageIndex + 1 });
      });
    });
  }

  ngOnDestroy() {
    this.filterSubscription.unsubscribe();
    this.gamesSubscription.unsubscribe();
  }

  applySearch(searchValue: string) {
    this.dataSource.filter = searchValue.trim().toLowerCase();
  }

  confirmFilter() {
    this.store.dispatch(filterSideNavClosed());
  }

  onGenresChange(event, genreId: number) {
    if (event.checked) {
      this.store.dispatch(genreFilterAdded({ payload: genreId }));
    }
    else {
      this.store.dispatch(genreFilterRemoved({ payload: genreId }));
    }
    this.goToPage({ value: 1 });
  }

  onPlatformsChange(event, platformId: number) {
    if (event.checked) {
      this.store.dispatch(platformFilterAdded({ payload: platformId }));
    }
    else {
      this.store.dispatch(platformFilterRemoved({ payload: platformId }));
    }
    this.goToPage({ value: 1 });
  }

  toggleFilter(sidenav) {
    if (sidenav['opened']) {
      this.store.dispatch(filterSideNavClosed());
    }
    else {
      this.store.dispatch(filterSideNavOpened());
    }
  }

  resetFilter() {
    this.store.dispatch(filterReset());
  }

  goToPage(event) {
    this.paginator.pageIndex = event.value - 1;
    this.paginator.page.next({
      length: this.paginator.length,
      pageIndex: this.paginator.pageIndex,
      pageSize: this.paginator.pageSize
    });
  }

  updatePageNumbers(event) {
    this.store.dispatch(paginationChanged({ payload: { length: event.length, pageSize: event.pageSize, pageIndex: event.pageIndex } }));
  }

}


