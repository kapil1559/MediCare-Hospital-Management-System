import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicList } from './dynamic-list';

describe('DynamicList', () => {
  let component: DynamicList;
  let fixture: ComponentFixture<DynamicList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DynamicList],
    }).compileComponents();

    fixture = TestBed.createComponent(DynamicList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
