import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './user-profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserProfileRoutingModule } from './user-profile-routing.module';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { UserProfileModalComponent } from './user-profile-modal/user-profile-modal.component';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSwitchModule } from 'ng-zorro-antd/switch';



@NgModule({
  declarations: [UserProfileComponent, UserProfileModalComponent],
  imports: [
    CommonModule,
    UserProfileRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NzTableModule,
    NzFormModule,
    NzSpinModule,
    NzPopconfirmModule,
    NzDatePickerModule,
    NzSwitchModule,
    NzSelectModule
  ]
})
export class UserProfileModule { }
