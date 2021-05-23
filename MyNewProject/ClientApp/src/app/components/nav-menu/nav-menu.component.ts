import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  loginExpanded = false;

  isLoggedIn: boolean;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  toggleLogin() {
    this.loginExpanded = !this.loginExpanded;
  }

  changeLoginStatus() {
    this.isLoggedIn = !this.isLoggedIn;
  }

}
