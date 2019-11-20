import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbPlaceForViewDto, PbPlaceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbPlaceModal',
    templateUrl: './view-pbPlace-modal.component.html'
})
export class ViewPbPlaceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbPlaceForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbPlaceForViewDto();
        this.item.pbPlace = new PbPlaceDto();
    }

    show(item: GetPbPlaceForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
