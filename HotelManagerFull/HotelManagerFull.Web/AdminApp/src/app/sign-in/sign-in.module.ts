import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { SignInRoutingModule } from './sign-in-routing.module';
import { SignInComponent } from './sign-in.component';
import { NzSpinModule } from 'ng-zorro-antd/spin';

@NgModule({
    declarations: [SignInComponent],
    imports: [
        CommonModule,
        SignInRoutingModule,
        ReactiveFormsModule,
        NzSpinModule,
    ],
})
export class SignInModule { }
