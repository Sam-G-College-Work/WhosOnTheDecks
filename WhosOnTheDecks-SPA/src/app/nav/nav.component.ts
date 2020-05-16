import { Component, OnInit } from "@angular/core";
import { AuthService } from "../_service/auth.service";
import { AlertifyService } from "../_service/alertly.service";
import { Router } from "@angular/router";
import { get } from "lodash";
import { format } from "url";

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

  // Login makes a call to auth service to the login method
  // The entered email and password is sent in the model declared above
  // If succefful a message is displayed and the is promoter check and is djcheck are called
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

  // Logged in makes a call to the authservice mthod loggedIn
  // this will return the Json web token
  loggedIn() {
    return this.authService.loggedIn();
  }

  // Logout sets isPromoter and isDj to false
  // the token in local storage is then removed
  // A message is diplayed and the user is directted home
  logout() {
    this.isPromoter = false;
    this.isDj = false;
    localStorage.removeItem("token");
    this.alertify.message("Logged Out");
    this.router.navigate(["/home"]);
  }

  // IsPromoter check checks the decoded token for the role
  // If the user is a promtoer is sets isPromtoer to true
  isPromoterCheck() {
    if (get(this.authService, "decodedToken.role", "") === "Promoter") {
      this.isPromoter = true;
    }
  }

  // IsDj cheks to see if the logged in user's role is a Dj and sets isDJ to true;
  isDjCheck() {
    if (get(this.authService, "decodedToken.role", "") === "Dj") {
      this.isDj = true;
    }
  }
}
