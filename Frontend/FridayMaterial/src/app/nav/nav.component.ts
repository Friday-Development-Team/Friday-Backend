import { Component, OnInit } from '@angular/core'
import { AuthService } from '../services/auth.service'
import { NavService } from '../services/nav.service'

@Component({
  selector: 'friday-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  currentPage = ''
  readonly urlBase: string = '/main/'


  readonly navs: NavElement[] = [
    { id: 'shop', url: this.urlBase + 'store/shop/', display: 'Shop' },
    { id: 'history', url: this.urlBase + 'store/history/', display: 'Order history' },
    { id: 'running', url: this.urlBase + 'store/running/', display: 'Running orders' },
    { id: 'manage', url: this.urlBase + 'manage', display: 'Management' },
  ]

  constructor(private navService: NavService, private auth: AuthService) {
    this.navService.getCurrentNavPage().subscribe(s => this.currentPage = s)
  }

  ngOnInit(): void {
  }

  logout(): void {
    this.auth.logout()
  }

}


class NavElement {
  /**
   *
   */
  constructor(public id: string, public url: string, public display: string) { }
}
