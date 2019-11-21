import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbLinkProForViewDto, PbLinkProDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbLinkProModal',
    templateUrl: './view-pbLinkPro-modal.component.html'
})
export class ViewPbLinkProModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbLinkProForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbLinkProForViewDto();
        this.item.pbLinkPro = new PbLinkProDto();
    }

    show(item: GetPbLinkProForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
