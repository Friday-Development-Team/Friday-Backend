/**
 * File used to store models containing only data or very simple methods
 */

export class Item {
    constructor(public id: number, public name: string, public price: number, public type: string, public count: number, public itemDetails: ItemDetails, public logs: null, public normalizedImageName: string) { }
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

    has(id: number): boolean {
        return this.items.some(s => s.item.id === id)
    }

    add(item: OrderItem) {
        if (!this.has(item.item.id)) this.items.push(item)
        else
            this.items.find(s => s.item.id === item.item.id).addAmount(item.amount)
        this.updateTotal()
    }
}

export class OrderItem {
    constructor(public item: Item, public amount: number) { }

    addAmount(amount: number) {
        if (amount < 0 && amount > this.amount) return false
        this.amount += amount
        return true
    }
}
