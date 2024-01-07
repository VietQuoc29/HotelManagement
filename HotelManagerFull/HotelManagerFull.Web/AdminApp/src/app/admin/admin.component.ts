import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent implements OnInit {
  fullName: string | undefined;
  username: string | undefined;
  roleName: string | undefined;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    this.fullName = this.authenticationService.userValue?.fullName;
    this.username = this.authenticationService.userValue?.username;
    this.roleName = this.authenticationService.userValue?.roleName;
    this.authenticationService.autoLogin();
  }

  logOut(): void {
    this.authenticationService.logout();
  }
}
