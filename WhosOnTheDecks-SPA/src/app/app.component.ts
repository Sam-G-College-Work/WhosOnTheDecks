import { Component, OnInit } from "@angular/core";
import { AuthService } from "./_service/auth.service";
import { JwtHelperService } from "@auth0/angular-jwt";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
// This is the main component of he angular application
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService(); // 3rd party JWTHelper service is declared

  // Constructor calls auth service to pe accessible
  constructor(private authservice: AuthService) {}

  // On initiation token is created and called from local storage where the back end has stored a token
  ngOnInit() {
    const token = localStorage.getItem("token");
    if (token) {
      this.authservice.decodedToken = this.jwtHelper.decodeToken(token); // Auth service using JWT helper will decode the token
    }
  }
}
