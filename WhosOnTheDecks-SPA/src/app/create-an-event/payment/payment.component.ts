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

  // Load Events in basket method takes the signed in users Id and sends it tot he promoter sevice method get promoter events
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

  // Make payment uses payment service to call the method make payment.
  // The method is upplied the logged in users id and the payment object created called card details
  // A message is displayed to say payment was recieved if the return is ok
  // The user is then taken to the confirmation page
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

  // Get total works out the total from the events array to display on the payments page
  get totalPayment() {
    return sumBy(this.eventDisplays, (eventDisplay) => eventDisplay.totalCost);
  }

  // Method used to open date picker to month and year
  openDatePicker(dp) {
    dp.open();
  }

  closeDatePicker(eventData: any, dp?: any) {
    // get month and year from eventData and close datepicker, thus not allowing user to select date
    dp.close();
  }
}
