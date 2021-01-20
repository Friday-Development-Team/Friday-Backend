import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { FormBuilder, Form, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'friday-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form: FormGroup
  error: boolean
  hasSubmitted: boolean

  constructor(private auth: AuthService, fb: FormBuilder, private router: Router) {
    this.form = fb.group({
      username: fb.control('', Validators.required),
      password: fb.control('', [Validators.required, Validators.minLength(6)])
    })
   }

  ngOnInit(): void {
  }

  submit(){
    this.hasSubmitted = true
    if(this.form.invalid){
      this.error=true
      return
    }
    
    this.auth
    .login(this.form.value.username, this.form.value.password)
    .subscribe(
      val => {
        if (val) {
          if (this.auth.redirectUrl) {
            this.router.navigateByUrl(this.auth.redirectUrl)
            this.auth.redirectUrl = undefined
          } else {
            this.router.navigate(['/store'])
          }
        }
      }
    );

  }

}
