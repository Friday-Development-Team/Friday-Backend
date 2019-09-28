import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'friday-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  hasSubmitted=false
  form: FormGroup

  constructor(private auth: AuthService, builder: FormBuilder, private router: Router) {
    this.form = builder.group({
      username: builder.control('', Validators.required),
      password: builder.control('', [Validators.required, Validators.minLength(6)])
    })
  }

  ngOnInit() {
  }

  onSubmit() {
    if (!this.form.invalid)
    //auth.login()
    console.log("login")
    this.hasSubmitted=true
  }

  toRegister(){
    this.router.navigate(['/auth/register'])
  }
}
