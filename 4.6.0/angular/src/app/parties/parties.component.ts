import { Component, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { PartyDto, PartyServiceProxy, ListResultDtoOfPartyDto } from "@shared/service-proxies/service-proxies";
import { finalize } from "rxjs/operators";
import { MatDialog } from "@angular/material";
import { CreatePartyDialogComponent } from "./create-party/create-party-dialog.component";
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
                //this.showPaging(result, pageNumber);
            });
    }
    protected delete(party: PartyDto)
    {

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
        
        createOrEditPartyDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}