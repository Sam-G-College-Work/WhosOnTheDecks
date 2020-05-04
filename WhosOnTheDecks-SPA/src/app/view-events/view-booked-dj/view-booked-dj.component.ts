import { Component, OnInit } from "@angular/core";
import { PromoterService } from "src/app/_service/promoter.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { AuthService } from "src/app/_service/auth.service";
import { MatCardModule } from "@angular/material";
import { ActivatedRoute } from "@angular/router";
import { Dj } from "src/app/_models/dj";
import { Booking } from "src/app/_models/booking";

@Component({
  selector: "app-view-booked-dj",
  templateUrl: "./view-booked-dj.component.html",
  styleUrls: ["./view-booked-dj.component.css"],
})
export class ViewBookedDjComponent implements OnInit {
  dj: Dj;
  booking: Booking;

  constructor(
    private route: ActivatedRoute,
    private promoterService: PromoterService,
    private alertify: AlertifyService,
    private authService: AuthService,
    matCardModule: MatCardModule
  ) {}

  ngOnInit() {
    this.loadDj();
    this.loadBooking();
  }

  loadDj() {
    this.promoterService.getDj(+this.route.snapshot.params["id"]).subscribe(
      (dj: Dj) => {
        this.dj = dj;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  loadBooking() {
    this.promoterService
      .getBooking(+this.route.snapshot.params["id"])
      .subscribe(
        (booking: Booking) => {
          this.booking = booking;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
