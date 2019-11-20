import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PbEbooksComponent } from './ebook/pbEbooks/pbEbooks.component';
import { PbSubjectsComponent } from './subject/pbSubjects/pbSubjects.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'ebook/pbEbooks', component: PbEbooksComponent, data: { permission: 'Pages.PbEbooks' }  },
                    { path: 'subject/pbSubjects', component: PbSubjectsComponent, data: { permission: 'Pages.PbSubjects' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
