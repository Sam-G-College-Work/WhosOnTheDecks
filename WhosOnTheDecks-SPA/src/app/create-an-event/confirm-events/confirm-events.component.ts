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
  djId: number;
  promoterId: number;
  eventToCreate: CreateEvent;
  paymentAmount = 0;

  constructor(
    private createEventService: CreateEventService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.eventToCreate = new CreateEvent();
    this.promoterId = this.authService.decodedToken.nameid;
    this.djId = this.activatedRoute.snapshot.params["id"];
    this.createEventObject();
    this.addEventAndDisplay();
  }

  createEventObject() {
    this.eventToCreate.dateTimeOfEvent = new Date(
      this.activatedRoute.snapshot.params["dateTimeOfEvent"]
    );
    this.eventToCreate.lengthOfEvent = +this.activatedRoute.snapshot.params[
      "lengthOfEvent"
    ];
    this.eventToCreate.eventAddress = this.activatedRoute.snapshot.params[
      "eventAddress"
    ];
    this.eventToCreate.postcode = this.activatedRoute.snapshot.params[
      "postcode"
    ];
  }

  addEventAndDisplay() {
    this.createEventService
      .createEvent(this.promoterId, this.djId, this.eventToCreate)
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
    this.createEventService.cancelOrders(this.authService.decodedToken.nameId);
    //this.router.navigate([""]);
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
