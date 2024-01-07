import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import {
  DATE_TIME_FORMAT,
  JPEG,
  JPG,
  PNG,
  StringEmpty,
} from 'src/app/constants/constants';
import { FloorsService } from 'src/app/services/floors.service';
import { RoomCategoriesService } from 'src/app/services/room-categories.service';
import { RoomStatusService } from 'src/app/services/room-status.service';
import { RoomService } from 'src/app/services/room.service';

@Component({
  selector: 'app-room-modal',
  templateUrl: './room-modal.component.html',
  styleUrls: ['./room-modal.component.scss'],
})
export class RoomModalComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  dateFormat = DATE_TIME_FORMAT;
  isShowInfo = false;
  hotelId = 0;
  listRoomStatus: any[] = [];
  listRoomCategories: any[] = [];
  listFloor: any[] = [];
  @ViewChild('myFiles') myFilesVariable!: ElementRef;
  listFile: string[] = [];

  constructor(
    private fb: FormBuilder,
    private toastService: ToastrService,
    private roomService: RoomService,
    private roomStatusService: RoomStatusService,
    private roomCategoriesService: RoomCategoriesService,
    private floorsService: FloorsService,
    private modal: NzModalRef,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.hotelId = params['hotelId'];
    });

    this.suggestionRoomStatus();
    this.suggestionRoomCategories();
    this.suggestionFloor();

    this.modalForm = this.fb.group({
      id: [0],
      name: [StringEmpty],
      price: [StringEmpty],
      promotionalPrice: [StringEmpty],
      star: ['1'],
      isActive: ['1'],
      roomStatusId: [1],
      roomCategoriesId: [StringEmpty],
      hotelId: [Number(this.hotelId)],
      floorId: [StringEmpty],
      createdBy: [StringEmpty],
      createdDate: [StringEmpty],
      modifiedBy: [StringEmpty],
      modifiedDate: [StringEmpty],
      listFile: this.fb.array([]),
    });

    if (this.data) {
      this.isShowInfo = true;
      this.modalForm.patchValue(this.data);
      this.modalForm.controls['star'].setValue(this.data.star.toString());
      this.modalForm.controls['isActive'].setValue(
        this.data.isActive === true ? '1' : '0'
      );
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
    params.promotionalPrice = params.promotionalPrice || StringEmpty;
    params.star = params.star || StringEmpty;
    params.isActive = params.isActive === '1' ? true : false;
    params.roomStatusId = params.roomStatusId || StringEmpty;
    params.roomCategoriesId = params.roomCategoriesId || StringEmpty;
    params.hotelId = params.hotelId;
    params.floorId = params.floorId || StringEmpty;

    if (params.name == StringEmpty) {
      msgError = 'Vui lòng nhập Tên phòng<br/>';
    }

    if (params.price == StringEmpty || params.price == 0) {
      msgError += 'Vui lòng nhập Giá phòng và phải lớn hơn 0<br/>';
    }

    if (
      params.promotionalPrice == StringEmpty ||
      params.promotionalPrice == 0
    ) {
      msgError += 'Vui lòng nhập Giá khuyến mại và phải lớn hơn 0<br/>';
    }

    if (
      params.price != StringEmpty &&
      params.promotionalPrice != StringEmpty &&
      params.price <= params.promotionalPrice
    ) {
      msgError += 'Giá khuyến mãi phải nhỏ hơn giá gốc<br/>';
    }

    if (params.id == 0 && this.listFile.length == 0) {
      msgError += 'Bạn cần phải chọn ít nhất 1 ảnh<br/>';
    }

    if (this.listFile.length > 10) {
      msgError += 'Tối đa 10 ảnh, Dung lượng tối đa 2 Mb 1 ảnh<br/>';
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));

    if (params.id == 0) {
      for (var i = 0; i < this.listFile.length; i++) {
        formData.append('listFile', this.listFile[i]);
      }
    }

    this.roomService.save(formData).subscribe((data) => {
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

  suggestionRoomStatus(): void {
    this.roomStatusService.suggestion().subscribe((data) => {
      if (data.responseCode === 200) {
        this.listRoomStatus = data.data;
      } else {
        this.toastService.error(data.responseMessage);
      }
    });
  }

  suggestionRoomCategories(): void {
    this.roomCategoriesService.suggestion().subscribe((data) => {
      if (data.responseCode === 200) {
        this.listRoomCategories = data.data;
      } else {
        this.toastService.error(data.responseMessage);
      }
    });
  }

  suggestionFloor(): void {
    this.floorsService.suggestion().subscribe((data) => {
      if (data.responseCode === 200) {
        this.listFloor = data.data;
      } else {
        this.toastService.error(data.responseMessage);
      }
    });
  }

  onFileChange(event: any) {
    this.listFile = [];
    for (var i = 0; i < event.target.files.length; i++) {
      const typeFile = event.target.files[0]
        ? event.target.files[0].name.split('.').pop()
        : StringEmpty;

      if (
        typeFile.toLowerCase() !== PNG.toLowerCase() &&
        typeFile.toLowerCase() !== JPG.toLowerCase() &&
        typeFile.toLowerCase() !== JPEG.toLowerCase()
      ) {
        this.myFilesVariable.nativeElement.value = StringEmpty;
        this.toastService.error('Vui lòng chọn File có định dạng ảnh');
        return;
      } else {
        let data = event.target.files[i];
        //check file have size < 2mb
        if (data.size <= 2097152) {
          this.listFile.push(data);
        } else {
          this.toastService.error(
            'Chú ý: Có 1 ảnh vượt quá 2Mb sẽ không được tải lên'
          );
        }
      }
    }

    if (this.listFile.length <= 0) {
      this.myFilesVariable.nativeElement.value = StringEmpty;
    }
  }
}
