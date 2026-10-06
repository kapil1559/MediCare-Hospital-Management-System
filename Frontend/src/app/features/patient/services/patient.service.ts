import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Patient } from '../models/patient.model';
import { PatientRequest } from '../models/patient-request.model';
import { DynamicListResponse } from '../../../shared/models/dynamic-list-response.model';
@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'https://localhost:44338/api/Patient';

  public getPatients(
    search: string,
    pageNumber: number,
    pageSize: number
  ): Observable<DynamicListResponse> {

    const params = new HttpParams()
      .set('search', search)
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);

    return this.http.get<DynamicListResponse>(
      this.apiUrl,
      { params }
    );
  }

  public getPatientById(
    patientId: number
  ): Observable<Patient | null> {

    return this.http.get<Patient>(
      `${this.apiUrl}/${patientId}`
    );
  }

  public savePatient(
    request: PatientRequest
  ): Observable<unknown> {

    return this.http.post(
      `${this.apiUrl}/IUPatient`,
      request
    );
  }

  public deletePatient(
    patientId: number
  ): Observable<unknown> {

    return this.http.delete(
      `${this.apiUrl}/${patientId}`
    );
  }
}