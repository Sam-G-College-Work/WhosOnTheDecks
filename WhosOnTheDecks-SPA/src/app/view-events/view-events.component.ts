import { Component, OnInit } from "@angular/core";
import { AlertifyService } from "../_service/alertly.service";
import { AuthService } from "../_service/auth.service";
import { EventDisplay } from "../_models/event-display";
import { PromoterService } from "../_service/promoter.service";

@Component({
  selector: "app-view-events",
  templateUrl: "./view-events.component.html",
  styleUrls: ["./view-events.component.css"],
})
export class ViewEventsComponent implements OnInit {
  // Property eventDisplay is created this is an array to hold the events being recieved from the front end
  eventDisplays: EventDisplay[];

  constructor(
    private promoterService: PromoterService,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    // Calls loadEvents method on page load
    this.loadEvents();
  }

  // Load Events method takes the signed in users Id and sends it tot he promoter sevice method get promoter events
  // The method then subsribes tot he observable return that is provided from the back end method
  // If successful all events are added to the events display array
  // If an exception is hit an error will notify the user an error occured
  loadEvents() {
    this.promoterService
      .getPromoterEvents(this.authService.decodedToken.nameid)
      .subscribe(
        (eventDisplays: EventDisplay[]) => {
          this.eventDisplays = eventDisplays;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
