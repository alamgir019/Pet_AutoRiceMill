import { Component, Injector, Optional, Inject, OnInit } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { PartyDto, PartyServiceProxy } from "@shared/service-proxies/service-proxies";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { finalize } from "rxjs/operators";

@Component({
  templateUrl: './edit-party-dialog.component.html',
  styles: [
    `
      mat-form-field {
        width: 100%;
      }
    `
  ]
})
export class EditPartyDialogComponent extends AppComponentBase implements OnInit{
  saving = false;
  party: PartyDto = new PartyDto();

  constructor(
    injector: Injector,
    public _partyService: PartyServiceProxy,
    private _dialogRef: MatDialogRef<EditPartyDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._partyService.get(this._id).subscribe(result => {
      this.party = result;
    });
  }
  
  save(): void {
    this.saving = true;        
    this._partyService
        .create(this.party)
        .pipe(
        finalize(() => {
            this.saving = false;
        })
        )
        .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close(true);
        });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}