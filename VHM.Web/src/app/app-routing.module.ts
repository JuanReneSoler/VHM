import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
	path:"/",
	redirectTo: "auth",
	pathMatch: "full",
	children:[
	    {
		path:"home",
	    }
	]
    },
    {
	path:"auth",
	loadChildren: ()=> import("./auth/auth.module").then(x => x.AuthModule)
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
