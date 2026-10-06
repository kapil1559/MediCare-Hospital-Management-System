import {
  ChangeDetectionStrategy,
  Component,
  inject
} from '@angular/core';

import { Router } from '@angular/router';

//import { DynamicListComponent } from '../../../../shared/components/dynamic-list/dynamic-list';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  //imports: [DynamicListComponent],
  templateUrl: './patient-list.html',
  styleUrl: './patient-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PatientListComponent {

  private readonly router = inject(Router);

  protected addPatient(): void {
    void this.router.navigate(['/patients/add']);
  }

}