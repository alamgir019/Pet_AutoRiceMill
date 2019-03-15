import { Component, OnInit } from '@angular/core';
import {Router,ActivatedRoute} from "@angular/router";

import { AlertService } from '../../../core/services/alert.service';
import { AuthService } from "../../../core/services/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  model: any = {};
  loading = false;
  returnUrl: string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alertService: AlertService
  ) { }

    ngOnInit() {
      // reset login status
      this.authService.logout();

      // get return url from route parameters or default to '/'
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login(event){

    this.loading = true;
    this.authService.login(this.model.username, this.model.password)
        .subscribe(
            data => {
                this.router.navigate([this.returnUrl]);
            },
            error => {
                console.log(error._body);
                this.alertService.error(error._body);
                this.loading = false;
            });
  }

}
