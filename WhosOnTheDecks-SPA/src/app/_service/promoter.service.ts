import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { EventDisplay } from "../_models/event-display";
import { Dj } from "../_models/dj";
import { Booking } from "../_models/booking";

@Injectable({
  providedIn: "root",
})
export class PromoterService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getPromoterEvents(id): Observable<EventDisplay[]> {
    return this.http.get<EventDisplay[]>(
      this.baseUrl + "promoters/events/" + id
    );
  }

  getDj(id): Observable<Dj> {
    return this.http.get<Dj>(this.baseUrl + "promoters/dj/" + id);
  }

  getBooking(id): Observable<Booking> {
    return this.http.get<Booking>(this.baseUrl + "promoters/booking/" + id);
  }
}
