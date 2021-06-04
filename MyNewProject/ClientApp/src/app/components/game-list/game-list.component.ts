import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { FormControl } from '@angular/forms';
import { State, Store } from '@ngrx/store';
import { IAppState } from '../../store/app.store';
import { filterReset, filterSideNavClosed, filterSideNavOpened, genreFilterAdded, genreFilterRemoved, IFilter, loadingStarted, platformFilterAdded, platformFilterRemoved, selectFilter, selectShowFilter, selectPagination, IPagination, paginationChanged } from '../../store/ui/ui.store';
import { Observable, Subscription } from 'rxjs';
import { gamesLoadingStarted, IGame, selectGameList } from '../../store/entities/games.store';
import { genresLoadingStarted, selectGenreList } from '../../store/entities/genres.store';
import { platformsLoadingStarted, selectPlatformList } from '../../store/entities/platforms.store';
import { IKeyValuePair } from '../../store/entities/shared';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit, OnDestroy {

  games$: Observable<IGame[]>;
  genres$: Observable<IKeyValuePair[]>;
  platforms$: Observable<IKeyValuePair[]>;
  displayedColumns: string[] = ['name', 'developer', 'release', 'action'];
  dataSource = new MatTableDataSource<IGame>();
  showFilter$: Observable<boolean>;
  filter$: Observable<IFilter>;
  pagination$: Observable<IPagination>;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  tooltipShowDelay = new FormControl(500);

  currentPage: number;
  pageNumbers: number[];

  private gamesSubscription: Subscription;
  private filterSubscription: Subscription;
  private paginationSubscription: Subscription;

  constructor(private store: Store<IAppState>) { }

  ngOnInit() {
    this.games$ = this.store.select(selectGameList);
    this.genres$ = this.store.select(selectGenreList);
    this.platforms$ = this.store.select(selectPlatformList);
    this.filter$ = this.store.select(selectFilter);
    this.showFilter$ = this.store.select(selectShowFilter);
    this.pagination$ = this.store.select(selectPagination);

    this.store.dispatch(genresLoadingStarted());
    this.store.dispatch(platformsLoadingStarted());

    this.filterSubscription = this.filter$.subscribe((filter) => {    
      this.store.dispatch(gamesLoadingStarted({ payload: filter }));
    });

    this.gamesSubscription = this.games$.subscribe((data) => {
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.paginator.length = data.length;
      this.dataSource.data = data;

      this.paginationSubscription = this.pagination$.pipe(take(1)).subscribe((pagination) => {
        console.log(pagination);
        this.updatePageNumbers(pagination);
        this.goToPage({ value: pagination.currentPage });
      });
    });
  }

  ngOnDestroy() {
    this.gamesSubscription.unsubscribe();
    this.filterSubscription.unsubscribe();
    this.paginationSubscription.unsubscribe();
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
  }

  onPlatformsChange(event, platformId: number) {
    if (event.checked) {
      this.store.dispatch(platformFilterAdded({ payload: platformId }));
    }
    else {
      this.store.dispatch(platformFilterRemoved({ payload: platformId }));
    }
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

    this.currentPage = event.pageIndex !== undefined ? event.pageIndex + 1 : event.currentPage;

    this.store.dispatch(paginationChanged({ payload: { currentPage: this.currentPage, pageSize: event.pageSize } }));

    this.paginator.pageSize = event.pageSize;

    this.paginator.pageIndex = this.currentPage - 1;

    this.pageNumbers = [];
    for (let i = 1; i <= Math.ceil(this.paginator.length / this.paginator.pageSize); i++) {
      this.pageNumbers.push(i);
    }
  }

}


