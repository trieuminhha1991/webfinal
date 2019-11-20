import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbPriceUserForViewDto, PbPriceUserDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbPriceUserModal',
    templateUrl: './view-pbPriceUser-modal.component.html'
})
export class ViewPbPriceUserModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbPriceUserForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbPriceUserForViewDto();
        this.item.pbPriceUser = new PbPriceUserDto();
    }

    show(item: GetPbPriceUserForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
