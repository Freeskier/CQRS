/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PhotoLikeService } from './photoLike.service';

describe('Service: PhotoLike', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PhotoLikeService]
    });
  });

  it('should ...', inject([PhotoLikeService], (service: PhotoLikeService) => {
    expect(service).toBeTruthy();
  }));
});
