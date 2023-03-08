import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LetterSelectionComponent } from './letter-selection.component';

describe('LetterSelectionComponent', () => {
  let component: LetterSelectionComponent;
  let fixture: ComponentFixture<LetterSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LetterSelectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LetterSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
