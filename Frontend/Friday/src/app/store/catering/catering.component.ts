import { Component } from '@angular/core';
import { ConfigService } from 'src/app/services/config.service';
import { CateringOrder } from 'src/app/models/models';
import { DataService } from 'src/app/services/data.service';
import { CateringService } from 'src/app/services/catering.service';
import { RefService } from 'src/app/services/ref.service';

@Component({
  selector: 'friday-catering',
  templateUrl: './catering.component.html',
  styleUrls: ['./catering.component.scss']
})
export class CateringComponent {

  private readonly ref: string = 'catering'

  combined: boolean = false

  orders: CateringOrder[] = []

  constructor(private config: ConfigService, private refS: RefService, private catering: CateringService) {
    this.refS.sendRef(this.ref)//Send reference

    this.combined = this.config.config.combinedCateringKitchen
    this.catering.getCateringOrders(false).subscribe(s => this.orders = s)
  }

  setActive(i: number) {
    let order = this.orders[i]
    if (!!order.statusbeverage)
      this.orders[i].statusbeverage = "Accepted"

  }

}
