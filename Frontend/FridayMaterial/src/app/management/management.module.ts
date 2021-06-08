import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManagementBaseComponent } from './management-base/management-base.component';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { CateringManagementComponent } from './catering/catering.component';
import { RoleGuard } from './role.guard';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: "", component: ManagementBaseComponent,
    canActivate: [RoleGuard], data: { role: ["admin", "catering"] }
    // children: [
    //   {
    //     path: "admin", component: AdminComponent,
    //     canActivate: [RoleGuard], data: { role: ["admin"] }
    //   },
    //   {
    //     path: "catering", component: CateringManagementComponent,
    //     canActivate: [RoleGuard], data: { role: ["catering"] }
    //   }
    // ]
  }
]

@NgModule({
  declarations: [ManagementBaseComponent, AdminComponent, CateringManagementComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MaterialModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class ManagementModule { }
