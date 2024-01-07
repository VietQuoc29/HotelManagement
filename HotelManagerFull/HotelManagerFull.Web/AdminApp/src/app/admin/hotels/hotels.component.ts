import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { ToastrService } from 'ngx-toastr';
import { StringEmpty } from 'src/app/constants/constants';
import { HotelService } from 'src/app/services/hotels.service';
import { HotelModalComponent } from './hotel-modal/hotel-modal.component';

@Component({
  selector: 'app-hotels',
  templateUrl: './hotels.component.html',
  styleUrls: ['./hotels.component.scss'],
})
export class HotelsComponent implements OnInit {
  listData: any[] = [];
  searchForm!: FormGroup;
  isLoading = false;
  total = 0;
  pageIndex = 1;
  pageSize = 10;
  textSearch = StringEmpty;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private hotelService: HotelService,
    private toastService: ToastrService,
    private viewContainerRef: ViewContainerRef,
    private modal: NzModalService,
    private titleService: Title
  ) { }

  ngOnInit(): void {
    this.titleService.setTitle('Quản Lý Khách Sạn - Khách Sạn');
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
      searchText: textSearch,
    };
    this.getData(params, true);
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
    this.hotelService.get(param).subscribe((data) => {
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
      searchText: textSearch,
    };
    this.getData(request);
  }

  cancel(): void { }

  confirm(id: any): void {
    this.isLoading = true;
    this.hotelService.delete(id).subscribe((result) => {
      if (result.responseCode === 200) {
        this.toastService.success(result.responseMessage);
        this.getData();
        if (this.listData.length === 1) {
          this.total = this.total / this.pageSize;
        }
      } else {
        this.toastService.error(result.responseMessage);
        this.isLoading = false;
      }
    });
  }

  openModal(data?: any): void {
    const modal = this.modal.create({
      nzTitle: data ? 'Cập nhật thông tin' : 'Thêm mới thông tin',
      nzContent: HotelModalComponent,
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

  resetForm(): void {
    const params = {
      page: 1,
      pageSize: 10,
      searchText: this.searchForm.controls['textSearch'].setValue(StringEmpty),
    };
    this.getData(params, true);
  }

  goToDashborad() {
    this.router.navigate(['admin/dashboard']);
  }
}
