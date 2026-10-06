import { DynamicListRow } from './dynamic-list-row.model';

export interface DynamicListResponse {
  data: DynamicListRow[];
  totalRecords: number;
  pageNumber: number;
  pageSize: number;
}