import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ICustomer } from 'src/app/models/customer';
import { ToastrService } from 'ngx-toastr';
import { CustomerService } from '../customer.service';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.css']
})
export class CustomerEditComponent implements OnInit {

  customerId = 0;
  customer: ICustomer;
  customerForm: FormGroup;
  firstname: FormControl = new FormControl('', [Validators.required]);
  lastname: FormControl = new FormControl('', [Validators.required]);
  email: FormControl = new FormControl('', [Validators.required, Validators.email]);
  phone: FormControl = new FormControl('', [Validators.required]);
  gender: FormControl = new FormControl('Female', [Validators.required]);

  constructor(
    private formBuilder: FormBuilder,
    private toaster: ToastrService,
    private customerService:
      CustomerService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
    const routeId = this.route.snapshot.params.id;
    if (routeId) {
      this.customerId = routeId;
      this.getCustomerDetail();
    } else {
      this.customerId = null;
    }
    this.createForm();

  }

  saveCustomer() {
    if (this.customerForm.valid) {
      this.customer = Object.assign({}, this.customerForm.value);
      if (this.customerId > 0) {
        this.updateCustomer();
      } else {
        this.createNewCustomer();
      }
    }
  }

  createNewCustomer() {
    this.customerId = null;
    this.customerService.saveCustomer(this.customer).subscribe((res: any) => {

      this.toaster.success('Customer saved succesfully');
      this.router.navigate(['customers', res.id]);
    }, error => {
      this.toaster.error('An error occured when save data');
    });
  }
  updateCustomer() {
    this.customerService.updateCustomer(this.customerId, this.customer).subscribe((res: any) => {
      this.toaster.success('Customer saved succesfully');
      this.router.navigate(['customers', this.customerId]);
    }, error => {
      this.toaster.error('An error occured when save data');
    });
  }

  getCustomerDetail() {
    this.route.data.subscribe((response) => {
      this.customer = response.customerEditResolve;

      if (this.customer) {
        this.setCustomerValues();
      }
    });
  }
  setCustomerValues() {

    this.firstname.setValue(this.customer.firstname);
    this.lastname.setValue(this.customer.lastname);
    this.gender.setValue(this.customer.gender);
    this.phone.setValue(this.customer.phone);
    this.email.setValue(this.customer.email);


  }

  createForm() {
    this.customerForm = this.formBuilder.group({
      email: this.email,
      firstname: this.firstname,
      lastname: this.lastname,
      phone: this.phone,
      gender: this.gender
    });
  }

}
