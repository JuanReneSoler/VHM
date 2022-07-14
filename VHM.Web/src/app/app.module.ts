import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import {LoginComponent} from './components/login/login.component';
import {HomeComponent} from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { NewComponent } from './components/new/new.component';

@NgModule({
  declarations: [
      AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    NewComponent
  ],
  imports: [
    BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
