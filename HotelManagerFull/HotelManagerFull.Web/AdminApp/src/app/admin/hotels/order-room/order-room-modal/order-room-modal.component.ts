import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { DATE_FORMAT, StringEmpty } from 'src/app/constants/constants';
import { CustomersService } from 'src/app/services/customers.service';
import { OrderRoomService } from 'src/app/services/order-room.service';

@Component({
  selector: 'app-order-room-modal',
  templateUrl: './order-room-modal.component.html',
  styleUrls: ['./order-room-modal.component.scss'],
})
export class OrderRoomModalComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  listCustomer: any[] = [];
  nzFilterOption = () => true;

  constructor(
    private fb: FormBuilder,
    private modal: NzModalRef,
    private router: Router,
    private toastService: ToastrService,
    private datepipe: DatePipe,
    private customersService: CustomersService,
    private orderRoomService: OrderRoomService
  ) {}

  ngOnInit(): void {
    this.modalForm = this.fb.group({
      id: [0],
      customerId: [StringEmpty],
      roomId: [this.data.id],
      fullName: [StringEmpty],
      phoneNumber: [StringEmpty],
      address: [StringEmpty],
      sexName: [StringEmpty],
      dateOfBirth: [StringEmpty],
      note: [StringEmpty],
    });

    setTimeout(() => {
      this.isLoading = false;
    }, 500);
  }

  submitForm(): void {
    this.isLoading = true;
    let msgError = StringEmpty;

    const params = this.modalForm.value;
    params.customerId = params.customerId || StringEmpty;

    if (params.customerId == StringEmpty) {
      msgError = 'Vui lòng nhập khách hàng';
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));

    this.orderRoomService.save(formData).subscribe((data) => {
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

  searchCustomer(value: string): void {
    if (value.length > 2) {
      this.isLoading = true;
      this.customersService.suggestion(value).subscribe((data) => {
        if (data.responseCode === 200) {
          this.listCustomer = data.data;
          this.isLoading = false;
        }
      });
    }
  }

  changeCustomer(id: any) {
    this.isLoading = true;
    this.customersService.getCustomerInfo(id).subscribe((data) => {
      this.modalForm.controls['fullName'].setValue(data.fullName);
      this.modalForm.controls['phoneNumber'].setValue(data.phoneNumber);
      this.modalForm.controls['address'].setValue(data.address);
      this.modalForm.controls['sexName'].setValue(
        data.sexId == 1 ? 'Nam' : 'Nữ'
      );
      this.modalForm.controls['dateOfBirth'].setValue(
        this.datepipe.transform(data.dateOfBirth, DATE_FORMAT)
      );
      this.modalForm.controls['note'].setValue(data.note);
      this.isLoading = false;
    });
  }

  goToCustomer() {
    const url = this.router.serializeUrl(
      this.router.createUrlTree(['admin/customers'])
    );
    window.open(url, '_blank');
  }
}
