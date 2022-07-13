import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

    constructor(private http: HttpClient) { }

    getAll()
    {
	return this.http.get(environment.endPoins+"products")
	    .subscribe({
		next(res){
		    return res;
		},
		error(res){
		    alert("error, view console to details");
		    console.log(res);
		}
	    })
    }

    add(product:object)
    {
	this.http.post(environment.endPoins+"products", product)
	    .subscribe({
		next(){ alert("saved ssuccerfull");},
		error(err){
		    alert("error, view console to details");
		    console.log(err);
		}
	    });
    }

    update(product:object)
    {
	this.http.put(environment.endPoins+"products", product)
	    .subscribe({
		next(){ alert("saved ssuccerfull");},
		error(err){
		    alert("error, view console to details");
		    console.log(err);
		}
	    });
    }

    delete(id:number)
    {
	this.http.get(environment.endPoins+`products?id=${id}`)
	    .subscribe({
		next(res){
		    return res;
		},
		error(res){
		    alert("error, view console to details");
		    console.log(res);
		}
	    })
    }
}
