import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProvincesRoutingModule } from './provinces-routing.module';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { ProvincesComponent } from './provinces.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { ProvincesModalComponent } from './provinces-modal/provinces-modal.component';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';

@NgModule({
  declarations: [ProvincesComponent, ProvincesModalComponent],
  imports: [
    CommonModule,
    ProvincesRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NzTableModule,
    NzFormModule,
    NzSpinModule,
    NzPopconfirmModule,
    NzDatePickerModule,
  ],
})
export class ProvincesModule { }
