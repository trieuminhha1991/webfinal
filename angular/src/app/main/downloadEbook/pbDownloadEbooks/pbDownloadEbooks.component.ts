import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PbDownloadEbooksServiceProxy, PbDownloadEbookDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPbDownloadEbookModalComponent } from './create-or-edit-pbDownloadEbook-modal.component';
import { ViewPbDownloadEbookModalComponent } from './view-pbDownloadEbook-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pbDownloadEbooks.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PbDownloadEbooksComponent extends AppComponentBase {

    @ViewChild('createOrEditPbDownloadEbookModal', { static: true }) createOrEditPbDownloadEbookModal: CreateOrEditPbDownloadEbookModalComponent;
    @ViewChild('viewPbDownloadEbookModalComponent', { static: true }) viewPbDownloadEbookModal: ViewPbDownloadEbookModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxNumberFilter : number;
		maxNumberFilterEmpty : number;
		minNumberFilter : number;
		minNumberFilterEmpty : number;
    maxMonthFilter : moment.Moment;
		minMonthFilter : moment.Moment;
        pbEbookEbookNameFilter = '';




    constructor(
        injector: Injector,
        private _pbDownloadEbooksServiceProxy: PbDownloadEbooksServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPbDownloadEbooks(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pbDownloadEbooksServiceProxy.getAll(
            this.filterText,
            this.maxNumberFilter == null ? this.maxNumberFilterEmpty: this.maxNumberFilter,
            this.minNumberFilter == null ? this.minNumberFilterEmpty: this.minNumberFilter,
            this.maxMonthFilter,
            this.minMonthFilter,
            this.pbEbookEbookNameFilter,
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

    createPbDownloadEbook(): void {
        this.createOrEditPbDownloadEbookModal.show();
    }

    deletePbDownloadEbook(pbDownloadEbook: PbDownloadEbookDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pbDownloadEbooksServiceProxy.delete(pbDownloadEbook.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._pbDownloadEbooksServiceProxy.getPbDownloadEbooksToExcel(
        this.filterText,
            this.maxNumberFilter == null ? this.maxNumberFilterEmpty: this.maxNumberFilter,
            this.minNumberFilter == null ? this.minNumberFilterEmpty: this.minNumberFilter,
            this.maxMonthFilter,
            this.minMonthFilter,
            this.pbEbookEbookNameFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
