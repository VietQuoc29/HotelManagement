import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomersRoutingModule } from './customers-routing.module';
import { CustomersModalComponent } from './customers-modal/customers-modal.component';
import { CustomersComponent } from './customers.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzSelectModule } from 'ng-zorro-antd/select';


@NgModule({
  declarations: [
    CustomersComponent,
    CustomersModalComponent
  ],
  imports: [
    CommonModule,
    CustomersRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NzTableModule,
    NzFormModule,
    NzSpinModule,
    NzPopconfirmModule,
    NzDatePickerModule,
    NzSelectModule
  ]
})
export class CustomersModule { }
