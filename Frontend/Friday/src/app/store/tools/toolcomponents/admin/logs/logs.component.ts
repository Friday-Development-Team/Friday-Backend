import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Log, ItemAmount } from 'src/app/models/models';
import { interval, Observable, of } from 'rxjs';

@Component({
  selector: 'friday-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit {

  logtypes: LogType[]
  selected: LogType
  needsInput: boolean = false

  selectedLogType: string | undefined

  hasSelectedLog: boolean = false//set to true when in subscribe method to avoid null errors in html

  data: Observable<Log[] | ItemAmount[] | number>

  form: FormGroup

  constructor(public admin: AdminService, public fb: FormBuilder) {
    this.logtypes = this.initLogs()
    this.selected = this.logtypes[0]
    this.form = fb.group({
      selection: fb.control('', Validators.required),
      input: fb.control('')
    })

    this.form.get('selection').valueChanges.subscribe(s => {
      this.selected = s
      this.needsInput = s.needInput
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
    console.log('has selected')
    let param = null
    if (this.selected.needInput) {
      param = this.form.get('input').value
      if (!!!param)
        return
    }
    console.log('input checked')
    this.hasSelectedLog = true
    let temp = new Log('temp', 2, null, null)
    this.data = of([temp, temp, temp, temp, temp])


    switch (this.selected.type.toLowerCase()) {
      case 'log':

        this.data = this.admin.getLogs(this.selected.route)
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
