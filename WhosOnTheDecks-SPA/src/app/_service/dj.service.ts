import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Booking } from "../_models/booking";
import { EventDisplay } from "../_models/event-display";

@Injectable({
  providedIn: "root",
})
export class DjService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getDjEvents(id): Observable<EventDisplay[]> {
    return this.http.get<EventDisplay[]>(
      this.baseUrl + "djs/getdjevents/" + id
    );
  }

  getDjBookings(id): Observable<Booking[]> {
    return this.http.get<Booking[]>(this.baseUrl + "djs/getdjbookings/" + id);
  }
}
