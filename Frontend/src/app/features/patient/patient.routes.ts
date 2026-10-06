import { Routes } from '@angular/router';

import { PatientListComponent } from './components/patient-list/patient-list';
import { PatientFormComponent } from './components/patient-form/patient-form';

export const patientRoutes: Routes = [
  {
    path: '',
    component: PatientListComponent
  },
  {
    path: 'new',
    component: PatientFormComponent
  }
];