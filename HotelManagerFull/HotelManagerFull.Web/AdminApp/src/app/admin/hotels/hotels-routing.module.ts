import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HotelsComponent } from './hotels.component';
import { OrderRoomComponent } from './order-room/order-room.component';
import { RoomComponent } from './room/room.component';

const routes: Routes = [
  {
    path: '',
    component: HotelsComponent,
  },
  {
    path: 'list-room-by-hotel/:hotelId', component: RoomComponent
  },
  {
    path: 'order-room/:hotelId', component: OrderRoomComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HotelsRoutingModule { }
