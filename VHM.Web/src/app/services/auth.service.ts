import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../models/user';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Register} from '../models/register';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

    constructor(private http: HttpClient, private cookie:CookieService) { }

    login(user:User)
    {
	this.http.post(environment.endPoins+"authenticate/login", user)
	    .subscribe((res)=>{
		this.cookie.set(environment.cookieName, JSON.stringify(res));
	    });
    }

    logout()
    {
	this.cookie.delete(environment.cookieName);
    }

    isLogged()
    {
	return this.cookie.check(environment.cookieName);
    }

    getHeader(){
	if(!this.cookie.check(environment.cookieName)) return;
	var _cookie = this.cookie.get(environment.cookieName);
	var _obj = JSON.parse(_cookie);
	return new HttpHeaders().set("Authorization", `Bearer ${_obj.token}`);
    }

    register(register:Register){
	return this.http.post(environment.endPoins+"authenticate/register", register);
    }

}
