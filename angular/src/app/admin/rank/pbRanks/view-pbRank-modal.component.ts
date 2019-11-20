import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbRankForViewDto, PbRankDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbRankModal',
    templateUrl: './view-pbRank-modal.component.html'
})
export class ViewPbRankModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbRankForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbRankForViewDto();
        this.item.pbRank = new PbRankDto();
    }

    show(item: GetPbRankForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
