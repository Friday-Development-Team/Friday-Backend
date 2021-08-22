import { CurrencyPipe } from '@angular/common'
import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Observable } from 'rxjs'
import { ShopUser } from 'src/app/models/models'
import { DataService } from 'src/app/services/data.service'
import { DialogService } from 'src/app/services/dialog.service'
import { SpinnerService } from 'src/app/services/spinner.service'
import { ToolService } from 'src/app/services/tool.service'

@Component({
  selector: 'friday-adjustuser',
  templateUrl: './adjustuser.component.html',
  styleUrls: ['./adjustuser.component.scss'],
})
export class AdjustuserComponent implements OnInit {
  selectedUser: ShopUser
  users: Observable<ShopUser[]>
  form: FormGroup
  newVal = 0

  constructor(
    private data: DataService,
    private tool: ToolService,
    private spinner: SpinnerService,
    private dialog: DialogService,
    private currencyPipe: CurrencyPipe,
    fb: FormBuilder,
  ) {
    this.users = this.data.getUsers()
    this.form = fb.group({
      users: fb.control('', Validators.required),
      currentbalance: fb.control(null),
      amount: fb.control(''),
      // passwords: fb.group(
      //   {
      //     password: fb.control(''),
      //     passwordConfirm: fb.control(''),
      //   },
      //   { validator: this.passwordConfirming }
      // ),
    })

    this.form.get('users').valueChanges.subscribe((s) => {
      this.selectedUser = s
      this.form.get('currentbalance').setValue(this.currencyPipe.transform(this.selectedUser.balance, 'EUR'))
      this.form.updateValueAndValidity()
    })

    this.form.get('amount').valueChanges.subscribe((s) => {
      this.newVal = +this.selectedUser.balance + +s
    })
  }

  ngOnInit(): void { }

  // passwordConfirming(group: FormGroup): { invalid: boolean } {
  //   if (group.invalid) return { invalid: true }

  //   if (group.get('password').value !== group.get('passwordConfirm').value) {
  //     return { invalid: true }
  //   }
  // }

  submit(): void {
    if (this.form.invalid) return

    const balance = this.form.get('amount').value
    const name = this.selectedUser.name

    const balanceChanged = balance !== this.selectedUser.balance

    let balanceChangeDone = true

    if (balanceChanged) {

      this.spinner.startSpinner()

      // Check if balance is changed
      if (balanceChanged) {
        balanceChangeDone = false
        this.tool.adjustUserBalance(name, balance).subscribe(() => {
          balanceChangeDone = true
          if (balanceChangeDone) this.spinner.stopSpinner(0)
        })
      }

    }
  }

  canSubmit(): boolean {
    return this.form.get('amount').dirty && this.form.valid
  }

}
