import { Component, Input, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';
import { differenceInCalendarDays } from 'date-fns';
import * as moment from 'moment';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import {
  DATE_FORMAT,
  DATE_TIME_FORMAT,
  MOMENT_DATE_FORMAT,
  StringEmpty,
} from 'src/app/constants/constants';
import { CustomersService } from 'src/app/services/customers.service';
import { SexService } from 'src/app/services/sex.service';

@Component({
  selector: 'app-customers-modal',
  templateUrl: './customers-modal.component.html',
  styleUrls: ['./customers-modal.component.scss'],
})
export class CustomersModalComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  dateFormat = DATE_TIME_FORMAT;
  dateFormatDateOfBirth = DATE_FORMAT;
  isShowInfo = false;
  listSex: any[] = [];

  constructor(
    private fb: FormBuilder,
    private toastService: ToastrService,
    private customersService: CustomersService,
    private modal: NzModalRef,
    private sexService: SexService
  ) {}

  ngOnInit(): void {
    this.suggestion();

    this.modalForm = this.fb.group({
      id: [0],
      fullName: [StringEmpty],
      idCard: [StringEmpty],
      phoneNumber: [StringEmpty],
      address: [StringEmpty],
      dateOfBirth: [StringEmpty],
      note: [StringEmpty],
      sexId: [StringEmpty],
      createdBy: [StringEmpty],
      createdDate: [StringEmpty],
      modifiedBy: [StringEmpty],
      modifiedDate: [StringEmpty],
    });

    if (this.data) {
      this.isShowInfo = true;
      this.modalForm.patchValue(this.data);
    }

    setTimeout(() => {
      this.isLoading = false;
    }, 500);
  }

  disabledDate = (current: Date): boolean => {
    // Can not select days before today and today
    return differenceInCalendarDays(current, new Date()) > 0;
    // tslint:disable-next-line:semicolon
  };

  cancelModal(reLoad: any): void {
    this.modal.destroy({ reLoad });
  }

  submitForm(): void {
    this.isLoading = true;
    let msgError = StringEmpty;

    const params = this.modalForm.value;
    params.fullName = params.fullName || StringEmpty;
    params.idCard = params.idCard || StringEmpty;
    params.phoneNumber = params.phoneNumber || StringEmpty;
    params.address = params.address || StringEmpty;
    params.dateOfBirth = params.dateOfBirth
      ? moment(params.dateOfBirth).format(MOMENT_DATE_FORMAT)
      : StringEmpty;
    params.note = params.note || StringEmpty;
    params.sexId = params.sexId || StringEmpty;

    if (params.fullName == StringEmpty) {
      msgError = 'Vui lòng nhập Tên đầy đủ<br/>';
    }

    if (params.phoneNumber == StringEmpty) {
      msgError += 'Vui lòng nhập Số điện thoại<br/>';
    }

    if (params.idCard == StringEmpty) {
      msgError += 'Vui lòng nhập Số CMD / Thẻ CCCD<br/>';
    } else {
      let checkIsNumberOnly = new FormControl(params.idCard, [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
        Validators.minLength(9),
        Validators.maxLength(12),
      ]);
      if (checkIsNumberOnly.status == 'INVALID') {
        msgError +=
          'Số CMD / Thẻ CCCD phải là số (9 số đối với CMT, 12 số đối với CCCD)<br/>';
      }
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));
    this.customersService.save(formData).subscribe((data) => {
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

  suggestion(): void {
    this.sexService.suggestion().subscribe((data) => {
      if (data.responseCode === 200) {
        this.listSex = data.data;
      } else {
        this.toastService.error(data.responseMessage);
      }
    });
  }
}
