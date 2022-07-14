import { Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {User} from "../../models/user";
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    user = new User("", "");
    constructor(private auth:AuthService, private router:Router) { }


  ngOnInit(): void {
      if(this.auth.isLogged()) this.router.navigateByUrl("products");
  }

    submit(){
	this.auth.login(this.user);
	if(this.auth.isLogged()) this.router.navigateByUrl("products");
    }

}
