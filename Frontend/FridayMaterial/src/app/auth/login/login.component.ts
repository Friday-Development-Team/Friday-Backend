import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { FormBuilder, Form, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'friday-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form: FormGroup
  error: boolean

  constructor(private auth: AuthService, fb: FormBuilder) {
    this.form = fb.group({
      username: fb.control('', Validators.required),
      password: fb.control('', [Validators.required, Validators.minLength(6)])
    })
   }

  ngOnInit(): void {
  }

  submit(){
    console.log("Test");
  }

}
