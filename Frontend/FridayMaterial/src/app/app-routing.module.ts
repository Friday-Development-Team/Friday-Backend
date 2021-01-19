import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { StoreBaseComponent } from './store/store-base/store-base.component';
import { AuthService } from './services/auth/auth.service';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  {path: "", redirectTo: "store", pathMatch: "full"},
  {path: "auth", component: LoginComponent},
  {path: "store", component: StoreBaseComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
