import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerComponent } from './customer.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { CustomerDetailResolver } from '../resolvers/customer-detail.resolver';
import { CustomerEditComponent } from './customer-edit/customer-edit.component';
import { CustomerEditResolver } from '../resolvers/customer-edit.resolver';


const routes: Routes = [
  {
    path: '',
    component: CustomerComponent,
    children: [
      {
        path: '',
        component: CustomerListComponent
      },
      {
        path: 'new',
        component: CustomerEditComponent
      },
      {
        path: ':id',
        component: CustomerDetailComponent,
        resolve: { customerDetailResolve: CustomerDetailResolver }
      },
      {
        path: 'edit/:id',
        component: CustomerEditComponent,
        resolve: { customerEditResolve: CustomerEditResolver }
      }
    ]
  },

];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: []
})

export class CustomerRoutingModule { }
