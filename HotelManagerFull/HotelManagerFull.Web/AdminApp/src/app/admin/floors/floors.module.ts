import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FloorsRoutingModule } from './floors-routing.module';
import { FloorsModalComponent } from './floors-modal/floors-modal.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { FloorsComponent } from './floors.component';


@NgModule({
  declarations: [
    FloorsComponent,
    FloorsModalComponent
  ],
  imports: [
    CommonModule,
    FloorsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NzTableModule,
    NzFormModule,
    NzSpinModule,
    NzPopconfirmModule,
    NzDatePickerModule,
  ]
})
export class FloorsModule { }
