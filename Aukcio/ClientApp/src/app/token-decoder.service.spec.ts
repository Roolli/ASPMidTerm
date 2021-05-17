import { TestBed } from '@angular/core/testing';

import { TokenDecoderService } from './token-decoder.service';

describe('TokenDecoderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TokenDecoderService = TestBed.get(TokenDecoderService);
    expect(service).toBeTruthy();
  });
});
