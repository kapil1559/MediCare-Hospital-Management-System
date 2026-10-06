import { inject, Injectable } from '@angular/core';
import {
  HttpClient,
  HttpParams
} from '@angular/common/http';
import { Observable, map } from 'rxjs';

import { ListConfiguration } from '../../../shared/models/list-configuration.model';
import { ListColumn } from '../../../shared/models/list-column.model';
import { DynamicListResponse } from '../../../shared/models/dynamic-list-response.model';

@Injectable({
  providedIn: 'root'
})
export class ListConfigurationService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'https://localhost:44338/api/ListConfiguration';

  public getConfiguration(
    moduleCode: string
  ): Observable<ListConfiguration> {

    return this.http
      .get<ListConfiguration>(
        `${this.apiUrl}/${moduleCode}`
      )
      .pipe(
        map(configuration => ({
          ...configuration,
          columns: this.parseColumns(
            configuration.columnsJson
          )
        }))
      );
  }

  public getListData(
    apiEndpoint: string,
    search: string,
    pageNumber: number,
    pageSize: number
  ): Observable<DynamicListResponse> {

    const params = new HttpParams()
      .set('search', search)
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);

    return this.http.get<DynamicListResponse>(
      `https://localhost:44338${apiEndpoint}`,
      { params }
    );
  }

  private parseColumns(
    columnsJson: string
  ): ListColumn[] {

    try {
      const columns: ListColumn[] =
        JSON.parse(columnsJson);

      return columns.filter(
        column => column.visible
      );
    } catch {
      return [];
    }
  }
}