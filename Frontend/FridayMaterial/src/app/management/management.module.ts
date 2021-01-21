import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManagementBaseComponent } from './management-base/management-base.component';
import { Routes } from '@angular/router';

const routes: Routes=[
  { path: "manage", component: ManagementBaseComponent, children: [

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
