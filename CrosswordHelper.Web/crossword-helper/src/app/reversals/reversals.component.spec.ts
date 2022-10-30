import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReversalsComponent } from './reversals.component';

describe('ReversalsComponent', () => {
  let component: ReversalsComponent;
  let fixture: ComponentFixture<ReversalsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReversalsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReversalsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
