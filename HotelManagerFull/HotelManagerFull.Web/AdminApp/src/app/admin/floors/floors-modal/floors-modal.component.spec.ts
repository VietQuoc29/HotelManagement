import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FloorsModalComponent } from './floors-modal.component';

describe('FloorsModalComponent', () => {
  let component: FloorsModalComponent;
  let fixture: ComponentFixture<FloorsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FloorsModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FloorsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
