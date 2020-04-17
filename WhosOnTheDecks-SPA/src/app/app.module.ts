import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import {FormsModule} from '@angular/forms';
import { AuthService } from './_service/auth.service';
import { HomeComponent } from './home/home.component';
import { ErrorInterceptorProvider } from './_service/error.interceptor';
import { RegisterPromoterComponent } from './register-promoter/register-promoter.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterPromoterComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
      ErrorInterceptorProvider,
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
