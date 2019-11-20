import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbTypeEbooksServiceProxy, CreateOrEditPbTypeEbookDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPbTypeEbookModal',
    templateUrl: './create-or-edit-pbTypeEbook-modal.component.html'
})
export class CreateOrEditPbTypeEbookModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbTypeEbook: CreateOrEditPbTypeEbookDto = new CreateOrEditPbTypeEbookDto();



    constructor(
        injector: Injector,
        private _pbTypeEbooksServiceProxy: PbTypeEbooksServiceProxy
    ) {
        super(injector);
    }

    show(pbTypeEbookId?: number): void {

        if (!pbTypeEbookId) {
            this.pbTypeEbook = new CreateOrEditPbTypeEbookDto();
            this.pbTypeEbook.id = pbTypeEbookId;

            this.active = true;
            this.modal.show();
        } else {
            this._pbTypeEbooksServiceProxy.getPbTypeEbookForEdit(pbTypeEbookId).subscribe(result => {
                this.pbTypeEbook = result.pbTypeEbook;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbTypeEbooksServiceProxy.createOrEdit(this.pbTypeEbook)
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
