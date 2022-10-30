import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemovalsComponent } from './removals.component';

describe('RemovalsComponent', () => {
  let component: RemovalsComponent;
  let fixture: ComponentFixture<RemovalsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemovalsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RemovalsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
