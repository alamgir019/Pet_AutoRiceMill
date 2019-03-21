import { Component, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { PartyDto, PartyServiceProxy, ListResultDtoOfPartyDto } from "@shared/service-proxies/service-proxies";
import { finalize } from "rxjs/operators";

class PagedPartiesRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}
@Component({
    templateUrl:'./parties.components.html',
    animations:[appModuleAnimation()]
})

export class PartiesComponent extends PagedListingComponentBase<PartyDto>{
    parties: PartyDto[] = [];    
    constructor(
        injector:Injector,
        private _partyService: PartyServiceProxy,
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

}