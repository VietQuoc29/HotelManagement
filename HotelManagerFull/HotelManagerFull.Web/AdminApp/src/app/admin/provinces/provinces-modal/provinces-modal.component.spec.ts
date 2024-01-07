import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProvincesModalComponent } from './provinces-modal.component';

describe('ProvincesModalComponent', () => {
  let component: ProvincesModalComponent;
  let fixture: ComponentFixture<ProvincesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProvincesModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProvincesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
