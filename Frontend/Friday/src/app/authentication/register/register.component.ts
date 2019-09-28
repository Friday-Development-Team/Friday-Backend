import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'friday-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  hasSubmitted = false
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
    this.hasSubmitted = true
  }

  toLogin() {
    this.router.navigate(['/auth/login'])
  }

}
