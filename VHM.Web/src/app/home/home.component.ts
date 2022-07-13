import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../services/products.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    constructor(private prodService: ProductsService) { }

    products:any = [];

    ngOnInit(): void {
	this.prodService.getAll()
	.subscribe((res)=>{
	    this.products = res;
	});
    }

    delete(id:any){
	this.prodService.delete(id);
    }

}
