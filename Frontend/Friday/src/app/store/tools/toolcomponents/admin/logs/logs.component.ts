import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Log } from 'src/app/models/models';

@Component({
  selector: 'friday-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit {

  logtypes: LogType[]
  selected: LogType

  data: number | Log[]

  form: FormGroup

  constructor(public admin: AdminService, public fb: FormBuilder) {
    this.logtypes = this.initLogs()
    this.form = fb.group({
      selection: fb.control('', Validators.required),
      input: fb.control('')
    })

    this.form.get('selection').valueChanges.subscribe(s => {
      this.selected = s
      this.form.get('input').setValidators((this.selected.needInput ? [Validators.required] : null))
    }

    )

  }

  ngOnInit() {
  }

  initLogs(): LogType[] {
    return [
      new LogType('All Currency Logs', 'list', false, 'log', 'currency/all'),
      new LogType('All Item Logs', 'list', false, 'log', 'item/all'),
      new LogType('Currency Logs By User', 'list', true, 'log', 'currency/user'),
      new LogType('Item Logs By Item', 'list', true, 'log', 'item/id'),
      new LogType('Remaining Stock', 'list', false, 'itemamount', 'stock/remaining'),
      new LogType('Sold Items', 'list', false, 'itemamount', 'stock/sold'),
      new LogType('Total Income', 'single', false, 'amount', 'total')
    ]
  }

  submit() {
    if (!!!this.selected)
      return
    let param = null
    if (this.selected.needInput) {
      param = this.form.get('input').value
      if (!!!param)
        return
    }

    switch (this.selected.type.toLowerCase()) {
      case 'log':

        this.admin.getLogs(this.selected.route).subscribe(s => this.data = s)
      case 'itemamount':
        break
      case 'amount':
        break
    }
  }

}

class LogType {
  constructor(public display: string, public displaytype: string, public needInput: boolean, public type: string, public route: string) { }
}
