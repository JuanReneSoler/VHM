import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {Register} from 'src/app/models/register';
import {AuthService} from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

    user = new Register("","","","","","", "");
    constructor(private auth:AuthService, private router:Router) { }

  ngOnInit(): void {
  }

    submit(){
	this.auth.register(this.user).subscribe(()=>{
	    this.router.navigateByUrl("auth");
	});
    }

}
