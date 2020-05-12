import { Component, OnInit } from "@angular/core";
import { CreateEvent } from "../_models/create-event";
import { Router } from "@angular/router";
import { CreateEventService } from "../_service/create-event.service";
import { AuthService } from "../_service/auth.service";
import { AlertifyService } from "../_service/alertly.service";

@Component({
  selector: "app-create-an-event",
  templateUrl: "./create-an-event.component.html",
  styleUrls: ["./create-an-event.component.css"],
})
export class CreateAnEventComponent implements OnInit {
  evNew: any = {};
  todaysDate = new Date();
  minDate = new Date().setDate(this.todaysDate.getDate() + 1);
  maxDate = new Date(2025, 12, 31);
  minLength = 1;
  maxLength = 8;
  shop: boolean;

  constructor(
    private router: Router,
    private createEventService: CreateEventService,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {}

  // Select dj takes the user to select dj page passing the event model through
  selectADj() {
    this.router.navigate(["/select-a-dj/", this.evNew]);
  }

  // Cancel mkaes a check to see if th euser has any bookings in their basket
  // if so it will take them to confirm events page
  // if not it will take them home
  cancel() {
    this.createEventService
      .shoppingExists(this.authService.decodedToken.nameid)
      .subscribe(
        (answer) => {
          if (answer) {
            this.router.navigate(["/confirm-events"]);
            this.alertify.error("There's bookings in your basket");
          } else {
            this.router.navigate([""]);
          }
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
