import {Injectable} from '@angular/core';
import {Outlook} from './outlook'
import {OutlookMessage} from "./outlook-message.class";
import { Subject, Observable } from 'rxjs';
import { JsonApiService } from '@app/core/services';
import { map, tap } from 'rxjs/operators';


@Injectable()
export class OutlookService {

  public activeFolder: Subject<any>;

  public messages: Subject<any>;

  private state = {
    lastFolder: '',
    messages: []
  };

  constructor(private jsonApiService: JsonApiService) {
    this.activeFolder = new Subject();
    this.messages = new Subject();
  }

  getOutlook(): Observable<Outlook> {
    return this.jsonApiService.fetch('/outlook/outlook.json')
  }

  getMessages(folder: string) {

    this.jsonApiService.fetch('/outlook/' + folder + '.json')
      .pipe(

      map(this.mapToMessages),
      tap(data => {
        this.state.lastFolder = folder;
        this.state.messages = data;

        this.activeFolder.next(folder);
        this.messages.next(this.state.messages);
        return data
      })
    )
      .subscribe();

  }

  getMessage(id: string) {
    return this.jsonApiService.fetch('/outlook/message.json')
  }

  deleteSelected() {

    this.messages.next(this.state.messages.filter((it)=>!it.selected))
  }

  private mapToMessages(rawMessages): Array<OutlookMessage> {
    return rawMessages.map(it=>new OutlookMessage(it))
  }

}
