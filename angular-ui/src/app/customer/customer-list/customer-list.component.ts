import { Component, OnInit, ViewChild, Output, ElementRef } from '@angular/core';
import { CustomerService } from '../customer.service';
import { ICustomer } from 'src/app/models/customer';
import { CustomerParams } from 'src/app/models/customerParams';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  customerParams: CustomerParams;
  customers: ICustomer[];
  totalCount: number;
  @ViewChild('search') searchTerm: ElementRef;

  displayedColumns: string[] = ['firstname', 'lastname', 'email', 'gender', 'phone', 'button'];
  pageSizeOptions = [5, 10, 20, 50];
  isLoadingResults = true;

  constructor(private customerService: CustomerService) {
    this.customerParams = this.customerService.getCustomerParams();
  }

  ngOnInit() {
    this.getCustomers();
  }

  getCustomers() {
    this.isLoadingResults = true;
    this.customerService.getCustomers(false).subscribe(response => {
      this.isLoadingResults = false;
      this.customers = response.data;
      this.totalCount = response.count;
      console.log(this.customers);
    }, error => {
      this.isLoadingResults = false;
      console.log(error);
    });
  }

  onPagerChange(event: PageEvent) {
    const params = this.customerService.getCustomerParams();

    if (params.pageSize !== event.pageSize) {
      params.pageSize = event.pageSize;
      this.customerService.setCustomerParams(params);
      this.getCustomers();
    }

    if (params.pageNumber !== event.pageIndex) {

      if (params.pageNumber < event.pageIndex) {
        params.pageNumber = event.pageIndex + 1;
      } else {
        params.pageNumber = event.pageIndex - 1;
      }

      this.customerService.setCustomerParams(params);
      this.getCustomers();
    }
  }

  onSearch() {
    const params = this.customerService.getCustomerParams();
    params.search = this.searchTerm.nativeElement.value;
    params.pageNumber = 1;
    this.customerService.setCustomerParams(params);
    this.getCustomers();
  }
}


