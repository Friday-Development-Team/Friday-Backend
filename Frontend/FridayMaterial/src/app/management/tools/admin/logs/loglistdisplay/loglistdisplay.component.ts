import { Component, Input, OnChanges, AfterViewInit, SimpleChanges, ViewChild } from '@angular/core'
import { MatTable } from '@angular/material/table'
import { Log, ItemAmount } from 'src/app/models/models'

@Component({
  selector: 'friday-loglistdisplay',
  templateUrl: './loglistdisplay.component.html',
  styleUrls: ['./loglistdisplay.component.scss']
})
export class LogListDisplayComponent implements AfterViewInit {

  @Input() columns: string[]
  @Input() logtype: 'log' | 'itemamount'
  @Input() data: Log[] | ItemAmount[]

  @ViewChild(MatTable) table: MatTable<any>

  constructor() { }

  ngAfterViewInit(): void {
    this.table?.renderRows()
  }

}
