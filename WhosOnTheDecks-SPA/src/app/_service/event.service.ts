import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Event } from "../_models/event";

@Injectable({
  providedIn: "root",
})
export class EventService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getEvents(id): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseUrl + "events/" + id);
  }

  getEvent(id): Observable<Event> {
    return this.http.get<Event>(this.baseUrl + "events/" + id);
  }
}
