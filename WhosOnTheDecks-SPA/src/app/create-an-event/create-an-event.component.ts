import { Component, OnInit } from "@angular/core";
import { CreateEvent } from "../_models/create-event";
import { Router } from "@angular/router";
import { CreateEventService } from "../_service/create-event.service";
import { AuthService } from "../_service/auth.service";
import { AlertifyService } from "../_service/alertly.service";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-create-an-event",
  templateUrl: "./create-an-event.component.html",
  styleUrls: ["./create-an-event.component.css"],
})
export class CreateAnEventComponent implements OnInit {
  evNew: any = {};
  todaysDate = new Date();
  minLength = 1;
  maxLength = 8;
  shop: boolean;

  lengthPattern = "^[1-9]d*$";

  // PostcodePatter will format postcode patterns to a specific format
  postcodePattern =
    "^(([gG][iI][rR] {0,}0[aA]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))$";

  // isFormValid is a boolean to check all formats are correct
  isValidFormSubmitted = false;

  constructor(
    private router: Router,
    private createEventService: CreateEventService,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {}

  // onformSubmit will check all fields have been enetered correctly and if not will not set the form to isValid
  onFormSubmit(form: NgForm) {
    this.isValidFormSubmitted = false;
    if (form.invalid) {
      return;
    }
    this.isValidFormSubmitted = true;
    this.selectADj();
  }

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
