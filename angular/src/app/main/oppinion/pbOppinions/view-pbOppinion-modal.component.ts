import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbOppinionForViewDto, PbOppinionDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbOppinionModal',
    templateUrl: './view-pbOppinion-modal.component.html'
})
export class ViewPbOppinionModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbOppinionForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbOppinionForViewDto();
        this.item.pbOppinion = new PbOppinionDto();
    }

    show(item: GetPbOppinionForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
