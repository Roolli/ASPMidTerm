export class JwtToken {
  sub: string;
  jti: string;
  'id': string; // id
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
  exp: number;
  iss: string;
  aud: string;
}
