import { Component, OnInit } from "@angular/core";
import { Dj } from "../_models/dj";
import { DjService } from "../_service/dj.service";
import { AlertifyService } from "../_service/alertly.service";

@Component({
  selector: "app-view-all-djs",
  templateUrl: "./view-all-djs.component.html",
  styleUrls: ["./view-all-djs.component.css"],
})
export class ViewAllDjsComponent implements OnInit {
  listOfDj: Dj[];

  constructor(
    private djService: DjService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.loadDjs();
  }

  loadDjs() {
    this.djService.getDjs().subscribe(
      (djs: Dj[]) => {
        this.listOfDj = djs;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
