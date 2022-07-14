import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {Product} from 'src/app/models/product';
import {AuthService} from 'src/app/services/auth.service';
import {ProductsService} from 'src/app/services/products.service';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.css']
})
export class NewComponent implements OnInit {

    product = new Product("", "", "", "", "",);
    typesArr:any = [];
    providerArr:any = [];
    id:any = 0;
    constructor(private auth:AuthService, private router:Router, private prod:ProductsService, private route: ActivatedRoute) {
	this.id = this.route.snapshot.paramMap.get("id");
	console.log(this.id);
    }

  ngOnInit(): void {
      if(!this.auth.isLogged()) this.router.navigateByUrl("auth");
      this.prod.types().subscribe((res)=>{
	  this.typesArr = res;
      });
      this.prod.providers().subscribe((res)=>{
	  this.providerArr = res;
      });
  }

    submit(){
	this.prod.add(this.product).subscribe(()=>{
	    this.router.navigateByUrl("products");
	});
    }

}
