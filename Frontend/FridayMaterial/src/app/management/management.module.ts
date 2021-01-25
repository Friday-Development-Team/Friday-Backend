import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManagementBaseComponent } from './management-base/management-base.component';
import { Routes } from '@angular/router';

const routes: Routes=[
  { path: "", component: ManagementBaseComponent, children: [
    { path: '', redirectTo: '', pathMatch: 'full'},// TODO Add redirect
    ] 
  }
]

@NgModule({
  declarations: [ManagementBaseComponent],
  imports: [
    CommonModule
  ]
})
export class ManagementModule { }
