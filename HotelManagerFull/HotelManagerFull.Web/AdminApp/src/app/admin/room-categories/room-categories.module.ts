import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoomCategoriesRoutingModule } from './room-categories-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { RoomCategoriesComponent } from './room-categories.component';
import { RoomCategoriesModalComponent } from './room-categories-modal/room-categories-modal.component';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';


@NgModule({
  declarations: [RoomCategoriesComponent, RoomCategoriesModalComponent],
  imports: [
    CommonModule,
    RoomCategoriesRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NzTableModule,
    NzFormModule,
    NzSpinModule,
    NzPopconfirmModule,
    NzDatePickerModule,
  ]
})
export class RoomCategoriesModule { }
