import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbEbookForViewDto, PbEbookDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbEbookModal',
    templateUrl: './view-pbEbook-modal.component.html'
})
export class ViewPbEbookModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbEbookForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbEbookForViewDto();
        this.item.pbEbook = new PbEbookDto();
    }

    show(item: GetPbEbookForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
