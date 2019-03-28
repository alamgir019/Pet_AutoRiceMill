import { Component, OnInit, Injector } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { inherits } from "util";
import { PartyDto, PartyServiceProxy, CreatePartyInput } from "@shared/service-proxies/service-proxies";
import { MatDialogRef } from "@angular/material";
import { finalize } from "rxjs/operators";

@Component({
    templateUrl:'create-party-dialog.component.html',
    styles:[`
    mat-form-field{ width:100%}
    `]
})
export class CreatePartyDialogComponent extends AppComponentBase{
    saving=false;
    party:CreatePartyInput=new CreatePartyInput();
    constructor(
        injector: Injector,
        private _partyService: PartyServiceProxy,
        private _dialogRef: MatDialogRef<CreatePartyDialogComponent>
        ){
            super(injector);
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