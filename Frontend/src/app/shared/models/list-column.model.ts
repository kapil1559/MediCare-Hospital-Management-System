export interface ListColumn {
  field: string;
  header: string;

  type:
    | 'text'
    | 'number'
    | 'date'
    | 'boolean'
    | 'badge'
    | 'gender'
    | 'phone'
    | 'email'
    | 'currency'
    | 'status';

  visible: boolean;
  sortable?: boolean;
}