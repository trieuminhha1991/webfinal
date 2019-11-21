import {Component, Injector, ViewEncapsulation, ViewChild} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {PbEbooksServiceProxy, PbEbookDto} from '@shared/service-proxies/service-proxies';
import {NotifyService} from '@abp/notify/notify.service';
import {AppComponentBase} from '@shared/common/app-component-base';
import {TokenAuthServiceProxy} from '@shared/service-proxies/service-proxies';
import {CreateOrEditPbEbookModalComponent} from './create-or-edit-pbEbook-modal.component';
import {ViewPbEbookModalComponent} from './view-pbEbook-modal.component';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {Table} from 'primeng/components/table/table';
import {Paginator} from 'primeng/components/paginator/paginator';
import {LazyLoadEvent} from 'primeng/components/common/lazyloadevent';
import {FileDownloadService} from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pbEbooks.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PbEbooksComponent extends AppComponentBase {

    @ViewChild('createOrEditPbEbookModal', {static: true}) createOrEditPbEbookModal: CreateOrEditPbEbookModalComponent;
    @ViewChild('viewPbEbookModalComponent', {static: true}) viewPbEbookModal: ViewPbEbookModalComponent;
    @ViewChild('dataTable', {static: true}) dataTable: Table;
    @ViewChild('paginator', {static: true}) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    ebookNameFilter = '';
    linkFilter = '';
    maxEbookDateStartFilter: moment.Moment;
    minEbookDateStartFilter: moment.Moment;
    proFilter = -1;
    maxEbookPriceFilter: number;
    maxEbookPriceFilterEmpty: number;
    minEbookPriceFilter: number;
    minEbookPriceFilterEmpty: number;
    maxEbookViewFilter: number;
    maxEbookViewFilterEmpty: number;
    minEbookViewFilter: number;
    minEbookViewFilterEmpty: number;
    maxEbookLikeFilter: number;
    maxEbookLikeFilterEmpty: number;
    minEbookLikeFilter: number;
    minEbookLikeFilterEmpty: number;
    maxEbookDislikeFilter: number;
    maxEbookDislikeFilterEmpty: number;
    minEbookDislikeFilter: number;
    minEbookDislikeFilterEmpty: number;
    discriptionFilter = '';
    ebookCoverFilter = '';
    maxBookPageFilter: number;
    maxBookPageFilterEmpty: number;
    minBookPageFilter: number;
    minBookPageFilterEmpty: number;
    userNameFilter = '';
    pbClassClassNameFilter = '';
    pbPlacePlaceNameFilter = '';
    pbRankRankNameFilter = '';
    pbStatusStatusNameFilter = '';
    pbSubjectSectionNameFilter = '';
    pbSubjectEducationSubjectNameFilter = '';
    pbTypeEbookTypeNameFilter = '';
    pbTypeFileTypeFileNameFilter = '';


    constructor(
        injector: Injector,
        private _pbEbooksServiceProxy: PbEbooksServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPbEbooks(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pbEbooksServiceProxy.getAll(
            this.filterText,
            this.ebookNameFilter,
            this.linkFilter,
            this.maxEbookDateStartFilter,
            this.minEbookDateStartFilter,
            this.proFilter,
            this.maxEbookPriceFilter == null ? this.maxEbookPriceFilterEmpty : this.maxEbookPriceFilter,
            this.minEbookPriceFilter == null ? this.minEbookPriceFilterEmpty : this.minEbookPriceFilter,
            this.maxEbookViewFilter == null ? this.maxEbookViewFilterEmpty : this.maxEbookViewFilter,
            this.minEbookViewFilter == null ? this.minEbookViewFilterEmpty : this.minEbookViewFilter,
            this.maxEbookLikeFilter == null ? this.maxEbookLikeFilterEmpty : this.maxEbookLikeFilter,
            this.minEbookLikeFilter == null ? this.minEbookLikeFilterEmpty : this.minEbookLikeFilter,
            this.maxEbookDislikeFilter == null ? this.maxEbookDislikeFilterEmpty : this.maxEbookDislikeFilter,
            this.minEbookDislikeFilter == null ? this.minEbookDislikeFilterEmpty : this.minEbookDislikeFilter,
            this.discriptionFilter,
            this.ebookCoverFilter,
            this.maxBookPageFilter == null ? this.maxBookPageFilterEmpty : this.maxBookPageFilter,
            this.minBookPageFilter == null ? this.minBookPageFilterEmpty : this.minBookPageFilter,
            this.userNameFilter,
            this.pbClassClassNameFilter,
            this.pbPlacePlaceNameFilter,
            this.pbRankRankNameFilter,
            this.pbStatusStatusNameFilter,
            this.pbSubjectSectionNameFilter,
            this.pbSubjectEducationSubjectNameFilter,
            this.pbTypeEbookTypeNameFilter,
            this.pbTypeFileTypeFileNameFilter,
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

    createPbEbook(): void {
        this.createOrEditPbEbookModal.show();
    }

    deletePbEbook(pbEbook: PbEbookDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pbEbooksServiceProxy.delete(pbEbook.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._pbEbooksServiceProxy.getPbEbooksToExcel(
            this.filterText,
            this.ebookNameFilter,
            this.linkFilter,
            this.maxEbookDateStartFilter,
            this.minEbookDateStartFilter,
            this.proFilter,
            this.maxEbookPriceFilter == null ? this.maxEbookPriceFilterEmpty : this.maxEbookPriceFilter,
            this.minEbookPriceFilter == null ? this.minEbookPriceFilterEmpty : this.minEbookPriceFilter,
            this.maxEbookViewFilter == null ? this.maxEbookViewFilterEmpty : this.maxEbookViewFilter,
            this.minEbookViewFilter == null ? this.minEbookViewFilterEmpty : this.minEbookViewFilter,
            this.maxEbookLikeFilter == null ? this.maxEbookLikeFilterEmpty : this.maxEbookLikeFilter,
            this.minEbookLikeFilter == null ? this.minEbookLikeFilterEmpty : this.minEbookLikeFilter,
            this.maxEbookDislikeFilter == null ? this.maxEbookDislikeFilterEmpty : this.maxEbookDislikeFilter,
            this.minEbookDislikeFilter == null ? this.minEbookDislikeFilterEmpty : this.minEbookDislikeFilter,
            this.discriptionFilter,
            this.ebookCoverFilter,
            this.maxBookPageFilter == null ? this.maxBookPageFilterEmpty : this.maxBookPageFilter,
            this.minBookPageFilter == null ? this.minBookPageFilterEmpty : this.minBookPageFilter,
            this.userNameFilter,
            this.pbClassClassNameFilter,
            this.pbPlacePlaceNameFilter,
            this.pbRankRankNameFilter,
            this.pbStatusStatusNameFilter,
            this.pbSubjectSectionNameFilter,
            this.pbSubjectEducationSubjectNameFilter,
            this.pbTypeEbookTypeNameFilter,
            this.pbTypeFileTypeFileNameFilter,
        )
            .subscribe(result => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
