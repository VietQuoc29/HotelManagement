import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { JPEG, JPG, PNG, StringEmpty } from 'src/app/constants/constants';
import { HotelImageService } from 'src/app/services/hotel-image.service';

@Component({
  selector: 'app-image-manager',
  templateUrl: './image-manager.component.html',
  styleUrls: ['./image-manager.component.scss']
})
export class ImageManagerComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  @ViewChild('myFiles') myFilesVariable!: ElementRef;
  listFile: string[] = [];
  listImage: any[] = [];

  constructor(private toastService: ToastrService,
    private fb: FormBuilder,
    private modal: NzModalRef,
    private hotelImageService: HotelImageService) { }

  ngOnInit(): void {
    this.modalForm = this.fb.group({
      id: [0],
      imageLink: [StringEmpty],
      roomId: [StringEmpty],
      listFile: this.fb.array([])
    });

    if (this.data) {
      this.listImage = this.data.listHotelImages;
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
    params.roomId = this.data.id;

    if (this.listFile.length <= 0) {
      msgError = "Vui lòng chọn File ảnh<br/>";
    }

    let totalFile = this.listImage.length + this.listFile.length;
    if (totalFile > 10) {
      msgError += 'Tổng số ảnh vượt quá số lượng 10 ảnh (hiện tại đã có ' + this.listImage.length + ' ảnh)';
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));

    for (var i = 0; i < this.listFile.length; i++) {
      formData.append('listFile', this.listFile[i]);
    }

    this.hotelImageService.uploadImage(formData).subscribe((data) => {
      if (data.responseCode === 200) {
        this.reLoadHotelImage(data.responseMessage);
      } else {
        this.toastService.error(data.responseMessage);
        this.isLoading = false;
      }
    });
  }

  onFileChange(event: any) {
    this.listFile = [];
    for (var i = 0; i < event.target.files.length; i++) {
      const typeFile = event.target.files[0]
        ? event.target.files[0].name.split('.').pop()
        : StringEmpty;

      if (typeFile.toLowerCase() !== PNG.toLowerCase() &&
        typeFile.toLowerCase() !== JPG.toLowerCase() &&
        typeFile.toLowerCase() !== JPEG.toLowerCase()) {
        this.myFilesVariable.nativeElement.value = StringEmpty;
        this.toastService.error("Vui lòng chọn File có định dạng ảnh");
        return;
      } else {
        let data = event.target.files[i];
        //check file have size < 2mb
        if (data.size <= 2097152) {
          this.listFile.push(data);
        } else {
          this.toastService.error("Chú ý: Có 1 ảnh vượt quá 2Mb sẽ không được tải lên");
        }
      }
    }

    if (this.listFile.length <= 0) {
      this.myFilesVariable.nativeElement.value = StringEmpty;
    }
  }

  cancel(): void { }

  confirm(id: any): void {
    this.isLoading = true;
    this.hotelImageService.delete(id).subscribe((result) => {
      if (result.responseCode === 200) {
        this.reLoadHotelImage(result.responseMessage);
      } else {
        this.toastService.error(result.responseMessage);
        this.isLoading = false;
      }
    });
  }

  reLoadHotelImage(responseMessage: string) {
    this.hotelImageService.getAllHotelImage(this.data.id).subscribe((data) => {
      if (data.responseCode === 200) {
        this.listImage = data.data;
        this.listFile = [];
        this.myFilesVariable.nativeElement.value = StringEmpty;
        this.toastService.success(responseMessage);
        this.isLoading = false;
      } else {
        this.toastService.error(responseMessage);
      }
    });
  }
}
