import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPbSubjectEducationForViewDto, PbSubjectEducationDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPbSubjectEducationModal',
    templateUrl: './view-pbSubjectEducation-modal.component.html'
})
export class ViewPbSubjectEducationModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbSubjectEducationForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbSubjectEducationForViewDto();
        this.item.pbSubjectEducation = new PbSubjectEducationDto();
    }

    show(item: GetPbSubjectEducationForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
