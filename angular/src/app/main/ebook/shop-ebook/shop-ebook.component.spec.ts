import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopEbookComponent } from './shop-ebook.component';

describe('ShopEbookComponent', () => {
  let component: ShopEbookComponent;
  let fixture: ComponentFixture<ShopEbookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShopEbookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShopEbookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
