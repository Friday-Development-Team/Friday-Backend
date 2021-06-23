import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule } from './auth/auth.module';
import { MaterialModule } from './material/material.module';
import { NavComponent } from './nav/nav.component';
import { httpInterceptorProviders } from './auth/interceptors';
import { MessageDialogComponent } from './shared/messagedialog/messagedialog.component';



@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MessageDialogComponent
  ],
  imports: [
    // Submodules
    AuthModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
  ],
  providers: [
    HttpClientModule,
    httpInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
