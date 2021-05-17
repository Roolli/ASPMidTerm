import {Injectable} from '@angular/core';
import {JwtToken} from './jwt-token';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class TokenDecoderService {

  constructor() {
  }

  decodeToken(): JwtToken {
    return jwtDecode<JwtToken>(sessionStorage.getItem('token'));
  }
}
