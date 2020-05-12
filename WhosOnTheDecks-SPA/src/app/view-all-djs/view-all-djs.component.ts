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

  // Load Djs makes a call to the home service page
  // The method get djs is called
  // The return is then subscribed to and the djs list is assigned to the djs array
  // If an exception is hit an eror message will display
  // All genres are taken from the djs and added to a genre array
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

  // Is DJ uses auth service to decode the singed in users token to find out the role they are assinged to
  // If returned true the html will display Dj specific tabs and remove promoter ones
  isDj() {
    if (this.authService.decodedToken.role === "Dj") {
      this.isdj = true;
    }
  }

  // GenreChanged takes in an event of mat select change
  // This will determin what genre has been picked
  // A new array of djs is used to store only the djs with this genre
  // This allows the display of the specific djs
  genreChanged(eventChange: MatSelectChange) {
    const newGenre = eventChange.value;

    if (newGenre === "Any") {
      this.djsToDisplay = this.djs;
    } else {
      this.djsToDisplay = this.djs.filter((dj) => dj.genre === newGenre);
    }
  }
}
