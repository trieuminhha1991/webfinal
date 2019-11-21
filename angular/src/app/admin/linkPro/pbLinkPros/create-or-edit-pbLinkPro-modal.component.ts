import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbLinkProsServiceProxy, CreateOrEditPbLinkProDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { PbLinkProPbEbookLookupTableModalComponent } from './pbLinkPro-pbEbook-lookup-table-modal.component';


@Component({
    selector: 'createOrEditPbLinkProModal',
    templateUrl: './create-or-edit-pbLinkPro-modal.component.html'
})
export class CreateOrEditPbLinkProModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('pbLinkProPbEbookLookupTableModal', { static: true }) pbLinkProPbEbookLookupTableModal: PbLinkProPbEbookLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbLinkPro: CreateOrEditPbLinkProDto = new CreateOrEditPbLinkProDto();

    pbEbookEbookName = '';


    constructor(
        injector: Injector,
        private _pbLinkProsServiceProxy: PbLinkProsServiceProxy
    ) {
        super(injector);
    }

    show(pbLinkProId?: number): void {

        if (!pbLinkProId) {
            this.pbLinkPro = new CreateOrEditPbLinkProDto();
            this.pbLinkPro.id = pbLinkProId;
            this.pbEbookEbookName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._pbLinkProsServiceProxy.getPbLinkProForEdit(pbLinkProId).subscribe(result => {
                this.pbLinkPro = result.pbLinkPro;

                this.pbEbookEbookName = result.pbEbookEbookName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbLinkProsServiceProxy.createOrEdit(this.pbLinkPro)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectPbEbookModal() {
        this.pbLinkProPbEbookLookupTableModal.id = this.pbLinkPro.pbEbookId;
        this.pbLinkProPbEbookLookupTableModal.displayName = this.pbEbookEbookName;
        this.pbLinkProPbEbookLookupTableModal.show();
    }


        setPbEbookIdNull() {
        this.pbLinkPro.pbEbookId = null;
        this.pbEbookEbookName = '';
    }


        getNewPbEbookId() {
        this.pbLinkPro.pbEbookId = this.pbLinkProPbEbookLookupTableModal.id;
        this.pbEbookEbookName = this.pbLinkProPbEbookLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
