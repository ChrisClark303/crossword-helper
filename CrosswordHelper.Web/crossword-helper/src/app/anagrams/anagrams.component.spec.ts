import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnagramsComponent } from './anagrams.component';

describe('AnagramsComponent', () => {
  let component: AnagramsComponent;
  let fixture: ComponentFixture<AnagramsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AnagramsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnagramsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
