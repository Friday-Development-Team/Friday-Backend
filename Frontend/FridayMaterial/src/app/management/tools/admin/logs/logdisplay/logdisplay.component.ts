import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core'
import { Log, ItemAmount } from 'src/app/models/models'

@Component({
  selector: 'friday-logdisplay',
  templateUrl: './logdisplay.component.html',
  styleUrls: ['./logdisplay.component.scss']
})
export class LogdisplayComponent implements OnInit, OnChanges {

  @Input() displaytype: 'list' | 'single' | '' = 'single'
  @Input() data: Log[] | ItemAmount[] | number

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes.data.currentValue)
  }

  ngOnInit(): void {
  }

}
