import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HotelsRoutingModule } from './hotels-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { HotelsComponent } from './hotels.component';
import { HotelModalComponent } from './hotel-modal/hotel-modal.component';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { RoomComponent } from './room/room.component';
import { OrderRoomComponent } from './order-room/order-room.component';
import { RoomModalComponent } from './room/room-modal/room-modal.component';
import { NgxMaskModule } from 'ngx-mask';
import { ImageManagerComponent } from './room/image-manager/image-manager.component';
import { OrderRoomModalComponent } from './order-room/order-room-modal/order-room-modal.component';
import { OrderRoomServiceComponent } from './order-room/order-room-service/order-room-service.component';
import { ReturnRoomModalComponent } from './order-room/return-room-modal/return-room-modal.component';
import { CKEditorModule } from 'ckeditor4-angular';

@NgModule({
  declarations: [
    HotelsComponent,
    HotelModalComponent,
    RoomComponent,
    OrderRoomComponent,
    RoomModalComponent,
    ImageManagerComponent,
    OrderRoomModalComponent,
    OrderRoomServiceComponent,
    ReturnRoomModalComponent,
  ],
  imports: [
    CommonModule,
    HotelsRoutingModule,
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
    CKEditorModule
  ],
})
export class HotelsModule { }
