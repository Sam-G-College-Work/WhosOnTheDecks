import { Component, OnInit } from "@angular/core";
import { EventDisplay } from "src/app/_models/event-display";
import { CreateEventService } from "src/app/_service/create-event.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { AuthService } from "src/app/_service/auth.service";
import { Router } from "@angular/router";
import { sumBy } from "lodash";

@Component({
  selector: "app-confirm-events",
  templateUrl: "./confirm-events.component.html",
  styleUrls: ["./confirm-events.component.css"],
})
export class ConfirmEventsComponent implements OnInit {
  // Property eventDisplay is created this is an array to hold the events being recieved from the front end
  eventDisplays: EventDisplay[];

  constructor(
    private createEventService: CreateEventService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    // Get events method is called on page load
    this.getEventsInBasket();
  }

  // GetEventsInBasket method takes the signed in users Id and sends it to the promoter sevice method get promoter events
  // The method then subsribes tot he observable return that is provided from the back end method
  // If successful all events are added to the events display array
  // If an exception is hit an error will notify the user an error occured
  getEventsInBasket() {
    this.createEventService
      .getPromoterOrders(this.authService.decodedToken.nameid)
      .subscribe(
        (eventDisplays: EventDisplay[]) => {
          this.eventDisplays = eventDisplays;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  // DeleteAll calls the createeventservice method cancel orders with the logged in users id
  // An observable is subcribed to and on is success a mesage is displayed and the user is taken home
  deleteAll() {
    this.createEventService
      .cancelOrders(this.authService.decodedToken.nameid)
      .subscribe(() => {
        this.alertify.success("All items deleted");
        this.router.navigate([""]);
      });
  }

  // This get is used to calculate the total of the events in eventsdisplay to display the total on the html page
  get totalPayment() {
    return sumBy(this.eventDisplays, (eventDisplay) => eventDisplay.totalCost);
  }
}
