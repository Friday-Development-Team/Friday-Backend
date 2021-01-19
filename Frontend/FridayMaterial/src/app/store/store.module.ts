import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreBaseComponent } from './store-base/store-base.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth/auth.service';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth/auth.guard';
import { ShopComponent } from './shop/shop.component';
import { AppRoutingModule } from '../app-routing.module';

const routes: Routes = [
  {
    path: 'store', component: StoreBaseComponent, canActivate: [AuthGuard], children: [
      { path: '', redirectTo: 'shop', pathMatch: 'full' },
      { path: 'shop', component: ShopComponent}
    ]
  }
];

@NgModule({
  declarations: [StoreBaseComponent, ShopComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    AppRoutingModule
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
