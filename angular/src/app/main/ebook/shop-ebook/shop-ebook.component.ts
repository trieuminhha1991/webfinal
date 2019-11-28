import {Component, Injector, ViewEncapsulation, ViewChild} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {PbEbooksServiceProxy, PbEbookDto} from '@shared/service-proxies/service-proxies';
import {NotifyService} from '@abp/notify/notify.service';
import {AppComponentBase} from '@shared/common/app-component-base';
import {TokenAuthServiceProxy} from '@shared/service-proxies/service-proxies';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {Paginator} from 'primeng/components/paginator/paginator';
import {LazyLoadEvent} from 'primeng/components/common/lazyloadevent';
import {FileDownloadService} from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import {MenuItem} from 'primeng/components/common/menuitem';
import {CreateOrEditPbEbookModalComponent} from '@app/main/ebook/pbEbooks/create-or-edit-pbEbook-modal.component';
import {ViewPbEbookModalComponent} from '@app/main/ebook/pbEbooks/view-pbEbook-modal.component';
import {SelectItem} from 'primeng/components/common/selectitem';
import { DataView } from 'primeng/dataview';

@Component({
  selector: 'app-shop-ebook',
  templateUrl: './shop-ebook.component.html',
  styleUrls: ['./shop-ebook.component.css'],
    animations: [appModuleAnimation()]
})
export class ShopEbookComponent extends AppComponentBase {
    @ViewChild('createOrEditPbEbookModal', {static: true}) createOrEditPbEbookModal: CreateOrEditPbEbookModalComponent;
    @ViewChild('viewPbEbookModalComponent', {static: true}) viewPbEbookModal: ViewPbEbookModalComponent;
    @ViewChild('dataTable', {static: true}) dataTable: DataView;
    @ViewChild('paginator', {static: true}) paginator: Paginator;
    sortOptions: SelectItem[];
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
    sortField:string;
    items: MenuItem[];
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
    ngOnInit() {
        this.sortOptions = [
            {label: 'Lượt xem', value: 'EbookView'},
            {label: 'Lượt thích', value: 'EbookLike'},
            {label: 'Mới nhất', value: '!EbookDateStart'}
        ];
        this.items = [
            {label: 'Tải về', icon: 'pi pi-refresh', command: () => {
                    ;
                }},
            {label: 'Xem chi tiết', icon: 'pi pi-times', command: () => {
                    ;
                }},
            {label: 'Mua Pro', icon: 'pi pi-info', command: () => {}},
            {label: 'Thêm vào bộ sưu tập', icon: 'pi pi-cog', command: () => {}}
        ];
    }
    onSortChange(event) {
        let value = event.value;
        if (value.indexOf('!') === 2) {
            this.sortField = value.substring(1, value.length);
        }
        else {
            this.sortField = value;
        }
    }
    getSorting(table: DataView): string {
        let sorting;
        if (table.sortField) {
            sorting = table.sortField+ ' DESC';
        }
        return sorting;
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
            this.getSorting(this.dataTable),
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
}
