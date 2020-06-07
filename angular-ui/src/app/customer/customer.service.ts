import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IPagination, Pagination } from '../models/pagination';
import { CustomerParams } from '../models/customerParams';
import { ICustomer } from '../models/customer';
import { of } from 'rxjs/internal/observable/of';
import { map, delay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {


  baseUrl = environment.apiUrl;
  customerParams = new CustomerParams();
  customers: ICustomer[] = [];
  pagination = new Pagination();

  constructor(private http: HttpClient) { }

  getCustomerParams() {
    return this.customerParams;
  }

  setCustomerParams(params: CustomerParams) {
    this.customerParams = params;
  }

  saveCustomer(customer: ICustomer) {
    return this.http.post(this.baseUrl + 'customers', customer);
  }

  updateCustomer(id: number, customer: ICustomer) {
    return this.http.put(this.baseUrl + 'customers/' + id, customer);
  }

  deleteCustomer(id: number) {
    return this.http.delete(this.baseUrl + 'customers/' + id);
  }

  getCustomers(useCache: boolean) {

    if (useCache === false) {
      this.customers = [];
    }

    if (this.customers.length > 0 && useCache === true) {
      const pagesReceived = Math.ceil(this.customers.length / this.customerParams.pageSize);

      if (this.customerParams.pageNumber <= pagesReceived) {
        this.pagination.data =
          this.customers.slice((this.customerParams.pageNumber - 1) * this.customerParams.pageSize,
            this.customerParams.pageNumber * this.customerParams.pageSize);

        return of(this.pagination);
      }
    }

    let params = new HttpParams();

    if (this.customerParams.search) {
      params = params.append('search', this.customerParams.search);
    }

    params = params.append('pageIndex', this.customerParams.pageNumber.toString());
    params = params.append('pageSize', this.customerParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'customers', { observe: 'response', params }).pipe(
      map(response => {
        this.customers = [...this.customers, ...response.body.data];
        this.pagination = response.body;
        return this.pagination;
      })
    );
  }

  getCustomerDetail(id: any) {
    return this.http.get<ICustomer>(this.baseUrl + 'customers/' + id);
  }

}
