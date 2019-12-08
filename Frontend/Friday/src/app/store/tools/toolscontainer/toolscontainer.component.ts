import { Component, OnInit } from '@angular/core';
import { RefService } from 'src/app/services/ref.service';

@Component({
  selector: 'friday-toolscontainer',
  templateUrl: './toolscontainer.component.html',
  styleUrls: ['./toolscontainer.component.scss']
})
export class ToolscontainerComponent implements OnInit {

  private readonly ref: string = 'tools'

  constructor(private refService: RefService) {
    this.refService.sendRef(this.ref)
  }

  ngOnInit() {

  }

}
