import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbTypeFileForViewDto, PbTypeFileDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbTypeFileModal',
    templateUrl: './view-pbTypeFile-modal.component.html'
})
export class ViewPbTypeFileModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbTypeFileForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbTypeFileForViewDto();
        this.item.pbTypeFile = new PbTypeFileDto();
    }

    show(item: GetPbTypeFileForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
