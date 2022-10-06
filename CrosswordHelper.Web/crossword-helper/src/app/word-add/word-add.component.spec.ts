import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WordAddComponent } from './word-add.component';

describe('WordAddComponent', () => {
  let component: WordAddComponent;
  let fixture: ComponentFixture<WordAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WordAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WordAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
