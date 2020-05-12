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
  // Booking object is declared
  booking: Booking;

  constructor(
    private djService: DjService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    // Load Booking is called on page load
    this.loadBooking();
  }

  // Load Booking makes a call to the promoters service page
  // The method get Booking is called and a snapshot of the event id from the selected event is passed in
  // The return is then subscribed to and the booking is assigned to the booking object
  // If an exception is hit an eror message will display
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

  // Has responded checks to see if the Dj has accepted or declined the booking offer
  // If so it returns true. This is used to display the accept decline buttons on the html page
  // If the method returns true the dj has responded and the buttons will not display
  hasResponded() {
    if (
      this.booking.bookingStatus === "Accepted" ||
      this.booking.bookingStatus === "Declined"
    ) {
      return true;
    }
  }

  // Booking response makes a call to dj service and
  // the method postResponse is given the snapshot fo the event selected from the last page
  // and the booking obejct with the new response
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
