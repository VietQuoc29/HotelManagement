import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { ToastrService } from 'ngx-toastr';
import { StringEmpty } from 'src/app/constants/constants';
import { OrderRoomService } from 'src/app/services/order-room.service';
import { RegisterRoomsService } from 'src/app/services/register-rooms.service';
import { ViewPaymentModalComponent } from './view-payment-modal/view-payment-modal.component';
import { ViewRegisterRoomModalComponent } from './view-register-room-modal/view-register-room-modal.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  listData: any[] = [];
  searchForm!: FormGroup;
  isLoading = false;
  total = 0;
  pageIndex = 1;
  pageSize = 10;
  textSearch = StringEmpty;

  listDataRegisterRooms: any[] = [];
  searchFormRegisterRooms!: FormGroup;
  isLoadingRegisterRooms = false;
  totalRegisterRooms = 0;
  pageIndexRegisterRooms = 1;
  pageSizeRegisterRooms = 10;
  textSearchRegisterRooms = StringEmpty;

  constructor(
    private fb: FormBuilder,
    private toastService: ToastrService,
    private orderRoomService: OrderRoomService,
    private modal: NzModalService,
    private viewContainerRef: ViewContainerRef,
    private titleService: Title,
    private registerRoomsService: RegisterRoomsService
  ) {}

  ngOnInit(): void {
    this.titleService.setTitle('Quản Lý Khách Sạn - Trang Chủ');
    this.searchForm = this.fb.group({
      textSearch: [StringEmpty],
    });

    this.searchFormRegisterRooms = this.fb.group({
      textSearchRegisterRooms: [StringEmpty],
    });
  }

  search(value: any): void {
    this.searchForm.patchValue({ textSearch: value.textSearch });
    const { textSearch } = this.searchForm.value;
    const params = {
      page: 1,
      pageSize: this.pageSize,
      searchText: textSearch,
    };
    this.getData(params, true);
  }

  searchRegisterRooms(value: any): void {
    this.searchFormRegisterRooms.patchValue({
      textSearchRegisterRooms: value.textSearchRegisterRooms,
    });
    const { textSearchRegisterRooms } = this.searchFormRegisterRooms.value;
    const params = {
      page: 1,
      pageSize: this.pageSize,
      searchText: textSearchRegisterRooms,
    };
    this.getDataRegisterRooms(params, true);
  }

  getData(request?: any, isReset = false): void {
    this.isLoading = true;
    const { textSearch } = this.searchForm.value;
    if (isReset) {
      this.pageIndex = request?.page;
      this.pageSize = request?.pageSize;
    }
    const param = {
      page: request?.page || this.pageIndex,
      pageSize: request?.pageSize || this.pageSize,
      searchText: request?.searchText || textSearch,
    };
    this.orderRoomService.get(param).subscribe((data) => {
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

  getDataRegisterRooms(request?: any, isReset = false): void {
    this.isLoadingRegisterRooms = true;
    const { textSearchRegisterRooms } = this.searchFormRegisterRooms.value;
    if (isReset) {
      this.pageIndexRegisterRooms = request?.page;
      this.pageSizeRegisterRooms = request?.pageSize;
    }
    const param = {
      page: request?.page || this.pageIndexRegisterRooms,
      pageSize: request?.pageSize || this.pageSizeRegisterRooms,
      searchText: request?.searchText || textSearchRegisterRooms,
    };
    this.registerRoomsService.get(param).subscribe((data) => {
      if (data.responseCode === 200) {
        this.totalRegisterRooms = data.totalRecords;
        this.listDataRegisterRooms = data.data;
        this.isLoadingRegisterRooms = false;
      } else {
        this.toastService.error(data.responseMessage);
        this.isLoadingRegisterRooms = false;
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
      searchText: textSearch,
    };
    this.getData(request);
  }

  onQueryParamsChangeRegisterRooms(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.pageIndexRegisterRooms = pageIndex;
    this.pageSizeRegisterRooms = pageSize;
    const { textSearchRegisterRooms } = this.searchFormRegisterRooms.value;
    const request = {
      page: this.pageIndexRegisterRooms,
      pageSize: this.pageSizeRegisterRooms,
      searchText: textSearchRegisterRooms,
    };
    this.getDataRegisterRooms(request);
  }

  resetForm(): void {
    const params = {
      page: 1,
      pageSize: 10,
      searchText: this.searchForm.controls['textSearch'].setValue(StringEmpty),
    };
    this.getData(params, true);
  }

  resetFormRegisterRooms(): void {
    const params = {
      page: 1,
      pageSize: 10,
      searchText:
        this.searchFormRegisterRooms.controls[
          'textSearchRegisterRooms'
        ].setValue(StringEmpty),
    };
    this.getDataRegisterRooms(params, true);
  }

  openModal(data?: any): void {
    const modal = this.modal.create({
      nzTitle: 'Thông tin chi tiết',
      nzContent: ViewPaymentModalComponent,
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

  openModalRegisterRooms(data?: any): void {
    const modal = this.modal.create({
      nzTitle: 'Thông tin chi tiết',
      nzContent: ViewRegisterRoomModalComponent,
      nzViewContainerRef: this.viewContainerRef,
      nzWidth: '700px',
      nzComponentParams: {
        data,
      },
      nzFooter: null,
      nzMaskClosable: false,
      nzClosable: false,
    });
    modal.afterClose.subscribe((result) => {
      if (result && result.reLoad) {
        this.getDataRegisterRooms();
      }
    });
  }
}
