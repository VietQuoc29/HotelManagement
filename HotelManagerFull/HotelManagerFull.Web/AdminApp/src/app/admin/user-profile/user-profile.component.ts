import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { ToastrService } from 'ngx-toastr';
import { StringEmpty } from 'src/app/constants/constants';
import { UserProfileService } from 'src/app/services/user-profile.service';
import { UserProfileModalComponent } from './user-profile-modal/user-profile-modal.component';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
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
    private userProfileService: UserProfileService,
    private toastService: ToastrService,
    private viewContainerRef: ViewContainerRef,
    private modal: NzModalService,
    private titleService: Title) { }

  ngOnInit(): void {
    this.titleService.setTitle('Quản Lý Khách Sạn - Người Dùng');
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
    this.userProfileService.get(param).subscribe((data) => {
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

  openModal(data?: any): void {
    const modal = this.modal.create({
      nzTitle: data ? 'Cập nhật thông tin' : 'Thêm mới thông tin',
      nzContent: UserProfileModalComponent,
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
  
  toggleActive(data: any, index: number): void {
    this.isLoading = true;
    const params = {
      id: data.id,
      isActive: !data.active,
    };
    this.userProfileService.activeUser(params).subscribe((result) => {
      if (result.responseCode === 200) {
        this.toastService.success(result.responseMessage);
        this.listData[index].active = !data.active;
        this.isLoading = false;
      } else {
        this.toastService.error(result.responseMessage);
        this.isLoading = false;
      }
    });
  }
}