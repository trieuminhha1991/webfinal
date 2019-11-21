import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PbLinkProsServiceProxy, PbLinkProDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPbLinkProModalComponent } from './create-or-edit-pbLinkPro-modal.component';
import { ViewPbLinkProModalComponent } from './view-pbLinkPro-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pbLinkPros.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PbLinkProsComponent extends AppComponentBase {

    @ViewChild('createOrEditPbLinkProModal', { static: true }) createOrEditPbLinkProModal: CreateOrEditPbLinkProModalComponent;
    @ViewChild('viewPbLinkProModalComponent', { static: true }) viewPbLinkProModal: ViewPbLinkProModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    linkNameFilter = '';
        pbEbookEbookNameFilter = '';




    constructor(
        injector: Injector,
        private _pbLinkProsServiceProxy: PbLinkProsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPbLinkPros(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pbLinkProsServiceProxy.getAll(
            this.filterText,
            this.linkNameFilter,
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

    createPbLinkPro(): void {
        this.createOrEditPbLinkProModal.show();
    }

    deletePbLinkPro(pbLinkPro: PbLinkProDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pbLinkProsServiceProxy.delete(pbLinkPro.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._pbLinkProsServiceProxy.getPbLinkProsToExcel(
        this.filterText,
            this.linkNameFilter,
            this.pbEbookEbookNameFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
