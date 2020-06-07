import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ICustomer } from '../models/customer';
import { CustomerService } from '../customer/customer.service';

@Injectable()

export class CustomerDetailResolver implements Resolve<ICustomer> {

  constructor(
    private customerService: CustomerService,
    private router: Router
  ) { }

  resolve(route: ActivatedRouteSnapshot): Observable<ICustomer> {
    return this.customerService.getCustomerDetail(route.params.id).pipe(
      catchError(err => {
        this.router.navigate(['/home']);
        return of(null);
      })
    );
  }

}
