import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Party } from "../_models/party";

@Injectable({
  providedIn: "root",
})
export class EventService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getEvents(id): Observable<Party[]> {
    return this.http.get<Party[]>(this.baseUrl + "events/getevents/" + id);
  }

  getEvent(id): Observable<Party> {
    return this.http.get<Party>(this.baseUrl + "events/getevent/" + id);
  }

  createEvent(model: any, id): Observable<Party> {
    return this.http.get<Party>(this.baseUrl + "events/create/" + model + id);
  }
}
