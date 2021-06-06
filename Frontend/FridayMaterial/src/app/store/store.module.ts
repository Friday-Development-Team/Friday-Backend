import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth/auth.service';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth/auth.guard';
import { ShopComponent } from './shop/shop.component';
import { StoreBaseComponent } from './store-base/store-base.component';
import { MaterialModule } from '../material/material.module';
import { ShowcaseComponent } from './showcase/showcase.component';
import { CartContainerComponent } from './cart-container/cart-container.component';
import { CartComponent } from './cart/cart.component';
import { ItemcardComponent } from './itemcard/itemcard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RunningComponent } from './running/running.component';
import { DialogComponent } from './dialog/dialog.component';
import { ErrordialogComponent } from './errordialog/errordialog.component';

const routes: Routes = [
  {
    path: '', canActivate: [AuthGuard], children: [
      { path: '', redirectTo: 'shop', pathMatch: 'full' },
      { path: 'shop', component: ShopComponent },
      { path: 'running', component: RunningComponent },
      //{ path: 'history', component: null},
      { path: '**', redirectTo: 'shop' }
    ]
  }
];

@NgModule({
  declarations: [
    StoreBaseComponent,
    ShopComponent,
    ShowcaseComponent,
    CartContainerComponent,
    CartComponent,
    ItemcardComponent,
    RunningComponent,
    DialogComponent,
    ErrordialogComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    AuthGuard,
    AuthService,
    HttpClientModule
  ],
  exports: [
    RouterModule
  ]
})
export class StoreModule { }
