import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { differenceInCalendarDays } from 'date-fns';
import * as moment from 'moment';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { DATE_TIME_FORMAT, DATE_FORMAT, StringEmpty, MOMENT_DATE_FORMAT } from 'src/app/constants/constants';
import { RoleService } from 'src/app/services/role.service';
import { SexService } from 'src/app/services/sex.service';
import { UserProfileService } from 'src/app/services/user-profile.service';

@Component({
  selector: 'app-user-profile-modal',
  templateUrl: './user-profile-modal.component.html',
  styleUrls: ['./user-profile-modal.component.scss']
})
export class UserProfileModalComponent implements OnInit {
  modalForm!: FormGroup;
  @Input() data: any;
  isLoading = true;
  dateFormat = DATE_TIME_FORMAT;
  dateFormatDateOfBirth = DATE_FORMAT;
  isShowInfo = false;
  listSex: any[] = [];
  listRole: any[] = [];

  constructor(
    private fb: FormBuilder,
    private toastService: ToastrService,
    private userProfileService: UserProfileService,
    private modal: NzModalRef,
    private sexService: SexService,
    private roleService: RoleService) { }

  ngOnInit(): void {
    this.suggestionSex();
    this.suggestionRole();

    this.modalForm = this.fb.group({
      id: [0],
      userName: [StringEmpty],
      fullName: [StringEmpty],
      dateOfBirth: [StringEmpty],
      phoneNumber: [StringEmpty],
      address: [StringEmpty],
      email: [StringEmpty],
      facebook: [StringEmpty],
      zalo: [StringEmpty],
      sexId: [StringEmpty],
      roleId: [StringEmpty],
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

  disabledDate = (current: Date): boolean => {
    // Can not select days before today and today
    return differenceInCalendarDays(current, new Date()) > 0;
    // tslint:disable-next-line:semicolon
  };

  cancelModal(reLoad: any): void {
    this.modal.destroy({ reLoad });
  }

  submitForm(): void {
    this.isLoading = true;
    let msgError = StringEmpty;

    const params = this.modalForm.value;
    params.userName = params.userName || StringEmpty;
    params.fullName = params.fullName || StringEmpty;
    params.dateOfBirth = params.dateOfBirth
      ? moment(params.dateOfBirth).format(MOMENT_DATE_FORMAT)
      : StringEmpty;
    params.phoneNumber = params.phoneNumber || StringEmpty;
    params.address = params.address || StringEmpty;
    params.email = params.email || StringEmpty;
    params.facebook = params.facebook || StringEmpty;
    params.zalo = params.zalo || StringEmpty;
    params.sexId = params.sexId || StringEmpty;
    params.roleId = params.roleId || StringEmpty;

    if (params.userName == StringEmpty) {
      msgError = 'Vui lòng nhập Tên đăng nhập<br/>';
    }

    if (params.fullName == StringEmpty) {
      msgError += 'Vui lòng nhập Tên đầy đủ<br/>';
    }

    if (params.roleId == StringEmpty) {
      msgError += 'Vui lòng chọn Vai trò';
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    const formData: FormData = new FormData();
    Object.keys(params).forEach((key) => formData.append(key, params[key]));
    this.userProfileService.save(formData).subscribe((data) => {
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

  suggestionSex(): void {
    this.sexService.suggestion().subscribe((data) => {
      if (data.responseCode === 200) {
        this.listSex = data.data;
      } else {
        this.toastService.error(data.responseMessage);
      }
    });
  }

  suggestionRole(): void {
    this.roleService.suggestion().subscribe((data) => {
      if (data.responseCode === 200) {
        this.listRole = data.data;
      } else {
        this.toastService.error(data.responseMessage);
      }
    });
  }

}

