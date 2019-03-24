import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {Outlook, Folder} from "./shared/outlook";
import {OutlookService} from "./shared/outlook.service";
import { routerTransition } from '@app/shared/utils/animations';

@Component({
  selector: 'sa-outlook',
  templateUrl: './outlook.component.html',
  animations: [routerTransition]
})
export class OutlookComponent implements OnInit {

  outlook:Outlook;

  activeFolderKey:string;
  activeFolder:Folder;

  private outlookSub:Subscription;
  private activeFolderSub:Subscription;

  constructor(private route:ActivatedRoute,
              private router:Router,
              private outlookService:OutlookService) {
    this.outlook = new Outlook();
    this.activeFolder = new Folder()
  }


  ngOnInit() {
    this.outlookSub = this.outlookService.getOutlook().subscribe(
      outlook => {
        this.outlook = outlook
      }
    );

    this.activeFolderSub = this.outlookService.activeFolder.subscribe(
      folder => {
        this.activeFolderKey = folder;
        if (this.outlook.folders) {
          this.activeFolder = this.outlook.folders.find(it=>it.key == folder)
        }
      }
    )
  }

  ngOnDestroy() {
    this.outlookSub.unsubscribe();
    this.activeFolderSub.unsubscribe();
  }

  deleteSelected(){
    this.outlookService.deleteSelected()
  }

  getState(outlet) {

    let ss = outlet.activatedRoute.snapshot;

    // return unique string that is used as state identifier in router animation
    return (
      outlet.activatedRouteData.state ||
      (ss.url.length
        ? ss.url[0].path
        : ss.parent.url.length
          ? ss.parent.url[0].path
          : null)
    );
  }
}
