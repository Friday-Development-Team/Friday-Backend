import { Component, OnInit } from '@angular/core';
import { RefService } from 'src/app/services/ref.service';
import { DataService } from 'src/app/services/data.service';
import { CateringOrder } from 'src/app/models/models';

@Component({
  selector: 'friday-running',
  templateUrl: './running.component.html',
  styleUrls: ['./running.component.scss']
})
export class RunningComponent implements OnInit {

  private readonly ref: string = 'running'

  orders: CateringOrder[]=[]

  constructor(private refService: RefService, private data: DataService) {
    this.refService.sendRef(this.ref)

    this.data.getRunning().subscribe(s => {
      console.log(s)
      this.orders = s
    })
  }

  ngOnInit() {
  }

}
