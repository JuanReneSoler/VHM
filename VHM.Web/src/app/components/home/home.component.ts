import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {AuthService} from 'src/app/services/auth.service';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    constructor(private prodService: ProductsService, private auth: AuthService, private router:Router) { }

    products:any = [];

    ngOnInit(): void {

	if(!this.auth.isLogged()) this.router.navigateByUrl("auth");

	this.prodService.getAll()
	.subscribe((res)=>{
	    this.products = res;
	});
    }

    remove(id:any):void{
	this.prodService.delete(id)
	.subscribe((res)=>console.log(res));
    }

}
