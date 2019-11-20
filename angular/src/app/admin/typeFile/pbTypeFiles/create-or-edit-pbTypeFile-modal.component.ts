import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbTypeFilesServiceProxy, CreateOrEditPbTypeFileDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPbTypeFileModal',
    templateUrl: './create-or-edit-pbTypeFile-modal.component.html'
})
export class CreateOrEditPbTypeFileModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbTypeFile: CreateOrEditPbTypeFileDto = new CreateOrEditPbTypeFileDto();



    constructor(
        injector: Injector,
        private _pbTypeFilesServiceProxy: PbTypeFilesServiceProxy
    ) {
        super(injector);
    }

    show(pbTypeFileId?: number): void {

        if (!pbTypeFileId) {
            this.pbTypeFile = new CreateOrEditPbTypeFileDto();
            this.pbTypeFile.id = pbTypeFileId;

            this.active = true;
            this.modal.show();
        } else {
            this._pbTypeFilesServiceProxy.getPbTypeFileForEdit(pbTypeFileId).subscribe(result => {
                this.pbTypeFile = result.pbTypeFile;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbTypeFilesServiceProxy.createOrEdit(this.pbTypeFile)
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
