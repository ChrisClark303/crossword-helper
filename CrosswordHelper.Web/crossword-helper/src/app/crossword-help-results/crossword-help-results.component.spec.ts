import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrosswordHelpResultsComponent } from './crossword-help-results.component';

describe('CrosswordHelpResultsComponent', () => {
  let component: CrosswordHelpResultsComponent;
  let fixture: ComponentFixture<CrosswordHelpResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CrosswordHelpResultsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrosswordHelpResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
