import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ShopUser } from 'src/app/models/models';
import { DialogService } from 'src/app/services/dialog.service';
import { ToolService } from 'src/app/services/tool.service';

@Component({
  selector: 'friday-adjustuser',
  templateUrl: './adjustuser.component.html',
  styleUrls: ['./adjustuser.component.scss']
})
export class AdjustuserComponent implements OnInit {

  selectedUser: ShopUser
  users: Observable<ShopUser[]>
  form: FormGroup
  newVal: number = 0

  constructor(private tool: ToolService, private dialog: DialogService, fb: FormBuilder) {
    this.form = fb.group({
      users: fb.control("", Validators.required),
      amount: fb.control(""),
      passwords: fb.group({
        password: fb.control(""),
        passwordConfirm: fb.control("")
      }, { validator: this.passwordConfirming })
    })

    this.form.get("users").valueChanges.subscribe(s => {
      this.selectedUser = s
      this.form.get("amount").setValue(this.selectedUser.balance)
      this.form.updateValueAndValidity()
    })

    this.form.get("amount").valueChanges.subscribe(s => {
      this.newVal = +this.selectedUser.balance + +s
    })
  }

  ngOnInit(): void {
  }

  passwordConfirming(group: FormGroup): { invalid: boolean } {
    if (group.invalid)
      return { invalid: true }

    if (group.get("password").value !== group.get("passwordConfirm").value) {
      return { invalid: true };
    }
  }

  submit() {
    if (this.form.invalid)
      return

    const balance = this.form.get("amount").value
    const pass = this.form.get("passwords.password").value

    // Check if balance is changed
    if (balance !== this.selectedUser.balance) {
      this.tool.adjustUserBalance(balance).subscribe()
    }

    // Check if pass is changed
    if (!!pass) {
      this.tool.changePass(pass).subscribe()
    }
  }

}
