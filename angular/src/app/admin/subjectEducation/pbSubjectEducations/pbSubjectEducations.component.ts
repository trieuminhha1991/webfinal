import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PbSubjectEducationsServiceProxy, PbSubjectEducationDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPbSubjectEducationModalComponent } from './create-or-edit-pbSubjectEducation-modal.component';
import { ViewPbSubjectEducationModalComponent } from './view-pbSubjectEducation-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pbSubjectEducations.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PbSubjectEducationsComponent extends AppComponentBase {

    @ViewChild('createOrEditPbSubjectEducationModal', { static: true }) createOrEditPbSubjectEducationModal: CreateOrEditPbSubjectEducationModalComponent;
    @ViewChild('viewPbSubjectEducationModalComponent', { static: true }) viewPbSubjectEducationModal: ViewPbSubjectEducationModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    subjectNameFilter = '';
    descriptionFilter = '';




    constructor(
        injector: Injector,
        private _pbSubjectEducationsServiceProxy: PbSubjectEducationsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPbSubjectEducations(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pbSubjectEducationsServiceProxy.getAll(
            this.filterText,
            this.subjectNameFilter,
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

    createPbSubjectEducation(): void {
        this.createOrEditPbSubjectEducationModal.show();
    }

    deletePbSubjectEducation(pbSubjectEducation: PbSubjectEducationDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pbSubjectEducationsServiceProxy.delete(pbSubjectEducation.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._pbSubjectEducationsServiceProxy.getPbSubjectEducationsToExcel(
        this.filterText,
            this.subjectNameFilter,
            this.descriptionFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
