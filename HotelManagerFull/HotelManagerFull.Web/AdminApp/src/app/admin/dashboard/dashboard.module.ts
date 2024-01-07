import { DashboardComponent } from './dashboard.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzTableModule } from 'ng-zorro-antd/table';
import { ViewPaymentModalComponent } from './view-payment-modal/view-payment-modal.component';
import { ViewRegisterRoomModalComponent } from './view-register-room-modal/view-register-room-modal.component';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NgxMaskModule } from 'ngx-mask';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';

@NgModule({
  declarations: [
    DashboardComponent,
    ViewPaymentModalComponent,
    ViewRegisterRoomModalComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DashboardRoutingModule,
    FormsModule,
    NzTableModule,
    NzFormModule,
    NzSpinModule,
    NzRadioModule,
    NgxMaskModule.forRoot(),
    NzDatePickerModule,
  ],
})
export class DashboardModule {}
