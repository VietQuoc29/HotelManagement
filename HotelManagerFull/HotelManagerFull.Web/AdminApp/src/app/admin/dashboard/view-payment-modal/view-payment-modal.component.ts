import { Component, Input, OnInit } from '@angular/core';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { StringEmpty } from 'src/app/constants/constants';
import { RoomService } from 'src/app/services/room.service';

@Component({
  selector: 'app-view-payment-modal',
  templateUrl: './view-payment-modal.component.html',
  styleUrls: ['./view-payment-modal.component.scss'],
})
export class ViewPaymentModalComponent implements OnInit {
  @Input() data: any;
  provinceName = StringEmpty;
  hotelName = StringEmpty;
  name = StringEmpty;
  floorName = StringEmpty;
  fullName = StringEmpty;
  phoneNumber = StringEmpty;
  idCard = StringEmpty;
  startTime = StringEmpty;
  endTime = StringEmpty;
  totalHour = StringEmpty;
  totalMinutes = StringEmpty;
  createdBy = StringEmpty;
  listTransactions: any[] = [];
  totalPaymentTemp = 0;
  orderRoomId = StringEmpty;
  status = StringEmpty;
  isLoading = true;

  constructor(private roomService: RoomService, private modal: NzModalRef) {}

  ngOnInit(): void {
    this.roomService
      .getInfoReturnRoom(this.data.roomId, false, this.data.id)
      .subscribe((data) => {
        this.provinceName = data.data[0]?.provinceName;
        this.hotelName = data.data[0]?.hotelName;
        this.name = data.data[0]?.name;
        this.floorName = data.data[0]?.floorName;
        this.fullName = data.data[0]?.fullName;
        this.phoneNumber = data.data[0]?.phoneNumber;
        this.idCard = data.data[0]?.idCard;
        this.startTime = data.data[0]?.startTime;
        this.endTime = data.data[0]?.endTime;
        this.totalHour = data.data[0]?.totalHour;
        this.totalMinutes = data.data[0]?.totalMinutes;
        this.createdBy = data.data[0]?.createdBy;
        this.listTransactions = data.data[0]?.listTransactions;
        this.totalPaymentTemp = data.data[0]?.totalPaymentTemp;
        this.orderRoomId = data.data[0]?.orderRoomId;
        this.status =
          data.data[0]?.status == 1 ? 'Đã thanh toán' : 'Chưa thanh toán';
        this.isLoading = false;
      });
  }

  cancelModal(reLoad: any): void {
    this.modal.destroy({ reLoad });
  }
}
