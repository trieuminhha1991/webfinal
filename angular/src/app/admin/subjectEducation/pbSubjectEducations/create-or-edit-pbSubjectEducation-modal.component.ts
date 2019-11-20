import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbSubjectEducationsServiceProxy, CreateOrEditPbSubjectEducationDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPbSubjectEducationModal',
    templateUrl: './create-or-edit-pbSubjectEducation-modal.component.html'
})
export class CreateOrEditPbSubjectEducationModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbSubjectEducation: CreateOrEditPbSubjectEducationDto = new CreateOrEditPbSubjectEducationDto();



    constructor(
        injector: Injector,
        private _pbSubjectEducationsServiceProxy: PbSubjectEducationsServiceProxy
    ) {
        super(injector);
    }

    show(pbSubjectEducationId?: number): void {

        if (!pbSubjectEducationId) {
            this.pbSubjectEducation = new CreateOrEditPbSubjectEducationDto();
            this.pbSubjectEducation.id = pbSubjectEducationId;

            this.active = true;
            this.modal.show();
        } else {
            this._pbSubjectEducationsServiceProxy.getPbSubjectEducationForEdit(pbSubjectEducationId).subscribe(result => {
                this.pbSubjectEducation = result.pbSubjectEducation;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbSubjectEducationsServiceProxy.createOrEdit(this.pbSubjectEducation)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
