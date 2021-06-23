import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';

import { AuthService } from './services/auth.service';
import { AuthGuard } from './auth/auth.guard';
import { StoreBaseComponent } from './store/store-base/store-base.component';
import { NavComponent } from './nav/nav.component';

const routes: Routes = [
  {path: "", redirectTo: "main", pathMatch: "full"},
  { path: "main", component: NavComponent, canActivate: [AuthGuard], children: [
    { path: "manage", loadChildren: () => import('./management/management.module').then(s=> s.ManagementModule)},
    { path: "store", component: StoreBaseComponent, loadChildren: () => import('./store/store.module').then(s=> s.StoreModule)},
    { path: "", redirectTo: 'store', pathMatch: 'full'}
    ]
  },
  { path: "**", redirectTo: "main", pathMatch: "full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
