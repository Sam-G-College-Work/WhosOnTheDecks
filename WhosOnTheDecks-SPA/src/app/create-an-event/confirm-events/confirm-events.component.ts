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
  eventDisplays: EventDisplay[];

  constructor(
    private createEventService: CreateEventService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.getEventsInBasket();
  }

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

  deleteAll() {
    this.createEventService
      .cancelOrders(this.authService.decodedToken.nameid)
      .subscribe(() => {
      this.alertify.success("All items deleted");
      this.router.navigate([""]);
      });
  }

  get totalPayment() {
    return sumBy(this.eventDisplays, eventDisplay => eventDisplay.totalCost);
  }
}
