import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomCategoriesModalComponent } from './room-categories-modal.component';

describe('RoomCategoriesModalComponent', () => {
  let component: RoomCategoriesModalComponent;
  let fixture: ComponentFixture<RoomCategoriesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoomCategoriesModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoomCategoriesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
