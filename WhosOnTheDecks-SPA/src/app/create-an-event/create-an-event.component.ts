import { Component, OnInit } from "@angular/core";
import { CreateEvent } from "../_models/create-event";

@Component({
  selector: "app-create-an-event",
  templateUrl: "./create-an-event.component.html",
  styleUrls: ["./create-an-event.component.css"],
})
export class CreateAnEventComponent implements OnInit {
  ev: CreateEvent;
  constructor() {}

  ngOnInit() {}
}
