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
  }

  loadBooking() {
    this.djService.getDjBooking(+this.route.snapshot.params["id"]).subscribe(
      (booking: Booking) => {
        if (booking) {
          this.booking = booking;
          this.hasResponded();
        }
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

  bookingResponse(response: string) {
    this.booking.bookingStatus = response;
    this.djService
      .postResponse(+this.route.snapshot.params["id"], this.booking)
      .subscribe(
        (next) => {
          this.alertify.success("Thank you for your response");
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
