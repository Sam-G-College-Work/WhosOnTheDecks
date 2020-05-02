import { Component, OnInit } from "@angular/core";
import { Booking } from "../_models/booking";
import { AlertifyService } from "../_service/alertly.service";
import { AuthService } from "../_service/auth.service";
import { MatCardModule } from "@angular/material";
import { DjService } from "../_service/dj.service";

@Component({
  selector: "app-view-bookings",
  templateUrl: "./view-bookings.component.html",
  styleUrls: ["./view-bookings.component.css"],
})
export class ViewBookingsComponent implements OnInit {
  djEvents: Event[];

  constructor(
    private djService: DjService,
    private alertify: AlertifyService,
    private authService: AuthService,
    matCardModule: MatCardModule
  ) {}

  ngOnInit() {
    this.loadDjEvents();
  }

  loadDjEvents() {
    this.djService.getDjEvents(this.authService.decodedToken.nameid).subscribe(
      (djEvents: Event[]) => {
        this.djEvents = djEvents;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
