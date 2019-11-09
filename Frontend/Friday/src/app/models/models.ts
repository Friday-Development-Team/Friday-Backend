export class Item {
    constructor(public id: number, public name: string, public price: number, public type: string, public count: number, public itemDetails: ItemDetails, public logs: null) { }
}

export class ItemDetails {
    constructor(public id: number, public itemId: number, public size: string, public calories: number, public sugarContent: number, public saltContent: number, public allergens: number) { }
}

export class Cart {
    total: number = 0
    items: OrderItem[] = []

    updateTotal() {
        this.total = this.items.map(s => { return s.item.price * s.amount }).reduce((acc, cur) => acc + cur)
    }
}

export class OrderItem {
    constructor(public item: Item, public amount: number) { }
}