import { Component, OnInit } from '@angular/core';
import { ICustomer } from 'src/app/models/customer';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../customer.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  customer: ICustomer;

  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService,
    private toaster: ToastrService,
    private router: Router
  ) { }

  ngOnInit() {
    this.getCustomerDetail();
  }

  getCustomerDetail() {
    this.route.data.subscribe((response) => {
      this.customer = response.customerDetailResolve;
      console.log(this.customer);
    });
  }

  deleteCustomer() {
    this.customerService.deleteCustomer(this.customer.id).subscribe((res: any) => {
      this.toaster.success('Customer deleted succesfully');
      this.router.navigate(['customers']);
    }, error => {
      this.toaster.error('An error occured when delete data');
    });
  }
}
