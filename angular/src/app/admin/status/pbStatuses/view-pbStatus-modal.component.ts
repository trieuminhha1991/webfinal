import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbStatusForViewDto, PbStatusDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbStatusModal',
    templateUrl: './view-pbStatus-modal.component.html'
})
export class ViewPbStatusModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbStatusForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbStatusForViewDto();
        this.item.pbStatus = new PbStatusDto();
    }

    show(item: GetPbStatusForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
