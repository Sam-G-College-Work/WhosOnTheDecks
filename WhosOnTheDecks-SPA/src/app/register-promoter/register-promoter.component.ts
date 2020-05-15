import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { AuthService } from "../_service/auth.service";
import { AlertifyService } from "../_service/alertly.service";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-register-promoter",
  templateUrl: "./register-promoter.component.html",
  styleUrls: ["./register-promoter.component.css"],
})
export class RegisterPromoterComponent implements OnInit {
  // An output of event emmiter is used between parent and child components home and promoter register
  @Output() cancelRegister = new EventEmitter();

  // a model is created of type any this will allow json data to be sent to the back end for registration
  model: any = {};

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {}

  // Register makes a call to c=auth service
  // The method register promoter is called and supplied the model from above
  // If successful the method is sunscribed to and the cancel register is turned to false so registration closes
  register() {
    this.authService.registerPromoter(this.model).subscribe(
      () => {
        this.cancelRegister.emit(false);
        this.alertify.success("registration successful");
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  // Cancel will change the event emmiter to false and close the registration page
  cancel() {
    this.cancelRegister.emit(false);
    this.alertify.message("cancelled");
  }
}
