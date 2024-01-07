import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import {
  DATE_TIME_FORMAT,
  JPEG,
  JPG,
  PNG,
  StringEmpty,
} from 'src/app/constants/constants';
import { ServiceCategoriesService } from 'src/app/services/service-categories.service';
import { ServiceService } from 'src/app/services/services.service';

@Component({
  selector: 'app-service-modal',
  templateUrl: './service-modal.component.html',
  styleUrls: ['./service-modal.component.scss'],
})
export class ServiceModalComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  dateFormat = DATE_TIME_FORMAT;
  isShowInfo = false;
  listServiceCategories: any[] = [];
  imageSrc = StringEmpty;
  @ViewChild('myFile') myFileVariable!: ElementRef;

  constructor(
    private fb: FormBuilder,
    private toastService: ToastrService,
    private serviceService: ServiceService,
    private serviceCategoriesService: ServiceCategoriesService,
    private modal: NzModalRef
  ) {}

  ngOnInit(): void {
    this.suggestion();

    this.modalForm = this.fb.group({
      id: [0],
      name: [StringEmpty],
      price: [StringEmpty],
      unit: [StringEmpty],
      status: ['1'],
      note: [StringEmpty],
      serviceCategoriesId: [StringEmpty],
      createdBy: [StringEmpty],
      createdDate: [StringEmpty],
      modifiedBy: [StringEmpty],
      modifiedDate: [StringEmpty],
      file: [null],
    });

    if (this.data) {
      this.isShowInfo = true;
      this.modalForm.patchValue(this.data);
      this.modalForm.controls['status'].setValue(
        this.data.status === true ? '1' : '0'
      );
      this.imageSrc = this.data.image;
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
    let msgError = StringEmpty;

    const params = this.modalForm.value;
    params.name = params.name || StringEmpty;
    params.price = params.price || StringEmpty;
    params.unit = params.unit || StringEmpty;
    params.status = params.status === '1' ? true : false;
    params.note = params.note || StringEmpty;
    params.serviceCategoriesId = params.serviceCategoriesId || StringEmpty;

    if (params.name == StringEmpty) {
      msgError = 'Vui lòng nhập Tên dịch vụ<br/>';
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));
    this.serviceService.save(formData).subscribe((data) => {
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
    this.serviceCategoriesService.suggestion().subscribe((data) => {
      if (data.responseCode === 200) {
        this.listServiceCategories = data.data;
      } else {
        this.toastService.error(data.responseMessage);
      }
    });
  }

  onFileChange(event: any) {
    const reader = new FileReader();
    const messError = 'Vui lòng chọn File có định dạng ảnh';
    const typeFile = event.target.files[0]
      ? event.target.files[0].name.split('.').pop()
      : StringEmpty;

    if (event.target.files && event.target.files.length) {
      if (
        typeFile.toLowerCase() !== PNG.toLowerCase() &&
        typeFile.toLowerCase() !== JPG.toLowerCase() &&
        typeFile.toLowerCase() !== JPEG.toLowerCase()
      ) {
        this.imageSrc = StringEmpty;
        this.myFileVariable.nativeElement.value = StringEmpty;
        this.modalForm.patchValue({
          file: null,
        });
        this.toastService.error(messError);
        return;
      }
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.modalForm.patchValue({
          file,
        });
      };
    } else {
      this.imageSrc = StringEmpty;
      this.myFileVariable.nativeElement.value = StringEmpty;
      this.modalForm.patchValue({
        file: null,
      });
    }
  }
}
