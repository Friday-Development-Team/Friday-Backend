import { Component, OnInit, ViewChild } from '@angular/core'
import { FormGroup, NgForm } from '@angular/forms'
import { Form, FormBuilder, Validators } from '@angular/forms'
import { Configuration } from 'src/app/models/models'
import { DataService } from 'src/app/services/data.service'
import { DialogService } from 'src/app/services/dialog.service'
import { SpinnerService } from 'src/app/services/spinner.service'
import { ToolService } from 'src/app/services/tool.service'

@Component({
  selector: 'friday-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.scss']
})
export class ConfigComponent implements OnInit {

  form: FormGroup
  config: Configuration

  constructor(private spinner: SpinnerService, private data: DataService, private dialog: DialogService, fb: FormBuilder) {

    this.config = this.data.getConfig()

    this.form = fb.group({
      combinedCateringKitchen: this.config.combinedCateringKitchen,
      usersSetSpot: this.config.usersSetSpot,
      cancelOnAccepted: this.config.cancelOnAccepted
    })

    this.data.getConfigChanges().subscribe(s => {
      this.config = s
      this._updateForm(s)
    })

    // Test

  }


  _updateForm(config: Configuration): void {
    this.form.get('combinedCateringKitchen').setValue(config.combinedCateringKitchen)
    this.form.get('usersSetSpot').setValue(config.usersSetSpot)
    this.form.get('cancelOnAccepted').setValue(config.cancelOnAccepted)
  }

  ngOnInit(): void {
  }

  submit(): void {
    this.spinner.startSpinner()
    this.data.setConfig(this.form.value as Configuration).subscribe(s => {
      this.spinner.stopSpinner(0)
      this.dialog.openTimedDialog(7000, 'Configuration was succesfully updated!')
    },
      err => this.dialog.displayErrorMessage('Configuration could not be updated!')
    )
  }

}
