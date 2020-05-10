import { Component, OnInit } from "@angular/core";
import { EventDisplay } from "src/app/_models/event-display";
import { CreateEventService } from "src/app/_service/create-event.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { AuthService } from "src/app/_service/auth.service";
import { CreateEvent } from "src/app/_models/create-event";
import { ActivatedRoute } from "@angular/router";

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

  constructor(
    private createEventService: CreateEventService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute
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
}
