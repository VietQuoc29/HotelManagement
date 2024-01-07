import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderRoomModalComponent } from './order-room-modal.component';

describe('OrderRoomModalComponent', () => {
  let component: OrderRoomModalComponent;
  let fixture: ComponentFixture<OrderRoomModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderRoomModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderRoomModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
