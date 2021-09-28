import { Component, OnInit } from '@angular/core'
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, NgForm, ValidationErrors, ValidatorFn, Validators } from '@angular/forms'
import { Observable, of } from 'rxjs'
import { catchError, map } from 'rxjs/operators'
import { ItemModel } from 'src/app/models/models'
import { DialogService } from 'src/app/services/dialog.service'
import { HttpService } from 'src/app/services/http.service'
import { SpinnerService } from 'src/app/services/spinner.service'
import { ToolService } from 'src/app/services/tool.service'
import { Constants } from '../../../../models/constants'

@Component({
  selector: 'friday-additem',
  templateUrl: './additem.component.html',
  styleUrls: ['./additem.component.scss']
})
export class AdditemComponent implements OnInit {

  types = Constants.itemTypes
  form: FormGroup

  constructor(private tool: ToolService, private spinner: SpinnerService,
    private dialog: DialogService, fb: FormBuilder, private http: HttpService) {
    this.form = fb.group({
      name: fb.control('', Validators.required),
      price: fb.control('', [Validators.required]),
      type: fb.control('', Validators.required),
      count: fb.control('', Validators.required),
      url: fb.control('', { validators: [Validators.required, this.imageUrlValidator()], updateOn: 'blur' }),
      size: fb.control(''),
      calories: fb.control(''),
      sugarContent: fb.control(''),
      saltContent: fb.control(''),
      allergens: fb.control('')
    })
    this.form.valueChanges.subscribe(s => console.log(this.form))
  }

  ngOnInit(): void {
  }

  addItem(): void {
    this.spinner.startSpinner()
    const model = this.form.value as ItemModel
    console.log(model)

    this.tool.addItem(model.toDTO())
      .subscribe(
        (response) => {
          this.spinner.stopSpinner()
        },
        (err) => {
          this.spinner.stopSpinner()
          this.dialog.displayErrorMessage(err)
        }
      )
  }

  imageUrlValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      return new Promise(resolve => {
        const image = new Image()
        image.addEventListener('load', () => { resolve(image) })
        image.src = control.value
      }).then(() => null).catch((err) => {
        console.log(err)
        return { invalid: true }
      })
    }
  }

}
