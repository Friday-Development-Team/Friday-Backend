import { Component, OnInit } from '@angular/core';
import { UserService, ShopUser } from 'src/app/services/user.service';

@Component({
  selector: 'friday-store-container',
  templateUrl: './store-container.component.html',
  styleUrls: ['./store-container.component.scss']
})
export class StoreContainerComponent implements OnInit {

  currentUser: ShopUser

  constructor(private userService: UserService) {
    this.userService.user.subscribe(s => {

      this.currentUser = s
    })
    this.userService.startUserPolling()
  }

  ngOnInit() {
  }

}
