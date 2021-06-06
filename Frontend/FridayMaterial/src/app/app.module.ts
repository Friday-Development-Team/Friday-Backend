import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthGuard } from './auth/auth.guard';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './services/auth/auth.service';
import { AuthModule } from './auth/auth.module';
import { StoreModule } from './store/store.module';
import { MaterialModule } from './material/material.module';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { NavComponent } from './nav/nav.component';
import { httpInterceptorProviders } from './auth/interceptors';



@NgModule({
  declarations: [
    AppComponent,
    NavComponent
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
