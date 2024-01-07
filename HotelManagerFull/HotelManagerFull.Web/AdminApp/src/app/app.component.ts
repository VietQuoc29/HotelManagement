import { Component } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Quản Lý Khách Sạn';

  constructor(private auth: AuthenticationService) { }

  ngOnInit(): void {
    this.auth.autoLogin();
  }
}
