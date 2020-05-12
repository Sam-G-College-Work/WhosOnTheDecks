import { Component, OnInit } from "@angular/core";
import { PromoterService } from "src/app/_service/promoter.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { ActivatedRoute } from "@angular/router";
import { Dj } from "src/app/_models/dj";
import { Booking } from "src/app/_models/booking";

@Component({
  selector: "app-view-booked-dj",
  templateUrl: "./view-booked-dj.component.html",
  styleUrls: ["./view-booked-dj.component.css"],
})
export class ViewBookedDjComponent implements OnInit {
  // A Dj Object is declared
  dj: Dj;

  // A Booking object is declared
  booking: Booking;

  constructor(
    private route: ActivatedRoute,
    private promoterService: PromoterService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    // Load Djs is called on page load
    this.loadDj();

    // Load booking is called on page load
    this.loadBooking();
  }

  // Load Dj makes a call to the promoters service page
  // The method get Dj is called and the signed in users Id is supplied
  // The return is then subscribed to and the dj is assigned to the dj object
  // If an exception is hit an eror message will display
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

  // Load Booking makes a call to the promoters service page
  // The method get Booking is called and a snapshot of the event id from the selected event is passed in
  // The return is then subscribed to and the booking is assigned to the booking object
  // If an exception is hit an eror message will display
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
