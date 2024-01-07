import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { StringEmpty } from '../constants/constants';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent implements OnInit {
  isLoading = false;
  signinForm!: FormGroup;
  returnUrl!: string;
  error!: string;

  constructor(
    private router: Router,
    private authentService: AuthenticationService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private toastService: ToastrService
  ) {
    if (this.authentService.userValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit(): void {
    this.signinForm = this.fb.group({
      userName: [''],
      passWord: [''],
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/admin';
  }

  get f() {
    return this.signinForm.controls;
  }

  submitForm(): void {
    this.isLoading = true;
    let msgError = StringEmpty;

    if (this.f['userName'].value == StringEmpty) {
      msgError = 'Bạn chưa nhập tài khoản<br/>';
    }

    if (this.f['passWord'].value == StringEmpty) {
      msgError += 'Bạn chưa nhập mật khẩu<br/>';
    }

    if (msgError != StringEmpty) {
      this.toastService.error(msgError, StringEmpty, { enableHtml: true });
      this.isLoading = false;
      return;
    }

    this.authentService
      .signIn(this.f['userName'].value, this.f['passWord'].value)
      .pipe(first())
      .subscribe(
        // tslint:disable-next-line:no-unused-expression
        (data: { data: null; message: string | undefined }) => {
          if (data.data == null) {
            this.toastService.error(data.message);
            this.isLoading = false;
          } else {
            // tslint:disable-next-line:no-unused-expression
            this.router.navigate([this.returnUrl]);
            this.isLoading = false;
          }
        },
        (error: string) => {
          this.error = error;
          this.isLoading = false;
        }
      );
  }
}
