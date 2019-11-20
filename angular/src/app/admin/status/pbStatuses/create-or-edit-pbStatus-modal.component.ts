import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbStatusesServiceProxy, CreateOrEditPbStatusDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPbStatusModal',
    templateUrl: './create-or-edit-pbStatus-modal.component.html'
})
export class CreateOrEditPbStatusModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbStatus: CreateOrEditPbStatusDto = new CreateOrEditPbStatusDto();



    constructor(
        injector: Injector,
        private _pbStatusesServiceProxy: PbStatusesServiceProxy
    ) {
        super(injector);
    }

    show(pbStatusId?: number): void {

        if (!pbStatusId) {
            this.pbStatus = new CreateOrEditPbStatusDto();
            this.pbStatus.id = pbStatusId;

            this.active = true;
            this.modal.show();
        } else {
            this._pbStatusesServiceProxy.getPbStatusForEdit(pbStatusId).subscribe(result => {
                this.pbStatus = result.pbStatus;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbStatusesServiceProxy.createOrEdit(this.pbStatus)
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
