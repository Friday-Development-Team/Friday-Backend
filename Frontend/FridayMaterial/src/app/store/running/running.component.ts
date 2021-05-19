import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CateringOrder } from 'src/app/models/models';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'friday-running',
  templateUrl: './running.component.html',
  styleUrls: ['./running.component.scss']
})
export class RunningComponent implements OnInit {

  private orders: Observable<CateringOrder[]>

  constructor(private data: DataService) {
    
  }

  ngOnInit(): void {
  }

}
