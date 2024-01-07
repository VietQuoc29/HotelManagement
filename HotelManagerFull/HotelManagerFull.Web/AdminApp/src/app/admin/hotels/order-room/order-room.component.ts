import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { ToastrService } from 'ngx-toastr';
import { StringEmpty } from 'src/app/constants/constants';
import { RoomStatusService } from 'src/app/services/room-status.service';
import { RoomService } from 'src/app/services/room.service';
import { OrderRoomModalComponent } from './order-room-modal/order-room-modal.component';
import { OrderRoomServiceComponent } from './order-room-service/order-room-service.component';
import { ReturnRoomModalComponent } from './return-room-modal/return-room-modal.component';

@Component({
  selector: 'app-order-room',
  templateUrl: './order-room.component.html',
  styleUrls: ['./order-room.component.scss'],
})
export class OrderRoomComponent implements OnInit {
  listData: any[] = [];
  searchForm!: FormGroup;
  isLoading = false;
  total = 0;
  pageIndex = 1;
  pageSize = 45;
  textSearch = StringEmpty;
  listRoomStatus: any[] = [];
  hotelId = 0;
  modalForm!: FormGroup;

  constructor(
    private router: Router,
    private titleService: Title,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private toastService: ToastrService,
    private roomService: RoomService,
    private modal: NzModalService,
    private viewContainerRef: ViewContainerRef,
    private roomStatusService: RoomStatusService
  ) {}

  ngOnInit(): void {
    this.titleService.setTitle('Quản Lý Khách Sạn - Quản Lý Thuê Phòng');

    this.suggestionRoomStatus();
    this.route.params.subscribe((params) => {
      this.hotelId = params['hotelId'];
    });

    this.searchForm = this.fb.group({
      textSearch: [StringEmpty],
    });
  }

  search(value: any): void {
    this.searchForm.patchValue({ textSearch: value.textSearch });
    const { textSearch } = this.searchForm.value;
    const params = {
      page: 1,
      pageSize: this.pageSize,
      roomStatusId: textSearch == null ? StringEmpty : textSearch,
    };
    this.getData(params);
  }

  getData(request?: any): void {
    this.isLoading = true;
    const { textSearch } = this.searchForm.value;
    const param = {
      page: request?.page || this.pageIndex,
      pageSize: request?.pageSize || this.pageSize,
      roomStatusId: request?.roomStatusId || textSearch,
      hotelId: Number(this.hotelId),
    };
    this.roomService.getInfoRoom(param).subscribe((data) => {
      if (data.responseCode === 200) {
        this.total = data.totalRecords;
        this.listData = data.data;
        this.isLoading = false;
      } else {
        this.toastService.error(data.responseMessage);
        this.isLoading = false;
      }
    });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;
    const { textSearch } = this.searchForm.value;
    const request = {
      page: this.pageIndex,
      pageSize: this.pageSize,
      roomStatusId: textSearch,
    };
    this.getData(request);
  }

  suggestionRoomStatus(): void {
    this.roomStatusService.suggestion().subscribe((data) => {
      if (data.responseCode === 200) {
        this.listRoomStatus = data.data;
      } else {
        this.toastService.error(data.responseMessage);
      }
    });
  }

  operationRoom(id: number, roomStatusId: number) {
    this.isLoading = true;
    this.modalForm = this.fb.group({
      id: [0],
      roomStatusId: [StringEmpty],
    });
    const params = this.modalForm.value;
    params.id = id;
    params.roomStatusId = roomStatusId;

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));

    this.roomService.updateRoomStatus(formData).subscribe((data) => {
      if (data.responseCode === 200) {
        this.toastService.success(data.responseMessage);
        setTimeout(() => {
          window.location.reload();
        }, 500);
      } else {
        this.toastService.error(data.responseMessage);
        this.isLoading = false;
      }
    });
  }

  openModal(data?: any): void {
    const modal = this.modal.create({
      nzTitle: 'Màn hình thông tin khách hàng đặt phòng',
      nzContent: OrderRoomModalComponent,
      nzViewContainerRef: this.viewContainerRef,
      nzWidth: '800px',
      nzComponentParams: {
        data,
      },
      nzFooter: null,
      nzMaskClosable: false,
      nzClosable: false,
    });
    modal.afterClose.subscribe((result) => {
      if (result && result.reLoad) {
        this.getData();
      }
    });
  }

  callService(data?: any): void {
    const modal = this.modal.create({
      nzTitle: 'Dịch vụ khách sạn',
      nzContent: OrderRoomServiceComponent,
      nzViewContainerRef: this.viewContainerRef,
      nzWidth: '100%',
      nzComponentParams: {
        data,
      },
      nzFooter: null,
      nzMaskClosable: false,
      nzClosable: false,
    });
    modal.afterClose.subscribe((result) => {
      if (result && result.reLoad) {
        this.getData();
      }
    });
  }

  returnRoom(data?: any): void {
    const modal = this.modal.create({
      nzTitle: 'Hoá đơn thanh toán',
      nzContent: ReturnRoomModalComponent,
      nzViewContainerRef: this.viewContainerRef,
      nzWidth: '80%',
      nzComponentParams: {
        data,
      },
      nzFooter: null,
      nzMaskClosable: false,
      nzClosable: false,
    });
    modal.afterClose.subscribe((result) => {
      if (result && result.reLoad) {
        this.getData();
      }
    });
  }

  goToDashborad() {
    this.router.navigate(['admin/dashboard']);
  }
}
