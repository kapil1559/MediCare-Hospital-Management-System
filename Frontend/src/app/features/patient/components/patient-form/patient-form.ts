import {
  ChangeDetectionStrategy,
  Component,
  inject,
  signal
} from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { FormPageComponent } from '../../../../shared/components/form-page/form-page';
import { FormHeaderComponent } from '../../../../shared/components/form-header/form-header';
import { FormSectionComponent } from '../../../../shared/components/form-section/form-section';
import { FormFooterComponent } from '../../../../shared/components/form-footer/form-footer';
import { PatientService } from '../../services/patient.service';
import { PatientRequest } from '../../models/patient-request.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-form',
  standalone: true,
  imports: [ReactiveFormsModule, FormPageComponent, FormHeaderComponent, FormSectionComponent, FormFooterComponent],
  templateUrl: './patient-form.html',
  styleUrl: './patient-form.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PatientFormComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly patientService = inject(PatientService);
  private readonly router = inject(Router);

  protected readonly saving = signal(false);
  protected readonly isEditMode = signal(false);

  protected readonly patientForm = this.formBuilder.nonNullable.group({
    patientID: [0],
    patientCode: ['', [Validators.required, Validators.maxLength(20)]],
    firstName: ['', [Validators.required, Validators.maxLength(100)]],
    lastName: ['', Validators.maxLength(100)],
    gender: ['', Validators.required],
    dob: [''],
    age: [0, [Validators.required, Validators.min(0), Validators.max(150)]],
    bloodGroup: [''],
    mobileNo: ['', Validators.maxLength(20)],
    email: ['', Validators.email],
    address: ['', Validators.maxLength(500)],
    city: [''],
    state: [''],
    pinCode: ['', Validators.maxLength(10)]
  });
today: any;

  protected save(): void {
    if (this.patientForm.invalid) {
      this.patientForm.markAllAsTouched();
      return;
    }

    const formValue = this.patientForm.getRawValue();

    const request: PatientRequest = {
      mode: this.isEditMode() ? 'Edit' : 'Add',
      patientID: formValue.patientID,
      patientCode: formValue.patientCode,
      firstName: formValue.firstName,
      lastName: formValue.lastName || null,
      gender: formValue.gender,
      dob: formValue.dob || null,
      age: formValue.age,
      bloodGroup: formValue.bloodGroup || null,
      mobileNo: formValue.mobileNo || null,
      email: formValue.email || null,
      address: formValue.address || null,
      city: formValue.city || null,
      state: formValue.state || null,
      pinCode: formValue.pinCode || null
    };

    this.saving.set(true);

    this.patientService.savePatient(request).subscribe({
      next: () => {
        this.saving.set(false);
        this.patientForm.reset({
          patientID: 0,
          patientCode: '',
          firstName: '',
          lastName: '',
          gender: '',
          dob: '',
          age: 0,
          bloodGroup: '',
          mobileNo: '',
          email: '',
          address: '',
          city: '',
          state: '',
          pinCode: ''
        });
      },
      error: () => {
        this.saving.set(false);
      }
    });
  }
  protected cancel(): void {
  window.history.back();
}
}