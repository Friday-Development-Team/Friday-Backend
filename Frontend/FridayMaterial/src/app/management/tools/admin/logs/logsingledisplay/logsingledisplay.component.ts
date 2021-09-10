import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'friday-logsingledisplay',
  templateUrl: './logsingledisplay.component.html',
  styleUrls: ['./logsingledisplay.component.scss']
})
export class LogsingledisplayComponent implements OnInit {

  @Input() data: number
  @Input() datatype: 'currency' | 'not-labeled'

  constructor() { }

  ngOnInit(): void {
  }

}
