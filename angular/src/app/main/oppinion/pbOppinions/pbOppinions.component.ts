import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PbOppinionsServiceProxy, PbOppinionDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPbOppinionModalComponent } from './create-or-edit-pbOppinion-modal.component';
import { ViewPbOppinionModalComponent } from './view-pbOppinion-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pbOppinions.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PbOppinionsComponent extends AppComponentBase {

    @ViewChild('createOrEditPbOppinionModal', { static: true }) createOrEditPbOppinionModal: CreateOrEditPbOppinionModalComponent;
    @ViewChild('viewPbOppinionModalComponent', { static: true }) viewPbOppinionModal: ViewPbOppinionModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    contentFilter = '';
        userNameFilter = '';
        pbEbookEbookNameFilter = '';




    constructor(
        injector: Injector,
        private _pbOppinionsServiceProxy: PbOppinionsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPbOppinions(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pbOppinionsServiceProxy.getAll(
            this.filterText,
            this.contentFilter,
            this.userNameFilter,
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

    createPbOppinion(): void {
        this.createOrEditPbOppinionModal.show();
    }

    deletePbOppinion(pbOppinion: PbOppinionDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pbOppinionsServiceProxy.delete(pbOppinion.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._pbOppinionsServiceProxy.getPbOppinionsToExcel(
        this.filterText,
            this.contentFilter,
            this.userNameFilter,
            this.pbEbookEbookNameFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
