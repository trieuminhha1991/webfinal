import {Component, Injector, ViewChild} from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {LazyLoadEvent} from "@node_modules/primeng/components/common/lazyloadevent";
import {PbEbooksServiceProxy, GetPbEbookForViewDto, TokenAuthServiceProxy} from '@shared/service-proxies/service-proxies';
import {NotifyService} from "@abp/notify/notify.service";
import {ActivatedRoute} from "@angular/router";
import {FileDownloadService} from "@shared/utils/file-download.service";
import * as moment from "@node_modules/moment";
import {Paginator} from 'primeng/components/paginator/paginator';

@Component({
  selector: 'app-shop-ebook',
  templateUrl: './shop-ebook.component.html',
  styleUrls: ['./shop-ebook.component.css'],
    animations: [appModuleAnimation()]
})
export class ShopEbookComponent extends AppComponentBase {
    @ViewChild('paginator', {static: true}) paginator: Paginator;
    Ebook: GetPbEbookForViewDto[] = [];
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
    getPbEbooks() {
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
            null,
            10,
            100
        ).subscribe(result => {
            this.Ebook=result.items;
        });
    }
}
