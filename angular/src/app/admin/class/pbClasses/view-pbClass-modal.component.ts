import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbClassForViewDto, PbClassDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbClassModal',
    templateUrl: './view-pbClass-modal.component.html'
})
export class ViewPbClassModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbClassForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbClassForViewDto();
        this.item.pbClass = new PbClassDto();
    }

    show(item: GetPbClassForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
