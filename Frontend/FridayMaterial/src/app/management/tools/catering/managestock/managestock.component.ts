import { Component } from '@angular/core'
import { AbstractControl, ValidatorFn, Validators } from '@angular/forms'
import { FormBuilder, FormGroup } from '@angular/forms'
import { Observable } from 'rxjs'
import { ItemAmountChange, ItemAmountChangeRequest } from 'src/app/models/models'
import { ToolService } from 'src/app/services/tool.service'

@Component({
  selector: 'friday-managestock',
  templateUrl: './managestock.component.html',
  styleUrls: ['./managestock.component.scss']
})
export class ManagestockComponent {

  form: FormGroup
  items: Observable<ItemAmountChange[]>
  selectedItem: ItemAmountChange

  constructor(private tool: ToolService, fb: FormBuilder) {

    this.items = this.tool.getCurrentStock()

    this.form = fb.group({
      items: fb.control('', Validators.required),
      currentAmount: fb.control(''),
      amount: fb.control(0, this.requiredNonZeroValidator()),
      projected: fb.control(0)
    })

    this.form.get('items').valueChanges.subscribe(s => {
      this.selectedItem = s
      this.form.get('currentAmount').setValue(this.selectedItem.amount)
      this.reset(this.form.get('amount'), 0)
    })

    this.form.get('amount').valueChanges.subscribe(s => {
      if (!this.fieldsValid()) {

        this.reset(this.form.get('projected'), 0)
        return
      }
      this.form.get('projected').setValue(+this.selectedItem.amount + +this.form.get('amount').value)
    })
  }

  submit(): void {
    if (this.form.invalid) return
    const request: ItemAmountChangeRequest = { id: this.selectedItem.id, amount: this.form.get('amount').value }
    this.tool.adjustStock(request).subscribe(s => {
      // TODO Show spinner and refresh data
    })
  }

  requiredNonZeroValidator(): ValidatorFn {
    return (control: AbstractControl) => {
      return !!control.value ? null : { nonZeroValue: false } // Only allow non falsy values
    }
  }

  reset(control: AbstractControl, value: any): void {
    control.setValue(value)
    control.markAsPristine()
    control.markAsUntouched()
  }

  fieldsValid() {
    return this.form.get('items').valid && this.form.get('amount').valid
  }

  giveErrorStyle() {
    return this.form.get('projected').value < 0
  }

}
