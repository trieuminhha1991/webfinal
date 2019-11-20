import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PbTypeEbooksServiceProxy, PbTypeEbookDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPbTypeEbookModalComponent } from './create-or-edit-pbTypeEbook-modal.component';
import { ViewPbTypeEbookModalComponent } from './view-pbTypeEbook-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pbTypeEbooks.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PbTypeEbooksComponent extends AppComponentBase {

    @ViewChild('createOrEditPbTypeEbookModal', { static: true }) createOrEditPbTypeEbookModal: CreateOrEditPbTypeEbookModalComponent;
    @ViewChild('viewPbTypeEbookModalComponent', { static: true }) viewPbTypeEbookModal: ViewPbTypeEbookModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    typeNameFilter = '';
    descriptionFilter = '';




    constructor(
        injector: Injector,
        private _pbTypeEbooksServiceProxy: PbTypeEbooksServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPbTypeEbooks(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pbTypeEbooksServiceProxy.getAll(
            this.filterText,
            this.typeNameFilter,
            this.descriptionFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createPbTypeEbook(): void {
        this.createOrEditPbTypeEbookModal.show();
    }

    deletePbTypeEbook(pbTypeEbook: PbTypeEbookDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pbTypeEbooksServiceProxy.delete(pbTypeEbook.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._pbTypeEbooksServiceProxy.getPbTypeEbooksToExcel(
        this.filterText,
            this.typeNameFilter,
            this.descriptionFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
