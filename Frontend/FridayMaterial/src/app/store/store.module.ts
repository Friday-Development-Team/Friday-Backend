import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth/auth.service';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth/auth.guard';
import { ShopComponent } from './shop/shop.component';
import { StoreBaseComponent } from './store-base/store-base.component';

const routes: Routes = [
  {
    path: '' , canActivate: [AuthGuard], children: [
      { path: '', redirectTo: 'shop', pathMatch: 'full' },
      { path: 'shop', component: ShopComponent},
      //{ path: 'history', component: null},
      { path: '**', redirectTo: 'shop'}
    ]
  }
];

@NgModule({
  declarations: [StoreBaseComponent, ShopComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  providers:[
    AuthGuard,
    AuthService,
    HttpClientModule
  ],
  exports: [
    RouterModule
  ]
})
export class StoreModule { }
