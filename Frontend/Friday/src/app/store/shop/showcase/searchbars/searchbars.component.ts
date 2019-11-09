import { Component, OnInit } from '@angular/core';
import { FilterService } from 'src/app/services/filter.service';

@Component({
  selector: 'friday-searchbars',
  templateUrl: './searchbars.component.html',
  styleUrls: ['./searchbars.component.scss']
})
export class SearchbarsComponent implements OnInit {

  constructor(private filter: FilterService) { }

  ngOnInit() {
  }

  searchName(event: any){
    console.log(event.target.value)
    this.filter.searchName(event.target.value)
  }

}
