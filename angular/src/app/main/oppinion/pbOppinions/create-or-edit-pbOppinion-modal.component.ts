import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbOppinionsServiceProxy, CreateOrEditPbOppinionDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { PbOppinionUserLookupTableModalComponent } from './pbOppinion-user-lookup-table-modal.component';
import { PbOppinionPbEbookLookupTableModalComponent } from './pbOppinion-pbEbook-lookup-table-modal.component';


@Component({
    selector: 'createOrEditPbOppinionModal',
    templateUrl: './create-or-edit-pbOppinion-modal.component.html'
})
export class CreateOrEditPbOppinionModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('pbOppinionUserLookupTableModal', { static: true }) pbOppinionUserLookupTableModal: PbOppinionUserLookupTableModalComponent;
    @ViewChild('pbOppinionPbEbookLookupTableModal', { static: true }) pbOppinionPbEbookLookupTableModal: PbOppinionPbEbookLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbOppinion: CreateOrEditPbOppinionDto = new CreateOrEditPbOppinionDto();

    userName = '';
    pbEbookEbookName = '';


    constructor(
        injector: Injector,
        private _pbOppinionsServiceProxy: PbOppinionsServiceProxy
    ) {
        super(injector);
    }

    show(pbOppinionId?: number): void {

        if (!pbOppinionId) {
            this.pbOppinion = new CreateOrEditPbOppinionDto();
            this.pbOppinion.id = pbOppinionId;
            this.userName = '';
            this.pbEbookEbookName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._pbOppinionsServiceProxy.getPbOppinionForEdit(pbOppinionId).subscribe(result => {
                this.pbOppinion = result.pbOppinion;

                this.userName = result.userName;
                this.pbEbookEbookName = result.pbEbookEbookName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbOppinionsServiceProxy.createOrEdit(this.pbOppinion)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectUserModal() {
        this.pbOppinionUserLookupTableModal.id = this.pbOppinion.userId;
        this.pbOppinionUserLookupTableModal.displayName = this.userName;
        this.pbOppinionUserLookupTableModal.show();
    }
        openSelectPbEbookModal() {
        this.pbOppinionPbEbookLookupTableModal.id = this.pbOppinion.pbEbookId;
        this.pbOppinionPbEbookLookupTableModal.displayName = this.pbEbookEbookName;
        this.pbOppinionPbEbookLookupTableModal.show();
    }


        setUserIdNull() {
        this.pbOppinion.userId = null;
        this.userName = '';
    }
        setPbEbookIdNull() {
        this.pbOppinion.pbEbookId = null;
        this.pbEbookEbookName = '';
    }


        getNewUserId() {
        this.pbOppinion.userId = this.pbOppinionUserLookupTableModal.id;
        this.userName = this.pbOppinionUserLookupTableModal.displayName;
    }
        getNewPbEbookId() {
        this.pbOppinion.pbEbookId = this.pbOppinionPbEbookLookupTableModal.id;
        this.pbEbookEbookName = this.pbOppinionPbEbookLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
