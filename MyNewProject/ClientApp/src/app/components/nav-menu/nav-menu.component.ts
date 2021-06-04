import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { IAppState } from '../../store/app.store';
import { loggedOut, selectIsAuthenticated } from '../../store/auth/auth.store';
import { selectShowLogin, loginPageActivated } from '../../store/ui/ui.store';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  loginExpanded : Observable<boolean>;

  isExpanded = false;

  isLoggedIn: Observable<boolean>;

  constructor(private store: Store<IAppState>) { }

  ngOnInit() {
    this.loginExpanded = this.store.select(selectShowLogin);
    this.isLoggedIn = this.store.select(selectIsAuthenticated);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  showLoginPage() {
    this.store.dispatch(loginPageActivated());
  }

  logout() {
    this.store.dispatch(loggedOut());
  }

}
