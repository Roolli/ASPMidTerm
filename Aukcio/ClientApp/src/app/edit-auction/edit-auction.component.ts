import {Component, Input, OnInit} from '@angular/core';
import {Auction} from '../auction';
import {ActivatedRoute, Router} from '@angular/router';
import {Location} from '@angular/common';
import {AuctionService} from '../auction.service';

@Component({
  selector: 'app-edit-auction',
  templateUrl: './edit-auction.component.html',
  styleUrls: ['./edit-auction.component.css']
})
export class EditAuctionComponent implements OnInit {
  public currentlyEdited: Auction;

  constructor(private router: Router, private route: ActivatedRoute, private location: Location, private auctionService: AuctionService) {
    const id = this.route.snapshot.paramMap.get('id');
    this.currentlyEdited = new Auction();
    if (id) {
      this.auctionService.getOne(id).subscribe(value => {
        this.currentlyEdited = {
          ...value
        };

      });
    }
  }

  submit() {
    if (this.currentlyEdited.uId !== undefined) {
      this.auctionService.update(this.currentlyEdited).subscribe(res => {
        this.router.navigate(['']);
      });
    } else {
      this.auctionService.create(this.currentlyEdited).subscribe(res => {
        this.router.navigate(['']);
      });
    }

  }

  ngOnInit() {
  }

}
