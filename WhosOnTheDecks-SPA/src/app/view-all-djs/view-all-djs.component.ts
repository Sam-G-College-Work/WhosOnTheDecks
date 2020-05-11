import { uniq } from "lodash";
import { Component, OnInit } from "@angular/core";
import { Dj } from "../_models/dj";
import { AlertifyService } from "../_service/alertly.service";
import { AuthService } from "../_service/auth.service";
import { HomeService } from "../_service/home.service";
import { MatSelectChange } from "@angular/material";

@Component({
  selector: "app-view-all-djs",
  templateUrl: "./view-all-djs.component.html",
  styleUrls: ["./view-all-djs.component.css"],
})
export class ViewAllDjsComponent implements OnInit {
  djs: Dj[];
  djsToDisplay: Dj[];
  genres: string[];
  isdj = false;
  selected: string;

  constructor(
    private homeService: HomeService,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.isDj();
    this.loadDjs();
  }

  loadDjs() {
    this.homeService.getDjs().subscribe(
      (djs: Dj[]) => {
        this.djs = djs;
        this.djsToDisplay = this.djs;
        this.genres = uniq(this.djs.map((dj) => dj.genre)).sort();
        this.genres = ["Any", ...this.genres];
        this.selected = "Any";
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

  genreChanged(eventChange: MatSelectChange) {
    const newGenre = eventChange.value;

    if (newGenre === "Any") {
      this.djsToDisplay = this.djs;
    } else {
      this.djsToDisplay = this.djs.filter((dj) => dj.genre === newGenre);
    }

  }
}
