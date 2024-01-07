import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewRegisterRoomModalComponent } from './view-register-room-modal.component';

describe('ViewRegisterRoomModalComponent', () => {
  let component: ViewRegisterRoomModalComponent;
  let fixture: ComponentFixture<ViewRegisterRoomModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewRegisterRoomModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewRegisterRoomModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
