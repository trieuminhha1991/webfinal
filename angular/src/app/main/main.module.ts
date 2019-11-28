import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {AppCommonModule} from '@app/shared/common/app-common.module';
import {PbPriceUsersComponent} from './priceUser/pbPriceUsers/pbPriceUsers.component';
import {ViewPbPriceUserModalComponent} from './priceUser/pbPriceUsers/view-pbPriceUser-modal.component';
import {CreateOrEditPbPriceUserModalComponent} from './priceUser/pbPriceUsers/create-or-edit-pbPriceUser-modal.component';
import {PbPriceUserUserLookupTableModalComponent} from './priceUser/pbPriceUsers/pbPriceUser-user-lookup-table-modal.component';

import {PbDownloadEbooksComponent} from './downloadEbook/pbDownloadEbooks/pbDownloadEbooks.component';
import {ViewPbDownloadEbookModalComponent} from './downloadEbook/pbDownloadEbooks/view-pbDownloadEbook-modal.component';
import {CreateOrEditPbDownloadEbookModalComponent} from './downloadEbook/pbDownloadEbooks/create-or-edit-pbDownloadEbook-modal.component';
import {PbDownloadEbookPbEbookLookupTableModalComponent} from './downloadEbook/pbDownloadEbooks/pbDownloadEbook-pbEbook-lookup-table-modal.component';

import {PbOppinionsComponent} from './oppinion/pbOppinions/pbOppinions.component';
import {ViewPbOppinionModalComponent} from './oppinion/pbOppinions/view-pbOppinion-modal.component';
import {CreateOrEditPbOppinionModalComponent} from './oppinion/pbOppinions/create-or-edit-pbOppinion-modal.component';
import {PbOppinionUserLookupTableModalComponent} from './oppinion/pbOppinions/pbOppinion-user-lookup-table-modal.component';
import {PbOppinionPbEbookLookupTableModalComponent} from './oppinion/pbOppinions/pbOppinion-pbEbook-lookup-table-modal.component';

import {PbEbooksComponent} from './ebook/pbEbooks/pbEbooks.component';
import {ViewPbEbookModalComponent} from './ebook/pbEbooks/view-pbEbook-modal.component';
import {CreateOrEditPbEbookModalComponent} from './ebook/pbEbooks/create-or-edit-pbEbook-modal.component';
import {PbEbookUserLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-user-lookup-table-modal.component';
import {PbEbookPbClassLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-pbClass-lookup-table-modal.component';
import {PbEbookPbPlaceLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-pbPlace-lookup-table-modal.component';
import {PbEbookPbRankLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-pbRank-lookup-table-modal.component';
import {PbEbookPbStatusLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-pbStatus-lookup-table-modal.component';
import {PbEbookPbSubjectLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-pbSubject-lookup-table-modal.component';
import {PbEbookPbSubjectEducationLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-pbSubjectEducation-lookup-table-modal.component';
import {PbEbookPbTypeEbookLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-pbTypeEbook-lookup-table-modal.component';
import {PbEbookPbTypeFileLookupTableModalComponent} from './ebook/pbEbooks/pbEbook-pbTypeFile-lookup-table-modal.component';

import {PbSubjectsComponent} from './subject/pbSubjects/pbSubjects.component';
import {ViewPbSubjectModalComponent} from './subject/pbSubjects/view-pbSubject-modal.component';
import {CreateOrEditPbSubjectModalComponent} from './subject/pbSubjects/create-or-edit-pbSubject-modal.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {PaginatorModule} from 'primeng/paginator';
import {EditorModule} from 'primeng/editor';
import {InputMaskModule} from 'primeng/inputmask';
import {FileUploadModule} from 'primeng/fileupload';
import {TableModule} from 'primeng/table';

import {UtilsModule} from '@shared/utils/utils.module';
import {CountoModule} from 'angular2-counto';
import {ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule} from 'ngx-bootstrap';
import {DashboardComponent} from './dashboard/dashboard.component';
import {MainRoutingModule} from './main-routing.module';
import {NgxChartsModule} from '@swimlane/ngx-charts';

import {
    BsDatepickerModule,
    BsDatepickerConfig,
    BsDaterangepickerConfig,
    BsLocaleService
} from 'ngx-bootstrap/datepicker';
import {NgxBootstrapDatePickerConfigService} from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import {SidebarModule} from 'primeng/sidebar';
import {ScrollPanelModule} from 'primeng/scrollpanel';
import {CardModule} from 'primeng/card';
import {TabViewModule} from 'primeng/tabview';
import {ShopEbookComponent} from './ebook/shop-ebook/shop-ebook.component';
import {SplitButtonModule} from 'primeng/splitbutton';
import { DetailebookComponent } from './ebook/detailebook/detailebook.component';
import { DataView } from 'primeng/dataview';
import { CarouselModule } from 'primeng/carousel';
import { RatingModule } from 'primeng/rating';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
        FileUploadModule,
        AutoCompleteModule,
        PaginatorModule,
        EditorModule,
        InputMaskModule,
        TableModule,
        SidebarModule, ScrollPanelModule, CardModule, TabViewModule, SplitButtonModule, CarouselModule, RatingModule,
        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot()
    ],
    declarations: [
        DataView,
        PbPriceUsersComponent,
        ViewPbPriceUserModalComponent, CreateOrEditPbPriceUserModalComponent,
        PbPriceUserUserLookupTableModalComponent,
        PbDownloadEbooksComponent,
        ViewPbDownloadEbookModalComponent, CreateOrEditPbDownloadEbookModalComponent,
        PbDownloadEbookPbEbookLookupTableModalComponent,
        PbOppinionsComponent,
        ViewPbOppinionModalComponent, CreateOrEditPbOppinionModalComponent,
        PbOppinionUserLookupTableModalComponent,
        PbOppinionPbEbookLookupTableModalComponent,
        PbEbooksComponent,
        ViewPbEbookModalComponent, CreateOrEditPbEbookModalComponent,
        PbEbookUserLookupTableModalComponent,
        PbEbookPbClassLookupTableModalComponent,
        PbEbookPbPlaceLookupTableModalComponent,
        PbEbookPbRankLookupTableModalComponent,
        PbEbookPbStatusLookupTableModalComponent,
        PbEbookPbSubjectLookupTableModalComponent,
        PbEbookPbSubjectEducationLookupTableModalComponent,
        PbEbookPbTypeEbookLookupTableModalComponent,
        PbEbookPbTypeFileLookupTableModalComponent,
        PbSubjectsComponent,
        ViewPbSubjectModalComponent, CreateOrEditPbSubjectModalComponent,
        DashboardComponent,
        ShopEbookComponent,
        DetailebookComponent,
    ],
    providers: [
        {provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig},
        {provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig},
        {provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale}
    ]
})
export class MainModule {
}
