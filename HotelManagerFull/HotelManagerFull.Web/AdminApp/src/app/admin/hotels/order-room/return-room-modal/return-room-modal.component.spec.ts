import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReturnRoomModalComponent } from './return-room-modal.component';

describe('ReturnRoomModalComponent', () => {
  let component: ReturnRoomModalComponent;
  let fixture: ComponentFixture<ReturnRoomModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReturnRoomModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReturnRoomModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
