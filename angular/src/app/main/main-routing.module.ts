import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PbSubjectsComponent } from './subject/pbSubjects/pbSubjects.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
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
