export class Item {
    constructor(public id: number, public name: string, public price: number, public type: string, public count: number, public itemDetails: ItemDetails, public logs: null) { }
}

export class ItemDetails {
    constructor(public id: number, public itemId: number, public size: string, public calories: number, public sugarContent: number, public saltContent: number, public allergens: number) { }
}
