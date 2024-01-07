import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import {
  DATE_TIME_FORMAT,
  JPEG,
  JPG,
  PNG,
  StringEmpty,
} from 'src/app/constants/constants';
import { ProvincesService } from 'src/app/services/provinces.service';

@Component({
  selector: 'app-provinces-modal',
  templateUrl: './provinces-modal.component.html',
  styleUrls: ['./provinces-modal.component.scss'],
})
export class ProvincesModalComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  dateFormat = DATE_TIME_FORMAT;
  isShowInfo = false;
  imageSrc = StringEmpty;
  @ViewChild('myFile') myFileVariable!: ElementRef;

  constructor(
    private fb: FormBuilder,
    private toastService: ToastrService,
    private provincesService: ProvincesService,
    private modal: NzModalRef
  ) {}

  ngOnInit(): void {
    this.modalForm = this.fb.group({
      id: [0],
      name: [StringEmpty],
      createdBy: [StringEmpty],
      createdDate: [StringEmpty],
      modifiedBy: [StringEmpty],
      modifiedDate: [StringEmpty],
      file: [null],
    });

    if (this.data) {
      this.isShowInfo = true;
      this.modalForm.patchValue(this.data);
      this.imageSrc = this.data.imageLink;
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

    if (params.name == StringEmpty) {
      msgError = 'Vui lòng nhập Tên tỉnh thành<br/>';
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));
    this.provincesService.save(formData).subscribe((data) => {
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
