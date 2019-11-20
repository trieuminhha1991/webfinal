import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbDownloadEbookForViewDto, PbDownloadEbookDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbDownloadEbookModal',
    templateUrl: './view-pbDownloadEbook-modal.component.html'
})
export class ViewPbDownloadEbookModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbDownloadEbookForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbDownloadEbookForViewDto();
        this.item.pbDownloadEbook = new PbDownloadEbookDto();
    }

    show(item: GetPbDownloadEbookForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
