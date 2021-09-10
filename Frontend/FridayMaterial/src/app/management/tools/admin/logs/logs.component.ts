import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { BehaviorSubject, Observable, of, Subject } from 'rxjs'
import { ShopUser, Item, Log, ItemAmount } from 'src/app/models/models'
import { DataService } from 'src/app/services/data.service'
import { ToolService } from 'src/app/services/tool.service'

@Component({
  selector: 'friday-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit {

  form: FormGroup = null

  logtypes: LogType[]
  selected: LogType
  needsInput = false

  displayText: string

  users: ShopUser[]
  items: Item[]
  displayList: { display: string, value: any }[]

  selectedLogType: 'log' | 'itemamount' | 'amount'
  selectedDisplayType: 'single' | 'list'
  selectedSingleDataType: 'currency' | 'non-labeled' | undefined
  // selectedDisplayTypeSubject: Subject<string> = new BehaviorSubject<string>('')

  hasSelectedLog = false// set to true when in subscribe method to avoid null errors in html

  data: Observable<Log[] | ItemAmount[] | number>

  constructor(public tool: ToolService, public dataService: DataService, public fb: FormBuilder) {
    // Get data
    this.dataService.getAllItems().subscribe(s => this.items = s)
    this.dataService.getUsers().subscribe(s => this.users = s)

    // Init form and logs
    this.logtypes = this.initLogs()
    this.selected = this.logtypes[0]
    this.form = fb.group({
      selection: fb.control('', Validators.required),
      input: fb.control('')
    })

    // Selection config
    this.form.get('selection').valueChanges.subscribe(s => {
      this.selected = s
      this.selectedLogType = s.type
      // this.selectedDisplayTypeSubject.next(s.displayType)
      this.selectedDisplayType = s.displayType
      this.needsInput = s.needInput

      if (s.needInput && s.inputDisplayName) {
        switch (s.inputDisplayName.toLowerCase()) {
          case 'username':
            this.displayList = this.users.map(t => ({ display: t.name, value: t.id }))
            break
          case 'item':
            this.displayList = this.items.map(t => ({ display: t.name, value: t.id }))
            break
          default:
            this.displayList = []
            break

        }
      }
      if (this.selected.needInput)
        this.form.get('input').setValidators(([Validators.required]))
      else
        this.form.get('input').clearValidators()
      this.form.get('input').reset()

    }
    )

    // this.selectedDisplayTypeSubject.subscribe(s => console.log(s))
  }

  ngOnInit(): void {
  }

  initLogs(): LogType[] {
    return [
      new LogType('All Currency Logs', 'list', false, 'log', 'currency/all'),
      new LogType('All Item Logs', 'list', false, 'log', 'item/all'),
      new LogType('Currency Logs By User', 'list', true, 'log', 'currency', 'Username'),
      new LogType('Item Logs By Item', 'list', true, 'log', 'item', 'Item'),
      new LogType('Remaining Stock', 'list', false, 'itemamount', 'stock/remaining', null, false),
      new LogType('Sold Items', 'list', false, 'itemamount', 'stock/sold', null, true),
      new LogType('Total Income', 'single', false, 'amount', 'total')
    ]
  }

  submit(): void {
    if (!!!this.selected)
      return
    const param = this.inputToParam()
    this.hasSelectedLog = true

    switch (this.selected.type.toLowerCase()) {
      case 'log':

        this.data = this.tool.getLogs(`${this.selected.route}/${this.needsInput ? param : ''}`)
        break
      case 'itemamount':
        this.data = this.tool.getItemAmounts(this.selected.route.includes('/sold'))
        this.displayText = this.selected.route.split('/')[1]
        break
      case 'amount':
        this.data = of(-1)
        this.data = this.tool.getTotalIncome()
        this.selectedSingleDataType = 'currency'
        // this.data.subscribe(s=> console.log(s))
        break
      default:
        break
    }
  }

  private inputToParam(): number {
    if (!this.selected.needInput)
      return null
    return this.form.get('input')?.value || -1
  }

  /**
   * Checks if the form is dirty, valid and if the necessary added input is filled in and valid
   * @returns If form can be submitted
   */
  canSubmit(): boolean {
    return this.form.dirty && this.form.valid && (!this.needsInput || (this.form.get('input')?.dirty && this.form.get('input')?.valid))
  }

  getDisplayColumns(): string[] {
    switch (this.selectedLogType.toLowerCase()) {
      case 'log':
        return ['name', 'time', 'amount']
      case 'itemamount':
        return ['name', 'amount']
    }
  }
}


class LogType {
  constructor(public display: string, public displayType: string,
              public needInput: boolean, public type: string, public route: string,
              public inputDisplayName?: string, public extraParam?: boolean) { }
}
