import { Component, OnInit } from "@angular/core";
import { DjService } from "src/app/_service/dj.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { ActivatedRoute } from "@angular/router";
import { Booking } from "src/app/_models/booking";

@Component({
  selector: "app-booking-response",
  templateUrl: "./booking-response.component.html",
  styleUrls: ["./booking-response.component.css"],
})
export class BookingResponseComponent implements OnInit {
  booking: Booking;

  constructor(
    private djService: DjService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadBooking();
    this.hasResponded();
  }

  loadBooking() {
    this.djService.getDjBooking(+this.route.snapshot.params["id"]).subscribe(
      (booking: Booking) => {
        this.booking = booking;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  hasResponded() {
    if (
      this.booking.bookingStatus === "Accepted" ||
      this.booking.bookingStatus === "Declined"
    ) {
      return true;
    }
  }

  bookingResponse(response) {}
}
