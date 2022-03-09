import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCustomerPageComponent } from './new-customer-page.component';

describe('NewCustomerPageComponent', () => {
  let component: NewCustomerPageComponent;
  let fixture: ComponentFixture<NewCustomerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewCustomerPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCustomerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
