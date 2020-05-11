import { Component, OnInit } from "@angular/core";
import { EventDisplay } from "src/app/_models/event-display";
import { CreateEventService } from "src/app/_service/create-event.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { AuthService } from "src/app/_service/auth.service";
import { CreateEvent } from "src/app/_models/create-event";
import { ActivatedRoute, Router } from "@angular/router";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";

@Component({
  selector: "app-confirm-events",
  templateUrl: "./confirm-events.component.html",
  styleUrls: ["./confirm-events.component.css"],
})
export class ConfirmEventsComponent implements OnInit {
  eventDisplays: EventDisplay[];
  paymentAmount = 0;

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
      .subscribe((val) => console.log("deleted"));
    this.router.navigate([""]);
  }

  // totalPayment() {
  //   this.createEventService
  //     .getTotal(this.authService.decodedToken.nameId)
  //     .subscribe(
  //       (total: number) => {
  //         this.paymentAmount = total;
  //       },
  //       (error) => {
  //         this.alertify.error(error);
  //       }
  //     );
  // }
}
