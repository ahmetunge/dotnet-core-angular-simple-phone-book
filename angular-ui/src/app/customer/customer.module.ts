import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { MatDialogModule } from '@angular/material/dialog';

import { ToastrModule } from 'ngx-toastr';

import { CustomerComponent } from './customer.component';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerService } from './customer.service';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { CustomerDetailResolver } from '../resolvers/customer-detail.resolver';
import { CustomerEditComponent } from './customer-edit/customer-edit.component';
import { CustomerEditResolver } from '../resolvers/customer-edit.resolver';


@NgModule({
  imports: [
    CommonModule,
    CustomerRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatCardModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatDialogModule,
    ToastrModule.forRoot({
      timeOut: 2500,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      enableHtml: true
    }),
  ],
  declarations: [
    CustomerComponent,
    CustomerListComponent,
    CustomerDetailComponent,
    CustomerEditComponent
  ],
  providers: [
    CustomerService,
    CustomerDetailResolver,
    CustomerEditResolver
  ]
})
export class CustomerModule { }
