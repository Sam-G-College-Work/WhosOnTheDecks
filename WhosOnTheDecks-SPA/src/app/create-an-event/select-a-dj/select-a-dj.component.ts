import { Component, OnInit } from "@angular/core";
import { Dj } from "src/app/_models/dj";
import { CreateEventService } from "src/app/_service/create-event.service";
import { AlertifyService } from "src/app/_service/alertly.service";
import { AuthService } from "src/app/_service/auth.service";
import { ActivatedRoute, Router } from "@angular/router";
import { uniq } from "lodash";
import { CreateEvent } from "src/app/_models/create-event";
import { DatePipe } from "@angular/common";
import { MatSelectChange } from "@angular/material";

@Component({
  selector: "app-select-a-dj",
  templateUrl: "./select-a-dj.component.html",
  styleUrls: ["./select-a-dj.component.css"],
})
export class SelectADjComponent implements OnInit {
  eventToCreate: CreateEvent;
  djs: Dj[];
  djsToDisplay: Dj[];
  genres: string[];
  selected: string;
  promoterId: number;

  constructor(
    private createEventService: CreateEventService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.promoterId = this.authService.decodedToken.nameid;
    this.eventToCreate = new CreateEvent();
    this.createEventObject();
    this.avaliableDjs();
  }

  // create Event takes the passed parameters and assings them to an event object
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

  // Avliable djs uses create event service to call the method get avaliable djs
  // the method is passed the event to create and using its date
  // is returned the djs avliable on that date
  // The genres from each dj are then put into the genres array and all duplicate entries are removed
  avaliableDjs() {
    this.createEventService.getAvaliableDjs(this.eventToCreate).subscribe(
      (djs: Dj[]) => {
        this.djs = djs;
        this.djsToDisplay = this.djs;
        this.genres = uniq(this.djs.map((dj) => dj.genre)).sort();
        this.genres = ["Any", ...this.genres];
        this.selected = "Any";
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  // AddEvent takes the singed in users id the dj they have selected and the event they have created
  // This is passed using create event service with the method createEvent
  // Upon the mthods success the user is then taken to the confirm events page
  addEvent(djId) {
    this.createEventService
      .createEvent(this.promoterId, djId, this.eventToCreate)
      .subscribe(
        (sucess) => {
          this.router.navigate(["/confirm-events/"]);
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  // Genre changed takes in the MatSelectChange event on the html page
  // The selected genre then filters the djsdisplay list into a new list containing only the djs with the selected genre
  genreChanged(eventChange: MatSelectChange) {
    const newGenre = eventChange.value;

    if (newGenre === "Any") {
      this.djsToDisplay = this.djs;
    } else {
      this.djsToDisplay = this.djs.filter((dj) => dj.genre === newGenre);
    }
  }
}
