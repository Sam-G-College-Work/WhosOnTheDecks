import { Component, OnInit } from "@angular/core";
import { Booking } from "../_models/booking";
import { AlertifyService } from "../_service/alertly.service";
import { AuthService } from "../_service/auth.service";
import { MatCardModule } from "@angular/material";
import { DjService } from "../_service/dj.service";
import { EventDisplay } from "../_models/event-display";

@Component({
  selector: "app-view-bookings",
  templateUrl: "./view-bookings.component.html",
  styleUrls: ["./view-bookings.component.css"],
})
export class ViewBookingsComponent implements OnInit {
  djEvents: EventDisplay[];

  constructor(
    private djService: DjService,
    private alertify: AlertifyService,
    private authService: AuthService,
    matCardModule: MatCardModule
  ) {}

  ngOnInit() {
    this.loadDjEvents();
  }

  // Load Dj Events method takes the signed in users Id and sends it tot he promoter sevice method get promoter events
  // The method then subsribes tot he observable return that is provided from the back end method
  // If successful all events are added to the events display array
  // If an exception is hit an error will notify the user an error occured
  loadDjEvents() {
    this.djService.getDjEvents(this.authService.decodedToken.nameid).subscribe(
      (djEvents: EventDisplay[]) => {
        this.djEvents = djEvents;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
