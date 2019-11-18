import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbClassesServiceProxy, CreateOrEditPbClassDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPbClassModal',
    templateUrl: './create-or-edit-pbClass-modal.component.html'
})
export class CreateOrEditPbClassModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbClass: CreateOrEditPbClassDto = new CreateOrEditPbClassDto();



    constructor(
        injector: Injector,
        private _pbClassesServiceProxy: PbClassesServiceProxy
    ) {
        super(injector);
    }

    show(pbClassId?: number): void {

        if (!pbClassId) {
            this.pbClass = new CreateOrEditPbClassDto();
            this.pbClass.id = pbClassId;

            this.active = true;
            this.modal.show();
        } else {
            this._pbClassesServiceProxy.getPbClassForEdit(pbClassId).subscribe(result => {
                this.pbClass = result.pbClass;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbClassesServiceProxy.createOrEdit(this.pbClass)
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
