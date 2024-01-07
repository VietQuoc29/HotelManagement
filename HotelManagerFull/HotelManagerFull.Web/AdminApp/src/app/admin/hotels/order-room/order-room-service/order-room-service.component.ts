import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { StringEmpty } from 'src/app/constants/constants';
import { ServiceCategoriesService } from 'src/app/services/service-categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-order-room-service',
  templateUrl: './order-room-service.component.html',
  styleUrls: ['./order-room-service.component.scss'],
})
export class OrderRoomServiceComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  listSerrviceByServiceCategories: any[] = [];
  listTransactions: any[] = [];
  newTransactions: any = {};

  constructor(
    private fb: FormBuilder,
    private modal: NzModalRef,
    private toastService: ToastrService,
    private serviceCategoriesService: ServiceCategoriesService,
    private transactionsService: TransactionsService
  ) {}

  ngOnInit(): void {
    this.getSerrviceByServiceCategories();

    this.modalForm = this.fb.group({});
  }

  submitForm(): void {
    this.isLoading = true;
    let msgError = StringEmpty;

    if (this.listTransactions.length == 0) {
      msgError = 'Bạn cần chọn ít nhất 1 dịch vụ<br>';
    }

    const formData = new FormData();
    for (let i = 0; i < this.listTransactions.length; i++) {
      formData.append(
        'listTransactions[' + i + '][orderRoomId]',
        this.listTransactions[i].orderRoomId
      );
      formData.append(
        'listTransactions[' + i + '][serviceId]',
        this.listTransactions[i].serviceId
      );

      if (
        this.listTransactions[i].quantity == null ||
        this.listTransactions[i].quantity <= 0
      ) {
        msgError += 'Số lượng không hợp lệ';
      } else {
        formData.append(
          'listTransactions[' + i + '][quantity]',
          this.listTransactions[i].quantity
        );
      }
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    this.transactionsService.save(formData).subscribe((data) => {
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

  getSerrviceByServiceCategories(): void {
    this.serviceCategoriesService
      .getAllSerrviceByServiceCategories()
      .subscribe((data) => {
        if (data.responseCode === 200) {
          this.listSerrviceByServiceCategories = data.data;
          this.isLoading = false;
        } else {
          this.toastService.error(data.responseMessage);
        }
      });
  }

  addFieldValue(serviceId: number, serviceName?: string) {
    let exist = this.listTransactions.filter(function (hero) {
      return hero.serviceId == serviceId;
    });

    if (exist.length == 0) {
      this.listTransactions.push(
        (this.newTransactions = {
          orderRoomId: this.data.orderRoomId,
          serviceId: serviceId,
          quantity: 1,
          name: serviceName,
        })
      );
    } else {
      this.toastService.warning(
        'Dịch vụ đã có trong giỏ, vui lòng cập nhật số lượng'
      );
    }

    this.newTransactions = {};
  }

  deleteFieldValue(index: number) {
    this.listTransactions.splice(index, 1);
  }
}
