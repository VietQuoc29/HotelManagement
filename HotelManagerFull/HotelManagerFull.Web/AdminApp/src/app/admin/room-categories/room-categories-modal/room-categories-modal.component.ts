import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { DATE_TIME_FORMAT, StringEmpty } from 'src/app/constants/constants';
import { RoomCategoriesService } from 'src/app/services/room-categories.service';

@Component({
  selector: 'app-room-categories-modal',
  templateUrl: './room-categories-modal.component.html',
  styleUrls: ['./room-categories-modal.component.scss']
})
export class RoomCategoriesModalComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  dateFormat = DATE_TIME_FORMAT;
  isShowInfo = false;

  constructor(
    private fb: FormBuilder,
    private toastService: ToastrService,
    private roomCategoriesService: RoomCategoriesService,
    private modal: NzModalRef) { }

  ngOnInit(): void {
    this.modalForm = this.fb.group({
      id: [0],
      name: [StringEmpty],
      createdBy: [StringEmpty],
      createdDate: [StringEmpty],
      modifiedBy: [StringEmpty],
      modifiedDate: [StringEmpty],
    });

    if (this.data) {
      this.isShowInfo = true;
      this.modalForm.patchValue(this.data);
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
      msgError = 'Vui lòng nhập Tên loại phòng<br/>';
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));
    this.roomCategoriesService.save(formData).subscribe((data) => {
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

}
