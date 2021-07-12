import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { IAppState } from './store/app.store';
import { autoLoginStarted } from './store/auth/auth.store';
import { selectLoading } from './store/ui/ui.store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  isLoading$: Observable<boolean>;

  constructor(private store: Store<IAppState>) { }

  ngOnInit() {
    this.isLoading$ = this.store.select(selectLoading);
    this.store.dispatch(autoLoginStarted());
  }
}
