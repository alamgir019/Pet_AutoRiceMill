import { Component, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { PartyDto, PartyServiceProxy, ListResultDtoOfPartyDto } from "@shared/service-proxies/service-proxies";
import { finalize } from "rxjs/operators";
import { MatDialog } from "@angular/material";
import { CreatePartyDialogComponent } from "./create-party/create-party-dialog.component";
import { EditPartyDialogComponent } from "./edit-party/edit-party-dialog.component";
//import * as ApiServiceProxies from '@shared/service-proxies/service-proxies';

class PagedPartiesRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}
@Component({
    templateUrl:'./parties.component.html',
    animations:[appModuleAnimation()]
    //providers: [ApiServiceProxies.PartyServiceProxy]
})

export class PartiesComponent extends PagedListingComponentBase<PartyDto>{
    parties: PartyDto[] = [];  
    constructor(
        injector:Injector,
        private _partyService: PartyServiceProxy,
        private _dialog: MatDialog
        ){
        super(injector);
    }
    editParty(party:PartyDto){
        this.showCreateOrEditUserDialog(party.id);
    }

    protected list(
        request: PagedPartiesRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        this._partyService
            .getAll(request.isActive)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: ListResultDtoOfPartyDto) => {
                this.parties = result.items;
                this.totalItems=result.items.length;
                //this.showPaging(result, pageNumber);
            });
    }
    protected delete(party: PartyDto)
    {
        abp.message.confirm(
        this.l('UserDeleteWarningMessage', party.name),
        (result: boolean) => {
            if (result) {
                this._partyService.delete(party.id).subscribe(() => {
                    abp.notify.success(this.l('SuccessfullyDeleted'));
                    this.refresh();
                });
            }
        }
    );
    }
    
    createParty(): void {
        this.showCreateOrEditUserDialog();
    }
    showCreateOrEditUserDialog(id?:number):void{
        let createOrEditPartyDialog;
        if(id==undefined||id<=0)
        {
            createOrEditPartyDialog=this._dialog.open(CreatePartyDialogComponent);
        }
        else
        {
            createOrEditPartyDialog = this._dialog.open(EditPartyDialogComponent, {
                data: id
            });

        }
        
        createOrEditPartyDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}