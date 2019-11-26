import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailebookComponent } from './detailebook.component';

describe('DetailebookComponent', () => {
  let component: DetailebookComponent;
  let fixture: ComponentFixture<DetailebookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailebookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailebookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
