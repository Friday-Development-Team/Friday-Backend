import { Component, OnInit } from '@angular/core'
import { AbstractControl, Form, FormBuilder, FormGroup, Validators } from '@angular/forms'
import { MatDialog } from '@angular/material/dialog'
import { DialogService } from 'src/app/services/dialog.service'
import { SpinnerService } from 'src/app/services/spinner.service'
import { ToolService } from 'src/app/services/tool.service'
import { MessageDialogComponent } from 'src/app/shared/messagedialog/messagedialog.component'

@Component({
  selector: 'friday-adduser',
  templateUrl: './adduser.component.html',
  styleUrls: ['./adduser.component.scss']
})
export class AdduserComponent implements OnInit {

  form: FormGroup

  constructor(private tool: ToolService, private dialog: DialogService, private spinner: SpinnerService, fb: FormBuilder) {
    this.form = fb.group({
      username: fb.control('', [Validators.required, Validators.email]),
      passwords: fb.group({
        password: fb.control('', [Validators.required]),
        passwordConfirm: fb.control('', [Validators.required])
      }, { validator: this.passwordConfirming })
    })
  }

  ngOnInit(): void {
  }

  addUser(): void {
    if (this.form.invalid || this.form.get('password').invalid)
      return

    const username = this.form.get('username').value
    const pass = this.form.get('passwords').get('password').value

    this.spinner.startSpinner()

    this.tool.addUser(username, pass).subscribe(s => {
      this.spinner.stopSpinner(0)
      this.dialog.openTimedDialog(7000, `User with email '${username}' was succesfully added to the system!`)
    },
      err => {
        this.spinner.stopSpinner(0)
        this.dialog.displayErrorMessage(`An error has occurred: ${err}. Contact the admin or try again.`)
      },
    )
  }

  passwordConfirming(group: FormGroup): { invalid: boolean } {
    if (group.invalid)
      return { invalid: true }

    if (group.get('password').value !== group.get('passwordConfirm').value) {
      return { invalid: true }
    }
  }

}
