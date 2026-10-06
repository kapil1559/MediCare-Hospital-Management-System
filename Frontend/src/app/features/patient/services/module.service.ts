import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Module } from '../models/module.model';

@Injectable({
  providedIn: 'root'
})
export class ModuleService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'https://localhost:44338/api/Modules';

  public getActiveModules(): Observable<Module[]> {
    return this.http.get<Module[]>(this.apiUrl);
  }
}