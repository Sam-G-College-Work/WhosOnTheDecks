import { Component, OnInit } from "@angular/core";
import { Booking } from "../_models/booking";
import { BookingService } from "../_service/booking.service";
import { AlertifyService } from "../_service/alertly.service";
import { AuthService } from "../_service/auth.service";
import { MatCardModule } from "@angular/material";

@Component({
  selector: "app-view-bookings",
  templateUrl: "./view-bookings.component.html",
  styleUrls: ["./view-bookings.component.css"],
})
export class ViewBookingsComponent implements OnInit {
  bookings: Booking[];

  constructor(
    private bookingService: BookingService,
    private alertify: AlertifyService,
    private authService: AuthService,
    matCardModule: MatCardModule
  ) {}

  ngOnInit() {
    this.loadBookings();
  }

  loadBookings() {
    this.bookingService
      .getDjBookings(this.authService.decodedToken.nameid)
      .subscribe(
        (bookings: Booking[]) => {
          this.bookings = bookings;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
