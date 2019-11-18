import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbSubjectsServiceProxy, CreateOrEditPbSubjectDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPbSubjectModal',
    templateUrl: './create-or-edit-pbSubject-modal.component.html'
})
export class CreateOrEditPbSubjectModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbSubject: CreateOrEditPbSubjectDto = new CreateOrEditPbSubjectDto();



    constructor(
        injector: Injector,
        private _pbSubjectsServiceProxy: PbSubjectsServiceProxy
    ) {
        super(injector);
    }

    show(pbSubjectId?: number): void {

        if (!pbSubjectId) {
            this.pbSubject = new CreateOrEditPbSubjectDto();
            this.pbSubject.id = pbSubjectId;

            this.active = true;
            this.modal.show();
        } else {
            this._pbSubjectsServiceProxy.getPbSubjectForEdit(pbSubjectId).subscribe(result => {
                this.pbSubject = result.pbSubject;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbSubjectsServiceProxy.createOrEdit(this.pbSubject)
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
