import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {User} from "../../models/user";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    constructor(private auth:AuthService) { }

  ngOnInit(): void {
  }

    logIn(model:User){
	this.auth.login(model)
    }

}
