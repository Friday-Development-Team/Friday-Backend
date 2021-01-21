import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';

import { AuthService } from './services/auth/auth.service';
import { AuthGuard } from './auth/auth.guard';
import { StoreBaseComponent } from './store/store-base/store-base.component';

const routes: Routes = [
  {path: "", redirectTo: "main", pathMatch: "full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
