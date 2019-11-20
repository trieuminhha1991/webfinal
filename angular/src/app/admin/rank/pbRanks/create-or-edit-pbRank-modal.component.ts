import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbRanksServiceProxy, CreateOrEditPbRankDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPbRankModal',
    templateUrl: './create-or-edit-pbRank-modal.component.html'
})
export class CreateOrEditPbRankModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbRank: CreateOrEditPbRankDto = new CreateOrEditPbRankDto();



    constructor(
        injector: Injector,
        private _pbRanksServiceProxy: PbRanksServiceProxy
    ) {
        super(injector);
    }

    show(pbRankId?: number): void {

        if (!pbRankId) {
            this.pbRank = new CreateOrEditPbRankDto();
            this.pbRank.id = pbRankId;

            this.active = true;
            this.modal.show();
        } else {
            this._pbRanksServiceProxy.getPbRankForEdit(pbRankId).subscribe(result => {
                this.pbRank = result.pbRank;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbRanksServiceProxy.createOrEdit(this.pbRank)
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
