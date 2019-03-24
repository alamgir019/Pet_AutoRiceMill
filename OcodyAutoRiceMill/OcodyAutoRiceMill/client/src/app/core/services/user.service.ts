import { Injectable } from "@angular/core";

import { BehaviorSubject } from "rxjs";
import { JsonApiService } from "@app/core/services/json-api.service";



const defaultUser = {
  username: "Guest"
}
@Injectable()
export class UserService {
  public user$ = new BehaviorSubject({...defaultUser});

  constructor(private jsonApiService: JsonApiService) {
    this.jsonApiService.fetch("/user/login-info.json").subscribe(this.user$)
  }

  public logout(){
    this.user$.next({...defaultUser})
  }

}
