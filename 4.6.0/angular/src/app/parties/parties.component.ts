import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { PartyServiceProxy, PartyListDto } from '@shared/service-proxies/service-proxies';
// import { CreateUserDialogComponent } from './create-user/create-user-dialog.component';
// import { EditUserDialogComponent } from './edit-user/edit-user-dialog.component';
// import { Moment } from 'moment';
// import { ResetPasswordDialogComponent } from './reset-password/reset-password.component';

class PagedUsersRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}

@Component({
    templateUrl: './parties.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
      ]
})
export class PartiesComponent extends PagedListingComponentBase<PartyListDto> {
    parties: PartyListDto[] = [];
    keyword = '';
    isActive: boolean | null;

    constructor(
        injector: Injector,
        private _partyService: PartyServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    createParty(): void {
        // this.showCreateOrEditPartyDialog();
    }

    editParty(party: PartyListDto): void {
        // this.showCreateOrEditPartyDialog(party.id);
    }

    public resetPassword(party: PartyListDto): void {
        // this.showResetPasswordPartyDialog(party.id);
    }
    delete(): void {
        // this.showCreateOrEditPartyDialog();
    }
    protected list(
        request: PagedUsersRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

        request.keyword = this.keyword;
        request.isActive = this.isActive;

        this._partyService
            .getAll(undefined, undefined, undefined, undefined)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: any) => {
                this.parties = result.items;
                this.showPaging(result, pageNumber);
            });
    }
}