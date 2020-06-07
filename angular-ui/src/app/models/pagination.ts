import { ICustomer } from './customer';


export interface IPagination {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: ICustomer[];
}

export class Pagination implements IPagination {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: ICustomer[] = [];
}
