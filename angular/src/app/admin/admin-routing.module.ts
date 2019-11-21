import { NgModule } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { PbLinkProsComponent } from './linkPro/pbLinkPros/pbLinkPros.component';
import { PbTypeEbooksComponent } from './typeEbook/pbTypeEbooks/pbTypeEbooks.component';
import { PbPlacesComponent } from './place/pbPlaces/pbPlaces.component';
import { PbSubjectEducationsComponent } from './subjectEducation/pbSubjectEducations/pbSubjectEducations.component';
import { PbStatusesComponent } from './status/pbStatuses/pbStatuses.component';
import { PbTypeFilesComponent } from './typeFile/pbTypeFiles/pbTypeFiles.component';
import { PbRanksComponent } from './rank/pbRanks/pbRanks.component';
import { PbClassesComponent } from './class/pbClasses/pbClasses.component';
import { AuditLogsComponent } from './audit-logs/audit-logs.component';
import { HostDashboardComponent } from './dashboard/host-dashboard.component';
import { DemoUiComponentsComponent } from './demo-ui-components/demo-ui-components.component';
import { EditionsComponent } from './editions/editions.component';
import { InstallComponent } from './install/install.component';
import { LanguageTextsComponent } from './languages/language-texts.component';
import { LanguagesComponent } from './languages/languages.component';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { OrganizationUnitsComponent } from './organization-units/organization-units.component';
import { RolesComponent } from './roles/roles.component';
import { HostSettingsComponent } from './settings/host-settings.component';
import { TenantSettingsComponent } from './settings/tenant-settings.component';
import { InvoiceComponent } from './subscription-management/invoice/invoice.component';
import { SubscriptionManagementComponent } from './subscription-management/subscription-management.component';
import { TenantsComponent } from './tenants/tenants.component';
import { UiCustomizationComponent } from './ui-customization/ui-customization.component';
import { UsersComponent } from './users/users.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'linkPro/pbLinkPros', component: PbLinkProsComponent, data: { permission: 'Pages.Administration.PbLinkPros' }  },
                    { path: 'typeEbook/pbTypeEbooks', component: PbTypeEbooksComponent, data: { permission: 'Pages.Administration.PbTypeEbooks' }  },
                    { path: 'place/pbPlaces', component: PbPlacesComponent, data: { permission: 'Pages.Administration.PbPlaces' }  },
                    { path: 'subjectEducation/pbSubjectEducations', component: PbSubjectEducationsComponent, data: { permission: 'Pages.Administration.PbSubjectEducations' }  },
                    { path: 'status/pbStatuses', component: PbStatusesComponent, data: { permission: 'Pages.Administration.PbStatuses' }  },
                    { path: 'typeFile/pbTypeFiles', component: PbTypeFilesComponent, data: { permission: 'Pages.Administration.PbTypeFiles' }  },
                    { path: 'rank/pbRanks', component: PbRanksComponent, data: { permission: 'Pages.Administration.PbRanks' }  },
                    { path: 'class/pbClasses', component: PbClassesComponent, data: { permission: 'Pages.Administration.PbClasses' }  },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Administration.Users' } },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Administration.Roles' } },
                    { path: 'auditLogs', component: AuditLogsComponent, data: { permission: 'Pages.Administration.AuditLogs' } },
                    { path: 'maintenance', component: MaintenanceComponent, data: { permission: 'Pages.Administration.Host.Maintenance' } },
                    { path: 'hostSettings', component: HostSettingsComponent, data: { permission: 'Pages.Administration.Host.Settings' } },
                    { path: 'editions', component: EditionsComponent, data: { permission: 'Pages.Editions' } },
                    { path: 'languages', component: LanguagesComponent, data: { permission: 'Pages.Administration.Languages' } },
                    { path: 'languages/:name/texts', component: LanguageTextsComponent, data: { permission: 'Pages.Administration.Languages.ChangeTexts' } },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' } },
                    { path: 'organization-units', component: OrganizationUnitsComponent, data: { permission: 'Pages.Administration.OrganizationUnits' } },
                    { path: 'subscription-management', component: SubscriptionManagementComponent, data: { permission: 'Pages.Administration.Tenant.SubscriptionManagement' } },
                    { path: 'invoice/:paymentId', component: InvoiceComponent, data: { permission: 'Pages.Administration.Tenant.SubscriptionManagement' } },
                    { path: 'tenantSettings', component: TenantSettingsComponent, data: { permission: 'Pages.Administration.Tenant.Settings' } },
                    { path: 'hostDashboard', component: HostDashboardComponent, data: { permission: 'Pages.Administration.Host.Dashboard' } },
                    { path: 'demo-ui-components', component: DemoUiComponentsComponent, data: { permission: 'Pages.DemoUiComponents' } },
                    { path: 'install', component: InstallComponent },
                    { path: 'ui-customization', component: UiCustomizationComponent }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AdminRoutingModule {

    constructor(
        private router: Router
    ) {
        router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                window.scroll(0, 0);
            }
        });
    }
}
