import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import {AuthService} from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

    header:any = "";
    constructor(private http: HttpClient, private auth:AuthService) {
	this.header = this.auth.getHeader();
    }

    getAll()
    {
	return this.http.get(environment.endPoins+"products", {headers: this.header});
    }

    add(product:object)
    {
	return this.http.post(environment.endPoins+"products", product, {headers: this.header});
    }

    update(product:object)
    {
	return this.http.put(environment.endPoins+"products", product, {headers: this.header});
    }

    delete(id:number)
    {
	return this.http.delete(environment.endPoins+"products", { params: {"id": id}, headers: this.header});
    }

    types(){
	return this.http.get(environment.endPoins+"products/types", {headers: this.header});
    }

    providers(){
	return this.http.get(environment.endPoins+"products/providers", {headers: this.header});
    }
}
