import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Observable, of } from 'rxjs'
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
  displayList: string[]

  selectedLogType: string
  selectedDisplayType: string

  hasSelectedLog = false// set to true when in subscribe method to avoid null errors in html

  data: Observable<Log[] | ItemAmount[] | number>



  constructor(public tool: ToolService, public dataService: DataService, public fb: FormBuilder) {
    this.dataService.getAllItems().subscribe(s => this.items = s)
    this.dataService.getUsers().subscribe(s => this.users = s)

    this.logtypes = this.initLogs()
    this.selected = this.logtypes[0]
    this.form = fb.group({
      selection: fb.control('', Validators.required),
      input: fb.control('')
    })

    this.form.get('selection').valueChanges.subscribe(s => {
      this.selected = s
      this.selectedLogType = s.type
      this.selectedDisplayType = s.displaytype
      this.needsInput = s.needInput

      if (s.needInput && s.inputDisplayName) {
        switch (s.inputDisplayName.toLowerCase()) {
          case 'username':
            this.displayList = this.users.map(t => t.name)
            break
          case 'item':
            this.displayList = this.items.map(t => t.name)
            break
          default:
            this.displayList = []
            break

        }
      }

      this.form.get('input').setValidators((this.selected.needInput ? [Validators.required] : null))
    }
    )
  }

  ngOnInit(): void {
  }

  initLogs(): LogType[] {
    return [
      new LogType('All Currency Logs', 'list', false, 'log', 'currency/all'),
      new LogType('All Item Logs', 'list', false, 'log', 'item/all'),
      new LogType('Currency Logs By User', 'list', true, 'log', 'currency/user', 'Username'),
      new LogType('Item Logs By Item', 'list', true, 'log', 'item/id', 'Item'),
      new LogType('Remaining Stock', 'list', false, 'itemamount', 'stock/remaining', null, false),
      new LogType('Sold Items', 'list', false, 'itemamount', 'stock/sold', null, true),
      new LogType('Total Income', 'single', false, 'amount', 'total')
    ]
  }

  print(){
    console.log(this.form);
  }

  submit(): void {
    if (!!!this.selected)
      return
    const param = this.inputToParam()

    this.hasSelectedLog = true



    switch (this.selected.type.toLowerCase()) {
      case 'log':

        this.data = this.tool.getLogs(this.selected.route, param)
        break
      case 'itemamount':
        this.data = this.tool.getItemAmounts(this.selected.route.includes('/sold'))
        this.displayText = this.selected.route.split('/')[1]
        break
      case 'amount':
        this.data = of(-1)
        this.data = this.tool.getTotalIncome()
        // this.data.subscribe(s=> console.log(s))
        break
      default:
        break
    }


  }

  private inputToParam(): string | number {
    if (this.selected.needInput) {
      let temp
      temp = this.form.get('input').value
      if (!!!temp)
        return

      switch (this.selected.inputDisplayName.toLowerCase()) {
        case 'username':
          return temp
        case 'item':
          const tempitem = this.items.find(s => s.name === temp)
          return tempitem.id
        default:
          return null
      }

    }
    return null
  }

}

class LogType {
  constructor(public display: string, public displayType: string,
    public needInput: boolean, public type: string, public route: string,
    public inputDisplayName?: string, public extraParam?: boolean) { }
}
