import { Routes } from '@angular/router';

import { MainLayoutComponent } from './shared/layout/main-layout';
import { DynamicListComponent } from './shared/components/dynamic-list/dynamic-list';
import { PatientFormComponent } from './features/patient/components/patient-form/patient-form';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'patients',
        pathMatch: 'full'
      },
      // Patient Registration
      {
        path: 'patients/new',
        component: PatientFormComponent
      },
      {
        path: ':moduleRoute',
        component: DynamicListComponent
      }
    ]
  }
];