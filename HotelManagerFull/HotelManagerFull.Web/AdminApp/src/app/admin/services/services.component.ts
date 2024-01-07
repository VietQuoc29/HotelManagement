import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { ToastrService } from 'ngx-toastr';
import { StringEmpty } from 'src/app/constants/constants';
import { ServiceCategoriesService } from 'src/app/services/service-categories.service';
import { ServiceService } from 'src/app/services/services.service';
import { ServiceCategoriesModalComponent } from './service-categories-modal/service-categories-modal.component';
import { ServiceModalComponent } from './service-modal/service-modal.component';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.scss']
})
export class ServicesComponent implements OnInit {
  //ServiceCategories
  listDataServiceCategories: any[] = [];
  searchFormServiceCategories!: FormGroup;
  isLoadingServiceCategories = false;
  totalServiceCategories = 0;
  pageIndexServiceCategories = 1;
  pageSizeServiceCategories = 10;
  textSearchServiceCategories = StringEmpty;

  //Service
  listDataService: any[] = [];
  searchFormService!: FormGroup;
  isLoadingService = false;
  totalService = 0;
  pageIndexService = 1;
  pageSizeService = 10;
  textSearchService = StringEmpty;

  constructor(
    private router: Router,
    private fbServiceCategories: FormBuilder,
    private fbService: FormBuilder,
    private serviceCategoriesService: ServiceCategoriesService,
    private serviceService: ServiceService,
    private toastService: ToastrService,
    private viewContainerRef: ViewContainerRef,
    private modal: NzModalService,
    private titleService: Title) { }

  ngOnInit(): void {
    this.titleService.setTitle('Quản Lý Khách Sạn - Dịch Vụ');

    //ServiceCategories
    this.searchFormServiceCategories = this.fbServiceCategories.group({
      textSearchServiceCategories: [StringEmpty],
    });

    //Service
    this.searchFormService = this.fbService.group({
      textSearchService: [StringEmpty],
    });
  }

  //ServiceCategories
  searchServiceCategories(value: any): void {
    this.searchFormServiceCategories.patchValue({ textSearchServiceCategories: value.textSearchServiceCategories });
    const { textSearchServiceCategories } = this.searchFormServiceCategories.value;
    const params = {
      page: 1,
      pageSize: this.pageSizeServiceCategories,
      searchText: textSearchServiceCategories,
    };
    this.getDataServiceCategories(params, true);
  }

  getDataServiceCategories(request?: any, isReset = false): void {
    this.isLoadingServiceCategories = true;
    const { textSearchServiceCategories } = this.searchFormServiceCategories.value;
    if (isReset) {
      this.pageIndexServiceCategories = request?.page;
      this.pageSizeServiceCategories = request?.pageSize;
    }
    const param = {
      page: request?.page || this.pageIndexServiceCategories,
      pageSize: request?.pageSize || this.pageSizeServiceCategories,
      searchText: request?.searchText || textSearchServiceCategories,
    };
    this.serviceCategoriesService.get(param).subscribe((data) => {
      if (data.responseCode === 200) {
        this.totalServiceCategories = data.totalRecords;
        this.listDataServiceCategories = data.data;
        this.isLoadingServiceCategories = false;
      } else {
        this.toastService.error(data.responseMessage);
        this.isLoadingServiceCategories = false;
      }
    });
  }

  onQueryParamsChangeServiceCategories(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.pageIndexServiceCategories = pageIndex;
    this.pageSizeServiceCategories = pageSize;
    const { textSearchServiceCategories } = this.searchFormServiceCategories.value;
    const request = {
      page: this.pageIndexServiceCategories,
      pageSize: this.pageSizeServiceCategories,
      searchText: textSearchServiceCategories,
    };
    this.getDataServiceCategories(request);
  }

  cancelServiceCategories(): void { }

  confirmServiceCategories(id: any): void {
    this.isLoadingServiceCategories = true;
    this.serviceCategoriesService.delete(id).subscribe((result) => {
      if (result.responseCode === 200) {
        this.toastService.success(result.responseMessage);
        this.getDataServiceCategories();
        if (this.listDataServiceCategories.length === 1) {
          this.totalServiceCategories = this.totalServiceCategories / this.pageSizeServiceCategories;
        }
      } else {
        this.toastService.error(result.responseMessage);
        this.isLoadingServiceCategories = false;
      }
    });
  }

  openModalServiceCategories(data?: any): void {
    const modal = this.modal.create({
      nzTitle: data ? 'Cập nhật thông tin' : 'Thêm mới thông tin',
      nzContent: ServiceCategoriesModalComponent,
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
        this.getDataServiceCategories();
      }
    });
  }

  resetFormServiceCategories(): void {
    const params = {
      page: 1,
      pageSize: 10,
      searchText: this.searchFormServiceCategories.controls['textSearchServiceCategories'].setValue(StringEmpty),
    };
    this.getDataServiceCategories(params, true);
  }

  //Service
  searchService(value: any): void {
    this.searchFormService.patchValue({ textSearchService: value.textSearchService });
    const { textSearchService } = this.searchFormService.value;
    const params = {
      page: 1,
      pageSize: this.pageSizeService,
      searchText: textSearchService,
    };
    this.getDataService(params, true);
  }

  getDataService(request?: any, isReset = false): void {
    this.isLoadingService = true;
    const { textSearchService } = this.searchFormService.value;
    if (isReset) {
      this.pageIndexService = request?.page;
      this.pageSizeService = request?.pageSize;
    }
    const param = {
      page: request?.page || this.pageIndexService,
      pageSize: request?.pageSize || this.pageSizeService,
      searchText: request?.searchText || textSearchService,
    };
    this.serviceService.get(param).subscribe((data) => {
      if (data.responseCode === 200) {
        this.totalService = data.totalRecords;
        this.listDataService = data.data;
        this.isLoadingService = false;
      } else {
        this.toastService.error(data.responseMessage);
        this.isLoadingService = false;
      }
    });
  }

  onQueryParamsChangeService(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.pageIndexService = pageIndex;
    this.pageSizeService = pageSize;
    const { textSearchService } = this.searchFormService.value;
    const request = {
      page: this.pageIndexService,
      pageSize: this.pageSizeService,
      searchText: textSearchService,
    };
    this.getDataService(request);
  }

  cancelService(): void { }

  confirmService(id: any): void {
    this.isLoadingService = true;
    this.serviceService.delete(id).subscribe((result) => {
      if (result.responseCode === 200) {
        this.toastService.success(result.responseMessage);
        this.getDataService();
        if (this.listDataService.length === 1) {
          this.totalService = this.totalService / this.pageSizeService;
        }
      } else {
        this.toastService.error(result.responseMessage);
        this.isLoadingService = false;
      }
    });
  }

  openModalService(data?: any): void {
    const modal = this.modal.create({
      nzTitle: data ? 'Cập nhật thông tin' : 'Thêm mới thông tin',
      nzContent: ServiceModalComponent,
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
        this.getDataService();
      }
    });
  }

  resetFormService(): void {
    const params = {
      page: 1,
      pageSize: 10,
      searchText: this.searchFormService.controls['textSearchService'].setValue(StringEmpty),
    };
    this.getDataService(params, true);
  }

  goToDashborad() {
    this.router.navigate(['admin/dashboard']);
  }
}
