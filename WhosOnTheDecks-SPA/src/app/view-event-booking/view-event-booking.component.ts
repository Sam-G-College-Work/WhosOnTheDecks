import { Component, OnInit } from "@angular/core";
import { AlertifyService } from "../_service/alertly.service";
import { ActivatedRoute } from "@angular/router";
import { BookingService } from "../_service/booking.service";
import { Booking } from "../_models/booking";
import { Dj } from "../_models/dj";
import { DjService } from "../_service/dj.service";
import { tap } from "rxjs/operators";

@Component({
  selector: "app-view-event-booking",
  templateUrl: "./view-event-booking.component.html",
  styleUrls: ["./view-event-booking.component.css"],
})
export class ViewEventBookingComponent implements OnInit {
  booking: Booking;
  dj: Dj;

  constructor(
    private bookingService: BookingService,
    private djService: DjService,
    private alertify: AlertifyService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    // this.loadBooking();
  }

  // loadBooking() {
  //   this.bookingService
  //     .getEventBooking(Number(this.activatedRoute.snapshot.params["id"]))
  //     .subscribe(
  //       (booking: Booking) => {
  //         tap((booking) => console.log(booking));
  //         this.booking = booking;
  //       },
  //       (error) => {
  //         this.alertify.error(error);
  //       }
  //     );

  //   this.loadDj(this.booking.djId);
  // }

  // loadDj(id) {
  //   this.djService.getDj(id).subscribe(
  //     (dj: Dj) => {
  //       this.dj = dj;
  //     },
  //     (error) => {
  //       this.alertify.error(error);
  //     }
  //   );
  // }
}
