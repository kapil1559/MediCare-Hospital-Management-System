import { ListColumn } from './list-column.model';

export interface ListConfiguration {
  moduleID: number;

  moduleCode: string;

  moduleName: string;

  route: string | null;

  moduleListConfigurationID: number;

  listTitle: string;

  apiEndpoint: string;

  searchEnabled: boolean;

  searchPlaceholder: string | null;

  addEnabled: boolean;

  editEnabled: boolean;

  deleteEnabled: boolean;

  pageSize: number;

  columnsJson: string;

  columns: ListColumn[];

  isActive: boolean;
}