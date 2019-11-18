import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PbSubjectsServiceProxy, PbSubjectDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPbSubjectModalComponent } from './create-or-edit-pbSubject-modal.component';
import { ViewPbSubjectModalComponent } from './view-pbSubject-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pbSubjects.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PbSubjectsComponent extends AppComponentBase {

    @ViewChild('createOrEditPbSubjectModal', { static: true }) createOrEditPbSubjectModal: CreateOrEditPbSubjectModalComponent;
    @ViewChild('viewPbSubjectModalComponent', { static: true }) viewPbSubjectModal: ViewPbSubjectModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    classNameFilter = '';
    objectNameFilter = '';
    chapterNameFilter = '';
    sectionNameFilter = '';




    constructor(
        injector: Injector,
        private _pbSubjectsServiceProxy: PbSubjectsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPbSubjects(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pbSubjectsServiceProxy.getAll(
            this.filterText,
            this.classNameFilter,
            this.objectNameFilter,
            this.chapterNameFilter,
            this.sectionNameFilter,
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

    createPbSubject(): void {
        this.createOrEditPbSubjectModal.show();
    }

    deletePbSubject(pbSubject: PbSubjectDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pbSubjectsServiceProxy.delete(pbSubject.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._pbSubjectsServiceProxy.getPbSubjectsToExcel(
        this.filterText,
            this.classNameFilter,
            this.objectNameFilter,
            this.chapterNameFilter,
            this.sectionNameFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
