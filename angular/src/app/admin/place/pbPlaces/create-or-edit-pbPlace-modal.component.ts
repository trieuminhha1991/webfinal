import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PbPlacesServiceProxy, CreateOrEditPbPlaceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPbPlaceModal',
    templateUrl: './create-or-edit-pbPlace-modal.component.html'
})
export class CreateOrEditPbPlaceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pbPlace: CreateOrEditPbPlaceDto = new CreateOrEditPbPlaceDto();



    constructor(
        injector: Injector,
        private _pbPlacesServiceProxy: PbPlacesServiceProxy
    ) {
        super(injector);
    }

    show(pbPlaceId?: number): void {

        if (!pbPlaceId) {
            this.pbPlace = new CreateOrEditPbPlaceDto();
            this.pbPlace.id = pbPlaceId;

            this.active = true;
            this.modal.show();
        } else {
            this._pbPlacesServiceProxy.getPbPlaceForEdit(pbPlaceId).subscribe(result => {
                this.pbPlace = result.pbPlace;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pbPlacesServiceProxy.createOrEdit(this.pbPlace)
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
