import { Component, OnInit } from "@angular/core";
import { AuthService } from "../_service/auth.service";
import { AlertifyService } from "../_service/alertly.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"],
})
export class NavComponent implements OnInit {
  model: any = {};
  isPromoter = false;
  isDj = false;

  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {
    this.isPromoterCheck();
    this.isDjCheck();
  }

  login() {
    this.authService.login(this.model).subscribe(
      (next) => {
        this.alertify.success("Logged in Successfully");
        this.isPromoterCheck();
        this.isDjCheck();
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(["/view-all-djs"]);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    this.isPromoter = false;
    this.isDj = false;
    localStorage.removeItem("token");
    this.alertify.message("Logged Out");
    this.router.navigate(["/home"]);
  }

  isPromoterCheck() {
    if (this.authService.decodedToken.role === "Promoter") {
      this.isPromoter = true;
    }
  }

  isDjCheck() {
    if (this.authService.decodedToken.role === "Dj") {
      this.isDj = true;
    }
  }
}
