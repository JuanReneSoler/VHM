import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../models/user';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

    constructor(private http: HttpClient, private cookie:CookieService) { }

    login(user:User)
    {
	var result = this.http.post(environment.endPoins+"authenticate", user)
	    .subscribe({
		next(){
		}
	    });
    }

}
