import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {LoginComponent} from './components/login/login.component';
import {NewComponent} from './components/new/new.component';
import {RegisterComponent} from './components/register/register.component';

const routes: Routes = [
    {
	path: "",
	redirectTo:"auth",
	pathMatch:"full"
    },
    {
	path: "products",
	component:HomeComponent
    },
    {
	path: "auth",
	component:LoginComponent
    },
    {
	path:"register",
	component:RegisterComponent
    },
    {
	path:"new",
	component:NewComponent
    },
    {
	path:"new/:id",
	component:RegisterComponent
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
