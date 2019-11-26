import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import {GetPbEbookForViewDto, PbEbookDto, PbEbooksServiceProxy} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-detailebook',
  templateUrl: './detailebook.component.html',
  styleUrls: ['./detailebook.component.css']
})
export class DetailebookComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPbEbookForViewDto;


    constructor(
        private route: ActivatedRoute,
        private _pbEbooksServiceProxy: PbEbooksServiceProxy,
        private router: Router,
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPbEbookForViewDto();
        this.item.pbEbook = new PbEbookDto();
    }
    ngOnInit() {
        const itemId = +this.route.snapshot.params['id'];
        this.item= this._pbEbooksServiceProxy.getPbEbookForView(itemId)[0];
    }

}
