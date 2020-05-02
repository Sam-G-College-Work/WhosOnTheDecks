import { Component, OnInit } from "@angular/core";
import { EventService } from "../_service/event.service";
import { AlertifyService } from "../_service/alertly.service";
import { MatCardModule } from "@angular/material";
import { AuthService } from "../_service/auth.service";
import { EventDisplay } from "../_models/event-display";

@Component({
  selector: "app-view-events",
  templateUrl: "./view-events.component.html",
  styleUrls: ["./view-events.component.css"],
})
export class ViewEventsComponent implements OnInit {
  eventDisplays: EventDisplay[];

  constructor(
    private eventService: EventService,
    private alertify: AlertifyService,
    private authService: AuthService,
    matCardModule: MatCardModule
  ) {}

  ngOnInit() {
    this.loadEvents();
  }

  loadEvents() {
    this.eventService.getEvents(this.authService.decodedToken.nameid).subscribe(
      (eventDisplays: EventDisplay[]) => {
        this.eventDisplays = eventDisplays;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
