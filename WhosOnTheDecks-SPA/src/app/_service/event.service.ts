import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { EventDisplay } from "../_models/event-display";

@Injectable({
  providedIn: "root",
})
export class EventService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getEvents(id): Observable<EventDisplay[]> {
    return this.http.get<EventDisplay[]>(
      this.baseUrl + "events/getevents/" + id
    );
  }

  getEvent(id): Observable<EventDisplay> {
    return this.http.get<EventDisplay>(this.baseUrl + "events/getevent/" + id);
  }
}
