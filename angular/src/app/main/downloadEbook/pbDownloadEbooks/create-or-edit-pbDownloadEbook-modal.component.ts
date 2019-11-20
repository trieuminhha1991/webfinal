import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbDownloadEbooksServiceProxy, CreateOrEditPbDownloadEbookDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { PbDownloadEbookPbEbookLookupTableModalComponent } from './pbDownloadEbook-pbEbook-lookup-table-modal.component';


@Component({
    selector: 'createOrEditPbDownloadEbookModal',
    templateUrl: './create-or-edit-pbDownloadEbook-modal.component.html'
})
export class CreateOrEditPbDownloadEbookModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('pbDownloadEbookPbEbookLookupTableModal', { static: true }) pbDownloadEbookPbEbookLookupTableModal: PbDownloadEbookPbEbookLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbDownloadEbook: CreateOrEditPbDownloadEbookDto = new CreateOrEditPbDownloadEbookDto();

    pbEbookEbookName = '';


    constructor(
        injector: Injector,
        private _pbDownloadEbooksServiceProxy: PbDownloadEbooksServiceProxy
    ) {
        super(injector);
    }

    show(pbDownloadEbookId?: number): void {

        if (!pbDownloadEbookId) {
            this.pbDownloadEbook = new CreateOrEditPbDownloadEbookDto();
            this.pbDownloadEbook.id = pbDownloadEbookId;
            this.pbDownloadEbook.month = moment().startOf('day');
            this.pbEbookEbookName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._pbDownloadEbooksServiceProxy.getPbDownloadEbookForEdit(pbDownloadEbookId).subscribe(result => {
                this.pbDownloadEbook = result.pbDownloadEbook;

                this.pbEbookEbookName = result.pbEbookEbookName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbDownloadEbooksServiceProxy.createOrEdit(this.pbDownloadEbook)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectPbEbookModal() {
        this.pbDownloadEbookPbEbookLookupTableModal.id = this.pbDownloadEbook.pbEbookId;
        this.pbDownloadEbookPbEbookLookupTableModal.displayName = this.pbEbookEbookName;
        this.pbDownloadEbookPbEbookLookupTableModal.show();
    }


        setPbEbookIdNull() {
        this.pbDownloadEbook.pbEbookId = null;
        this.pbEbookEbookName = '';
    }


        getNewPbEbookId() {
        this.pbDownloadEbook.pbEbookId = this.pbDownloadEbookPbEbookLookupTableModal.id;
        this.pbEbookEbookName = this.pbDownloadEbookPbEbookLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
