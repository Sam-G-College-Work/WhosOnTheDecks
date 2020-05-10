import { Component, OnInit } from "@angular/core";
import { Dj } from "src/app/_models/dj";
import { CreateEventService } from "src/app/_service/create-event.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { AuthService } from "src/app/_service/auth.service";
import { ActivatedRoute, Router } from "@angular/router";
import { uniq } from "lodash";
import { CreateEvent } from "src/app/_models/create-event";
import { DatePipe } from "@angular/common";

@Component({
  selector: "app-select-a-dj",
  templateUrl: "./select-a-dj.component.html",
  styleUrls: ["./select-a-dj.component.css"],
})
export class SelectADjComponent implements OnInit {
  eventToCreate: CreateEvent;
  djs: Dj[];
  genres: string[];
  selected: string;

  constructor(
    private createEvent: CreateEventService,
    private alertify: AlertifyService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.eventToCreate = new CreateEvent();
    this.createEventObject();
    this.avaliableDjs();
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

  avaliableDjs() {
    this.createEvent.getAvaliableDjs(this.eventToCreate).subscribe(
      (djs: Dj[]) => {
        this.djs = djs;
        this.genres = uniq(this.djs.map((dj) => dj.genre));
        this.genres.push("Any");
        this.genres.reverse();
        this.selected = "Any";
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  confirmEvent(djId) {
    this.router.navigate(["/confirm-events/" + djId, this.eventToCreate]);
  }
}
