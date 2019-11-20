import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbPriceUsersServiceProxy, CreateOrEditPbPriceUserDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { PbPriceUserUserLookupTableModalComponent } from './pbPriceUser-user-lookup-table-modal.component';


@Component({
    selector: 'createOrEditPbPriceUserModal',
    templateUrl: './create-or-edit-pbPriceUser-modal.component.html'
})
export class CreateOrEditPbPriceUserModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('pbPriceUserUserLookupTableModal', { static: true }) pbPriceUserUserLookupTableModal: PbPriceUserUserLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbPriceUser: CreateOrEditPbPriceUserDto = new CreateOrEditPbPriceUserDto();

            month: Date;
    userName = '';


    constructor(
        injector: Injector,
        private _pbPriceUsersServiceProxy: PbPriceUsersServiceProxy
    ) {
        super(injector);
    }

    show(pbPriceUserId?: number): void {
this.month = null;

        if (!pbPriceUserId) {
            this.pbPriceUser = new CreateOrEditPbPriceUserDto();
            this.pbPriceUser.id = pbPriceUserId;
            this.userName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._pbPriceUsersServiceProxy.getPbPriceUserForEdit(pbPriceUserId).subscribe(result => {
                this.pbPriceUser = result.pbPriceUser;

                if (this.pbPriceUser.month) {
					this.month = this.pbPriceUser.month.toDate();
                }
                this.userName = result.userName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
        if (this.month) {
            if (!this.pbPriceUser.month) {
                this.pbPriceUser.month = moment(this.month).startOf('day');
            }
            else {
                this.pbPriceUser.month = moment(this.month);
            }
        }
        else {
            this.pbPriceUser.month = null;
        }
            this._pbPriceUsersServiceProxy.createOrEdit(this.pbPriceUser)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectUserModal() {
        this.pbPriceUserUserLookupTableModal.id = this.pbPriceUser.userId;
        this.pbPriceUserUserLookupTableModal.displayName = this.userName;
        this.pbPriceUserUserLookupTableModal.show();
    }


        setUserIdNull() {
        this.pbPriceUser.userId = null;
        this.userName = '';
    }


        getNewUserId() {
        this.pbPriceUser.userId = this.pbPriceUserUserLookupTableModal.id;
        this.userName = this.pbPriceUserUserLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
