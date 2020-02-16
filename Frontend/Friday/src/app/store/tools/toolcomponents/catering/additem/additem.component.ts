import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/models/models';
import { AdminService } from 'src/app/services/admin.service';

@Component({
  selector: 'friday-additem',
  templateUrl: './additem.component.html',
  styleUrls: ['./additem.component.scss']
})
export class AdditemComponent implements OnInit {

  item: Item

  constructor(private admin: AdminService) { }

  ngOnInit() {
  }

  

}
