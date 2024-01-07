import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ServicesRoutingModule } from './services-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { ServicesComponent } from './services.component';
import { ServiceCategoriesModalComponent } from './service-categories-modal/service-categories-modal.component';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { ServiceModalComponent } from './service-modal/service-modal.component';
import { NgxMaskModule } from 'ngx-mask';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzRadioModule } from 'ng-zorro-antd/radio';


@NgModule({
  declarations: [ServicesComponent, ServiceCategoriesModalComponent, ServiceModalComponent],
  imports: [
    CommonModule,
    ServicesRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NzTableModule,
    NzFormModule,
    NzSpinModule,
    NzPopconfirmModule,
    NzDatePickerModule,
    NzSelectModule,
    NzRadioModule,
    NgxMaskModule.forRoot(),
  ]
})
export class ServicesModule { }
