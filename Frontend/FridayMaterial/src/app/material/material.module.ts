import { NgModule } from '@angular/core';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';


const modules = [
  MatCardModule,
  MatInputModule,
  MatButtonModule,
  MatToolbarModule,
  MatToolbarModule,
  MatSidenavModule,
  MatListModule,
  MatIconModule,
  FlexLayoutModule
];

@NgModule({
  imports: modules,
  exports: modules,
})
export class MaterialModule {}

