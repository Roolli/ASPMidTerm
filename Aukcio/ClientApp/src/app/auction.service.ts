import {Inject, Injectable} from '@angular/core';
import {TokenDecoderService} from './token-decoder.service';
import {HttpClient} from '@angular/common/http';
import {JwtToken} from './jwt-token';
import {Auction} from './auction';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuctionService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  getAll(): Observable<Auction[]> {
    return this.http.get<Auction[]>(this.baseUrl + 'Auction');
  }

  getOne(id: string): Observable<Auction> {
    return this.http.get<Auction>(this.baseUrl + 'Auction/' + id);
  }

  update(auction: Auction): Observable<any> {
    return this.http.put(this.baseUrl + 'Auction/' + auction.uId, auction);
  }

  create(auction: Auction): Observable<any> {
    return this.http.post(this.baseUrl + 'Auction', auction);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + 'Auction/' + id);
  }

  placeBid(newBid: number, uid: string) {
    console.log(this.baseUrl + 'Auction/bid/' + uid);
    return this.http.put(this.baseUrl + 'Auction/bid/' + uid, {newbid: newBid});
  }

  buyout(uid: string): Observable<any> {
    return this.http.delete(this.baseUrl + 'Auction/bid/' + uid);
  }
}
