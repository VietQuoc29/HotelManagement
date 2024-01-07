import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceCategoriesModalComponent } from './service-categories-modal.component';

describe('ServiceCategoriesModalComponent', () => {
  let component: ServiceCategoriesModalComponent;
  let fixture: ComponentFixture<ServiceCategoriesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceCategoriesModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceCategoriesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
