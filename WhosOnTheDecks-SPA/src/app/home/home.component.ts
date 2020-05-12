import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthService } from "../_service/auth.service";

export interface Food {
  value: string;
  viewValue: string;
}

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(private http: HttpClient, private authService: AuthService) {}

  ngOnInit() {}

  registerToggle() {
    this.registerMode = true;
  }

  // Cancel register mode takes in a boolen from child components event emitter cancel register
  // This then sets register mode to false
  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

  // Returns the logged in users json web token
  loggedIn() {
    return this.authService.loggedIn();
  }
}
