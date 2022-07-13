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
	return this.http.get(environment.endPoins+"products");
    }

    add(product:object)
    {
	this.http.post(environment.endPoins+"products", product);
    }

    update(product:object)
    {
	this.http.put(environment.endPoins+"products", product);
    }

    delete(id:number)
    {
	this.http.delete(environment.endPoins+"products", { params: {"id": id}});
    }
}
