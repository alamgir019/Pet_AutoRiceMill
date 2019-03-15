import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NotificationService } from "@app/core/services/notification.service";

import {UserService} from "@app/core/services/user.service";

@Component({
  selector: "sa-logout",
  template: `
<div id="logout" (click)="showPopup()" class="btn-header transparent pull-right">
        <span> <a title="Sign Out"><i class="fa fa-sign-out"></i></a> </span>
    </div>
  `,
  styles: []
})
export class LogoutComponent implements OnInit {

  public user

  constructor(
    private userService: UserService,
    private router: Router,
    private notificationService: NotificationService
  ) {
  }

  showPopup() {
    this.notificationService.smartMessageBox(
      {
        title:
          "<i class='fa fa-sign-out txt-color-orangeDark'></i> Logout <span class='txt-color-orangeDark'><strong>" + this.userService.user$.value.username+"</strong></span> ?",
        content:
          "You can improve your security further after logging out by closing this opened browser",
        buttons: "[No][Yes]"
      },
      ButtonPressed => {
        if (ButtonPressed == "Yes") {
          this.logout();
        }
      }
    );
  }

  logout() {
    this.userService.logout()
    this.router.navigate(["/auth/login"]);
  }

  ngOnInit() {}
}
