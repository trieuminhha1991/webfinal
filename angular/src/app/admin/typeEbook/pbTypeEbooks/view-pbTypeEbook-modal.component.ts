import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbTypeEbookForViewDto, PbTypeEbookDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbTypeEbookModal',
    templateUrl: './view-pbTypeEbook-modal.component.html'
})
export class ViewPbTypeEbookModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbTypeEbookForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbTypeEbookForViewDto();
        this.item.pbTypeEbook = new PbTypeEbookDto();
    }

    show(item: GetPbTypeEbookForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
