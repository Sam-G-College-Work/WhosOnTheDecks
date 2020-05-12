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

  // Get Dj events calls to the djs controller and using the dj id will return a list of that djs events
  getDjEvents(id): Observable<EventDisplay[]> {
    return this.http.get<EventDisplay[]>(
      this.baseUrl + "djs/getdjevents/" + id
    );
  }

  // Get booking makes a call to the djs controller nd suplying an event id will return the booking details
  getDjBooking(id): Observable<Booking> {
    return this.http.get<Booking>(this.baseUrl + "djs/booking/" + id);
  }

  // post response will make a call to the djs controller and using the booking id and the booking object
  // will change the status of the booking to the selected repsonse
  postResponse(id: number, booking: Booking) {
    return this.http.put(`${this.baseUrl}djs/update/${id}`, booking);
  }
}
