/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DialogHelperService } from './dialogHelper.service';

describe('Service: DialogHelper', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DialogHelperService]
    });
  });

  it('should ...', inject([DialogHelperService], (service: DialogHelperService) => {
    expect(service).toBeTruthy();
  }));
});
