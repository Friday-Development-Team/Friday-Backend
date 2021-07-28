/**
 * File used to store models containing only data or very simple methods
 */

export class Item {
    constructor(public id: number, public name: string, public price: number, public type: string,
                public count: number, public itemDetails: ItemDetails, public logs: null, public normalizedImageName: string) { }
}

export class ItemDetails {
    constructor(public id: number, public itemId: number, public size: string, public calories: number,
                public sugarContent: number, public saltContent: number, public allergens: number) { }
}

export class Cart {
    total = 0
    items: OrderItem[] = []


    updateTotal() {
        this.total = this.items.map(s => +s.item.price * +s.amount).reduce((acc, cur) => acc + cur, 0)
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

    remove(id: number, count: number) {
        if (!this.has(id)) return false
        const item = this.items.find(s => s.item.id === id)
        item.amount -= count
        if (!!!item.amount)
            this.items = this.items.filter(s => s !== item)
        return true
    }

    clear() {
        this.items = []
        this.updateTotal()
    }
}

export class OrderItem {
    constructor(public item: Item, public amount: number) { }

    addAmount(additional: number) {
        if (additional < 0 && additional > this.amount) return false
        this.amount = +this.amount + +additional
        return true
    }

    getCost() {
        return +this.item.price * +this.amount
    }
}

export class OrderDTO {
    constructor(public items: OrderItemDTO[]) { }
}

export class OrderItemDTO {
    constructor(public id: number, public amount: number) { }
}

export class CateringOrder {
    constructor(
        public id: number,
        public items: HistoryOrderItem[], public user: string,
        public statusFood: string,
        public statusBeverage: string,
        public orderTime: Date,
        public totalPrice: number) { }

    isActive(): boolean {
        const bool = this.statusBeverage === 'Accepted' || this.statusBeverage === 'Accepted' || this.statusFood === 'SentToKitchen'
        console.log(bool)
        return bool
    }
}

export class OrderHistory {
    constructor(public orders?: HistoryOrder[], public username?: string) { }

    fromJson(json: any) {
        if (!json)
            return
        this.username = json.userName
        this.orders = json.orders.map(s => {
            const temp = new HistoryOrder()
            temp.fromJson(s)
            return s
        })

    }
}

export class HistoryOrder {

    constructor(public totalPrice?: number, public orderTime?: Date, public completionTimeFood?: Date,
                public completionTimeBeverage?: Date, public items?: HistoryOrderItem[]) { }

    fromJson(json: any) {
        this.totalPrice = json.totalPrice
        this.orderTime = json.orderTime
        this.completionTimeFood = json.completionTimeFood
        this.completionTimeBeverage = json.completionTimeBeverage
        this.items = json.items.map(s => {
            const item = new HistoryOrderItem()
            item.fromJson(s)
            return item
        })
    }
}
export class HistoryOrderItem {
    fromJson(json: any) {
        this.itemName = json.itemName
        this.amount = json.amount
    }
    constructor(public itemName?: string, public amount?: number) { }
}

export class ShopUser {
    constructor(public name: string, public balance: number) { }
}

export class Configuration {
    constructor(public combinedCateringKitchen: boolean, public usersSetSpot: boolean, public cancelOnAccepted: boolean) { }
}