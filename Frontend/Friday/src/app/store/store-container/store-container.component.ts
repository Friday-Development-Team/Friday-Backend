import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'friday-store-container',
  templateUrl: './store-container.component.html',
  styleUrls: ['./store-container.component.scss']
})
export class StoreContainerComponent implements OnInit {

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

}
