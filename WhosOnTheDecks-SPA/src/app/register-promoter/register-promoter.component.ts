import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { AuthService } from "../_service/auth.service";
import { AlertifyService } from "../_service/alertly.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-register-promoter",
  templateUrl: "./register-promoter.component.html",
  styleUrls: ["./register-promoter.component.css"],
})
export class RegisterPromoterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {}

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

  cancel() {
    this.cancelRegister.emit(false);
    this.alertify.message("cancelled");
  }
}
