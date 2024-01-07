import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderRoomServiceComponent } from './order-room-service.component';

describe('OrderRoomServiceComponent', () => {
  let component: OrderRoomServiceComponent;
  let fixture: ComponentFixture<OrderRoomServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderRoomServiceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderRoomServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
