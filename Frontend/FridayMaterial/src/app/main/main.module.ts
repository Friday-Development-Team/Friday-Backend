import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { RouterModule, Routes } from '@angular/router';
import { MaterialModule } from '../material/material.module';

const routes: Routes= [
  { path: "main", component: NavComponent, children: [
    { path: "manage", loadChildren: () => import('../management/management.module').then(s=> s.ManagementModule)},
    { path: "store", loadChildren: () => import('../store/store.module').then(s=> s.StoreModule)},
    {path: "", redirectTo: 'store', pathMatch: 'full'}
    ]
  }
]

@NgModule({
  declarations: [NavComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MaterialModule
  ],
  exports: [RouterModule]
})
export class MainModule { }
