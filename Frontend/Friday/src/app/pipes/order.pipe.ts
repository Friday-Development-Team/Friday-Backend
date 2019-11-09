import { Pipe, PipeTransform } from '@angular/core';
import { Item } from '../models/models';

@Pipe({
  name: 'order'
})
export class OrderPipe implements PipeTransform {

  transform(value: Item[], args?: string): any {
    if (!args)
      return value

    switch (args) {
      case "Name":
        return value.sort((a, b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0))
      case "Price: low to high":
        return value.sort(s => s.price)
      case "Price: high to low":
        return value.sort(s => s.price).reverse()
      case "Calories: low to high":
        return value.sort(s => s.itemDetails.calories)
      case "Calories: high to low":
        return value.sort(s => s.itemDetails.calories).reverse()
      case "Type":
        return value.sort((a, b) => (a.type > b.type) ? 1 : ((b.type > a.type) ? -1 : 0))
      default:
        return value;
    }
  }

}
