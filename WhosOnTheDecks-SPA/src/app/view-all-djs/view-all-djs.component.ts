import { Component, OnInit } from "@angular/core";
import { Dj } from "../_models/dj";
import { AlertifyService } from "../_service/alertly.service";
import { MatCardModule } from "@angular/material/card";
import { AuthService } from "../_service/auth.service";
import { HomeService } from "../_service/home.service";

@Component({
  selector: "app-view-all-djs",
  templateUrl: "./view-all-djs.component.html",
  styleUrls: ["./view-all-djs.component.css"],
})
export class ViewAllDjsComponent implements OnInit {
  djs: Dj[];
  isdj = false;

  constructor(
    private homeService: HomeService,
    private alertify: AlertifyService,
    private authService: AuthService,
    matCardModule: MatCardModule
  ) {}

  ngOnInit() {
    this.isDj();
    this.loadDjs();
  }

  loadDjs() {
    this.homeService.getDjs().subscribe(
      (djs: Dj[]) => {
        this.djs = djs;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  isDj() {
    if (this.authService.decodedToken.role === "Dj") {
      this.isdj = true;
    }
  }
}
