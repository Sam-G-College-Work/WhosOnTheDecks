import { Component, OnInit } from "@angular/core";
import { AlertifyService } from "../_service/alertly.service";
import { MatCardModule } from "@angular/material";
import { AuthService } from "../_service/auth.service";
import { Event } from "../_models/event";
import { PromoterService } from "../_service/promoter.service";

@Component({
  selector: "app-view-events",
  templateUrl: "./view-events.component.html",
  styleUrls: ["./view-events.component.css"],
})
export class ViewEventsComponent implements OnInit {
  eventDisplays: Event[];

  constructor(
    private promoterService: PromoterService,
    private alertify: AlertifyService,
    private authService: AuthService,
    matCardModule: MatCardModule
  ) {}

  ngOnInit() {
    this.loadEvents();
  }

  loadEvents() {
    this.promoterService
      .getPromoterEvents(this.authService.decodedToken.nameid)
      .subscribe(
        (eventDisplays: Event[]) => {
          this.eventDisplays = eventDisplays;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
