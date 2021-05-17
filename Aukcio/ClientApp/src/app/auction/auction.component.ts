import {Component, Inject, OnInit} from '@angular/core';
import {Auction} from '../auction';
import {AuctionService} from '../auction.service';
import {JwtToken} from '../jwt-token';
import {TokenDecoderService} from '../token-decoder.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css']
})
export class AuctionComponent implements OnInit {

  public auctions: Auction[];

  public decoded_token: JwtToken;

  constructor(private tokenDecoder: TokenDecoderService, private auctionService: AuctionService, private router: Router) {
    this.decoded_token = tokenDecoder.decodeToken();
  }

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.auctionService.getAll().subscribe(val => this.auctions = val);
  }

  editAuction(id: string) {
    this.router.navigate(['edit/', id]);
  }

  deleteAuction(id: string) {
    this.auctionService.delete(id).subscribe(val => {
      this.getAll();
    });
  }

  placeBid(newBid: string, uid: string) {
    this.auctionService.placeBid(parseInt(newBid, 10), uid).subscribe(val => {
      this.getAll();
    });
  }

  buyout(uid: string) {
    this.auctionService.buyout(uid).subscribe(val => {
      this.getAll();
    });
  }
}
