import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { DATE_TIME_FORMAT, StringEmpty } from 'src/app/constants/constants';
import { RegisterRoomsService } from 'src/app/services/register-rooms.service';

@Component({
  selector: 'app-view-register-room-modal',
  templateUrl: './view-register-room-modal.component.html',
  styleUrls: ['./view-register-room-modal.component.scss'],
})
export class ViewRegisterRoomModalComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  dateFormat = DATE_TIME_FORMAT;

  constructor(
    private fb: FormBuilder,
    private toastService: ToastrService,
    private registerRoomsService: RegisterRoomsService,
    private modal: NzModalRef,
    private route: ActivatedRoute,
    public datepipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.modalForm = this.fb.group({
      id: [0],
      fullName: [StringEmpty],
      email: [StringEmpty],
      phoneNumber: [StringEmpty],
      roomName: [StringEmpty],
      promotionalPrice: [StringEmpty],
      timeFrom: [StringEmpty],
      timeTo: [StringEmpty],
      status: [StringEmpty],
      statusName: [StringEmpty],
      createdDate: [StringEmpty],
      modifiedBy: [StringEmpty],
      modifiedDate: [StringEmpty],
      time: [StringEmpty],
      note: [StringEmpty],
      message: [StringEmpty],
    });

    if (this.data) {
      this.modalForm.patchValue(this.data);
      this.modalForm.controls['status'].setValue(
        this.data.status === true ? '1' : '0'
      );

      let timeTemp =
        this.datepipe.transform(this.data.timeFrom, DATE_TIME_FORMAT) +
        ' - ' +
        this.datepipe.transform(this.data.timeTo, DATE_TIME_FORMAT);
      this.modalForm.controls['time'].setValue(timeTemp);
    }

    setTimeout(() => {
      this.isLoading = false;
    }, 500);
  }

  cancelModal(reLoad: any): void {
    this.modal.destroy({ reLoad });
  }

  submitForm(): void {
    this.isLoading = true;

    const params = this.modalForm.value;
    params.status = params.status === '1' ? true : false;
    params.message = params.message || StringEmpty;

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));

    this.registerRoomsService.save(formData).subscribe((data) => {
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
}
