import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { id } from 'date-fns/locale';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { DATE_FORMAT, StringEmpty } from 'src/app/constants/constants';
import { RoomService } from 'src/app/services/room.service';

@Component({
  selector: 'app-return-room-modal',
  templateUrl: './return-room-modal.component.html',
  styleUrls: ['./return-room-modal.component.scss'],
})
export class ReturnRoomModalComponent implements OnInit {
  modalForm!: FormGroup;
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
  isLoading = true;

  constructor(
    private fb: FormBuilder,
    private roomService: RoomService,
    private toastService: ToastrService,
    private modal: NzModalRef
  ) {}

  ngOnInit(): void {
    this.roomService
      .getInfoReturnRoom(this.data.id, true, 0)
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
        this.isLoading = false;
      });

    this.modalForm = this.fb.group({
      orderRoomId: [0],
      endTime: [StringEmpty],
      totalPayment: [StringEmpty],
    });
  }

  submitForm(): void {
    this.isLoading = true;
    let msgError = StringEmpty;

    const params = this.modalForm.value;
    params.orderRoomId = this.orderRoomId;
    params.endTime = this.endTime;
    params.totalPayment = params.totalPayment || StringEmpty;

    if (params.totalPayment == StringEmpty) {
      msgError = 'Vui lòng nhập số tiền khách hàng thanh toán';
    } else {
      if (
        params.totalPayment !=
        parseFloat(this.totalPaymentTemp.toString()).toFixed(0)
      ) {
        msgError =
          'Số tiền khách hàng thanh toán không khớp. Vui lòng kiểm tra lại';
      }
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));

    this.roomService.paymentRoom(formData).subscribe((data) => {
      if (data.responseCode === 200) {
        this.toastService.success(data.responseMessage);
        this.cancelModal(true);
        this.isLoading = false;
      } else {
        this.toastService.error(data.responseMessage);
        this.isLoading = false;
      }
    });
  }

  cancelModal(reLoad: any): void {
    this.modal.destroy({ reLoad });
  }
}
