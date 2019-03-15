import {Injectable} from '@angular/core';
import {Observable} from 'rxjs'
import { JsonApiService } from '@app/core/services';


@Injectable()
export class MapStyleService {


  constructor(private jsonApiService:JsonApiService) {  }


  fetchStyle(style):Observable<any> {
    return this.jsonApiService.fetch(style.url)
  }

}
