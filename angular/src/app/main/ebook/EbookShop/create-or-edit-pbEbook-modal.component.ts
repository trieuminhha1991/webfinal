import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbEbooksServiceProxy, CreateOrEditPbEbookDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ShopUserLookupTableModalComponent } from './pbEbook-user-lookup-table-modal.component';
import { ShopPbClassLookupTableModalComponent } from './pbEbook-pbClass-lookup-table-modal.component';
import { ShopPbPlaceLookupTableModalComponent } from './pbEbook-pbPlace-lookup-table-modal.component';
import { ShopPbRankLookupTableModalComponent } from './pbEbook-pbRank-lookup-table-modal.component';
import { ShopPbStatusLookupTableModalComponent } from './pbEbook-pbStatus-lookup-table-modal.component';
import { ShopPbSubjectLookupTableModalComponent } from './pbEbook-pbSubject-lookup-table-modal.component';
import { ShopPbSubjectEducationLookupTableModalComponent } from './pbEbook-pbSubjectEducation-lookup-table-modal.component';
import { ShopPbTypeEbookLookupTableModalComponent } from './pbEbook-pbTypeEbook-lookup-table-modal.component';
import { ShopPbTypeFileLookupTableModalComponent } from './pbEbook-pbTypeFile-lookup-table-modal.component';


@Component({
    selector: 'createOrEditPbEbookModal',
    templateUrl: './create-or-edit-pbEbook-modal.component.html'
})
export class CreateOrEditShopModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('pbEbookUserLookupTableModal', { static: true }) pbEbookUserLookupTableModal: ShopUserLookupTableModalComponent;
    @ViewChild('pbEbookPbClassLookupTableModal', { static: true }) pbEbookPbClassLookupTableModal: ShopPbClassLookupTableModalComponent;
    @ViewChild('pbEbookPbPlaceLookupTableModal', { static: true }) pbEbookPbPlaceLookupTableModal: ShopPbPlaceLookupTableModalComponent;
    @ViewChild('pbEbookPbRankLookupTableModal', { static: true }) pbEbookPbRankLookupTableModal: ShopPbRankLookupTableModalComponent;
    @ViewChild('pbEbookPbStatusLookupTableModal', { static: true }) pbEbookPbStatusLookupTableModal: ShopPbStatusLookupTableModalComponent;
    @ViewChild('pbEbookPbSubjectLookupTableModal', { static: true }) pbEbookPbSubjectLookupTableModal: ShopPbSubjectLookupTableModalComponent;
    @ViewChild('pbEbookPbSubjectEducationLookupTableModal', { static: true }) pbEbookPbSubjectEducationLookupTableModal: ShopPbSubjectEducationLookupTableModalComponent;
    @ViewChild('pbEbookPbTypeEbookLookupTableModal', { static: true }) pbEbookPbTypeEbookLookupTableModal: ShopPbTypeEbookLookupTableModalComponent;
    @ViewChild('pbEbookPbTypeFileLookupTableModal', { static: true }) pbEbookPbTypeFileLookupTableModal: ShopPbTypeFileLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbEbook: CreateOrEditPbEbookDto = new CreateOrEditPbEbookDto();

            ebookDateStart: Date;
    userName = '';
    pbClassClassName = '';
    pbPlacePlaceName = '';
    pbRankRankName = '';
    pbStatusStatusName = '';
    pbSubjectSectionName = '';
    pbSubjectEducationSubjectName = '';
    pbTypeEbookTypeName = '';
    pbTypeFileTypeFileName = '';


    constructor(
        injector: Injector,
        private _pbEbooksServiceProxy: PbEbooksServiceProxy
    ) {
        super(injector);
    }

    show(pbEbookId?: number): void {
this.ebookDateStart = null;

        if (!pbEbookId) {
            this.pbEbook = new CreateOrEditPbEbookDto();
            this.pbEbook.id = pbEbookId;
            this.userName = '';
            this.pbClassClassName = '';
            this.pbPlacePlaceName = '';
            this.pbRankRankName = '';
            this.pbStatusStatusName = '';
            this.pbSubjectSectionName = '';
            this.pbSubjectEducationSubjectName = '';
            this.pbTypeEbookTypeName = '';
            this.pbTypeFileTypeFileName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._pbEbooksServiceProxy.getPbEbookForEdit(pbEbookId).subscribe(result => {
                this.pbEbook = result.pbEbook;

                if (this.pbEbook.ebookDateStart) {
					this.ebookDateStart = this.pbEbook.ebookDateStart.toDate();
                }
                this.userName = result.userName;
                this.pbClassClassName = result.pbClassClassName;
                this.pbPlacePlaceName = result.pbPlacePlaceName;
                this.pbRankRankName = result.pbRankRankName;
                this.pbStatusStatusName = result.pbStatusStatusName;
                this.pbSubjectSectionName = result.pbSubjectSectionName;
                this.pbSubjectEducationSubjectName = result.pbSubjectEducationSubjectName;
                this.pbTypeEbookTypeName = result.pbTypeEbookTypeName;
                this.pbTypeFileTypeFileName = result.pbTypeFileTypeFileName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
        if (this.ebookDateStart) {
            if (!this.pbEbook.ebookDateStart) {
                this.pbEbook.ebookDateStart = moment(this.ebookDateStart).startOf('day');
            }
            else {
                this.pbEbook.ebookDateStart = moment(this.ebookDateStart);
            }
        }
        else {
            this.pbEbook.ebookDateStart = null;
        }
            this._pbEbooksServiceProxy.createOrEdit(this.pbEbook)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectUserModal() {
        this.pbEbookUserLookupTableModal.id = this.pbEbook.userId;
        this.pbEbookUserLookupTableModal.displayName = this.userName;
        this.pbEbookUserLookupTableModal.show();
    }
        openSelectPbClassModal() {
        this.pbEbookPbClassLookupTableModal.id = this.pbEbook.pbClassId;
        this.pbEbookPbClassLookupTableModal.displayName = this.pbClassClassName;
        this.pbEbookPbClassLookupTableModal.show();
    }
        openSelectPbPlaceModal() {
        this.pbEbookPbPlaceLookupTableModal.id = this.pbEbook.pbPlaceId;
        this.pbEbookPbPlaceLookupTableModal.displayName = this.pbPlacePlaceName;
        this.pbEbookPbPlaceLookupTableModal.show();
    }
        openSelectPbRankModal() {
        this.pbEbookPbRankLookupTableModal.id = this.pbEbook.pbRankId;
        this.pbEbookPbRankLookupTableModal.displayName = this.pbRankRankName;
        this.pbEbookPbRankLookupTableModal.show();
    }
        openSelectPbStatusModal() {
        this.pbEbookPbStatusLookupTableModal.id = this.pbEbook.pbStatusId;
        this.pbEbookPbStatusLookupTableModal.displayName = this.pbStatusStatusName;
        this.pbEbookPbStatusLookupTableModal.show();
    }
        openSelectPbSubjectModal() {
        this.pbEbookPbSubjectLookupTableModal.id = this.pbEbook.pbSubjectId;
        this.pbEbookPbSubjectLookupTableModal.displayName = this.pbSubjectSectionName;
        this.pbEbookPbSubjectLookupTableModal.show();
    }
        openSelectPbSubjectEducationModal() {
        this.pbEbookPbSubjectEducationLookupTableModal.id = this.pbEbook.pbSubjectEducationId;
        this.pbEbookPbSubjectEducationLookupTableModal.displayName = this.pbSubjectEducationSubjectName;
        this.pbEbookPbSubjectEducationLookupTableModal.show();
    }
        openSelectPbTypeEbookModal() {
        this.pbEbookPbTypeEbookLookupTableModal.id = this.pbEbook.pbTypeEbookId;
        this.pbEbookPbTypeEbookLookupTableModal.displayName = this.pbTypeEbookTypeName;
        this.pbEbookPbTypeEbookLookupTableModal.show();
    }
        openSelectPbTypeFileModal() {
        this.pbEbookPbTypeFileLookupTableModal.id = this.pbEbook.pbTypeFileId;
        this.pbEbookPbTypeFileLookupTableModal.displayName = this.pbTypeFileTypeFileName;
        this.pbEbookPbTypeFileLookupTableModal.show();
    }


        setUserIdNull() {
        this.pbEbook.userId = null;
        this.userName = '';
    }
        setPbClassIdNull() {
        this.pbEbook.pbClassId = null;
        this.pbClassClassName = '';
    }
        setPbPlaceIdNull() {
        this.pbEbook.pbPlaceId = null;
        this.pbPlacePlaceName = '';
    }
        setPbRankIdNull() {
        this.pbEbook.pbRankId = null;
        this.pbRankRankName = '';
    }
        setPbStatusIdNull() {
        this.pbEbook.pbStatusId = null;
        this.pbStatusStatusName = '';
    }
        setPbSubjectIdNull() {
        this.pbEbook.pbSubjectId = null;
        this.pbSubjectSectionName = '';
    }
        setPbSubjectEducationIdNull() {
        this.pbEbook.pbSubjectEducationId = null;
        this.pbSubjectEducationSubjectName = '';
    }
        setPbTypeEbookIdNull() {
        this.pbEbook.pbTypeEbookId = null;
        this.pbTypeEbookTypeName = '';
    }
        setPbTypeFileIdNull() {
        this.pbEbook.pbTypeFileId = null;
        this.pbTypeFileTypeFileName = '';
    }


        getNewUserId() {
        this.pbEbook.userId = this.pbEbookUserLookupTableModal.id;
        this.userName = this.pbEbookUserLookupTableModal.displayName;
    }
        getNewPbClassId() {
        this.pbEbook.pbClassId = this.pbEbookPbClassLookupTableModal.id;
        this.pbClassClassName = this.pbEbookPbClassLookupTableModal.displayName;
    }
        getNewPbPlaceId() {
        this.pbEbook.pbPlaceId = this.pbEbookPbPlaceLookupTableModal.id;
        this.pbPlacePlaceName = this.pbEbookPbPlaceLookupTableModal.displayName;
    }
        getNewPbRankId() {
        this.pbEbook.pbRankId = this.pbEbookPbRankLookupTableModal.id;
        this.pbRankRankName = this.pbEbookPbRankLookupTableModal.displayName;
    }
        getNewPbStatusId() {
        this.pbEbook.pbStatusId = this.pbEbookPbStatusLookupTableModal.id;
        this.pbStatusStatusName = this.pbEbookPbStatusLookupTableModal.displayName;
    }
        getNewPbSubjectId() {
        this.pbEbook.pbSubjectId = this.pbEbookPbSubjectLookupTableModal.id;
        this.pbSubjectSectionName = this.pbEbookPbSubjectLookupTableModal.displayName;
    }
        getNewPbSubjectEducationId() {
        this.pbEbook.pbSubjectEducationId = this.pbEbookPbSubjectEducationLookupTableModal.id;
        this.pbSubjectEducationSubjectName = this.pbEbookPbSubjectEducationLookupTableModal.displayName;
    }
        getNewPbTypeEbookId() {
        this.pbEbook.pbTypeEbookId = this.pbEbookPbTypeEbookLookupTableModal.id;
        this.pbTypeEbookTypeName = this.pbEbookPbTypeEbookLookupTableModal.displayName;
    }
        getNewPbTypeFileId() {
        this.pbEbook.pbTypeFileId = this.pbEbookPbTypeFileLookupTableModal.id;
        this.pbTypeFileTypeFileName = this.pbEbookPbTypeFileLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
