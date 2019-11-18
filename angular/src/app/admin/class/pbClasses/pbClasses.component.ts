import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PbClassesServiceProxy, PbClassDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPbClassModalComponent } from './create-or-edit-pbClass-modal.component';
import { ViewPbClassModalComponent } from './view-pbClass-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pbClasses.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PbClassesComponent extends AppComponentBase {

    @ViewChild('createOrEditPbClassModal', { static: true }) createOrEditPbClassModal: CreateOrEditPbClassModalComponent;
    @ViewChild('viewPbClassModalComponent', { static: true }) viewPbClassModal: ViewPbClassModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    classGroupFilter = '';
    classNameFilter = '';




    constructor(
        injector: Injector,
        private _pbClassesServiceProxy: PbClassesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPbClasses(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pbClassesServiceProxy.getAll(
            this.filterText,
            this.classGroupFilter,
            this.classNameFilter,
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

    createPbClass(): void {
        this.createOrEditPbClassModal.show();
    }

    deletePbClass(pbClass: PbClassDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pbClassesServiceProxy.delete(pbClass.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._pbClassesServiceProxy.getPbClassesToExcel(
        this.filterText,
            this.classGroupFilter,
            this.classNameFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
