import { Component, OnInit } from "@angular/core";
import { CreateEventService } from "src/app/_service/create-event.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { AuthService } from "src/app/_service/auth.service";
import { Router } from "@angular/router";
import { PaymentService } from "src/app/_service/payment.service";
import { EventDisplay } from "src/app/_models/event-display";
import { sumBy } from "lodash";
import { CardDetails } from "src/app/_models/card-details";

@Component({
  selector: "app-payment",
  templateUrl: "./payment.component.html",
  styleUrls: ["./payment.component.css"],
})
export class PaymentComponent implements OnInit {
  eventDisplays: EventDisplay[];
  cardDetails: CardDetails;
  todaysDate = new Date();
  minDate = new Date().setDate(this.todaysDate.getMonth());
  maxDate = new Date().setDate(this.todaysDate.getMonth() + 6);

  constructor(
    private createEventService: CreateEventService,
    private paymentService: PaymentService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.cardDetails = new CardDetails();
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

  makePayment() {
    this.paymentService
      .makePayment(this.authService.decodedToken.nameid, this.cardDetails)
      .subscribe(
        () => {
          this.alertify.success("Payment Recieved");
          this.router.navigate(["confirmation"]);
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  get totalPayment() {
    return sumBy(this.eventDisplays, (eventDisplay) => eventDisplay.totalCost);
  }

  openDatePicker(dp) {
    dp.open();
  }

  closeDatePicker(eventData: any, dp?: any) {
    // get month and year from eventData and close datepicker, thus not allowing user to select date
    dp.close();
  }
}
