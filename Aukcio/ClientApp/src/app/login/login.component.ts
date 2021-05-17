import {Component, OnInit} from '@angular/core';
import {AuthService} from '../auth.service';
import {User} from '../user';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private auth: AuthService, private router: Router) {
  }

  ngOnInit() {
  }

  login(username: HTMLInputElement, password: HTMLInputElement) {
    const user = new User();
    user.Username = username.value;
    user.Password = password.value;
    this.auth.sendLogin(user).subscribe(v => {
      this.router.navigate(['']);
    }, error => {
      console.log(error);
      // do something else....
    });
  }

}
