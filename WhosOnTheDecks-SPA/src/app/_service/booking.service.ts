import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Booking } from "../_models/booking";

@Injectable({
  providedIn: "root",
})
export class BookingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getEventBooking(id): Observable<Booking> {
    return this.http.get<Booking>(
      this.baseUrl + "bookings/geteventbookings/" + id
    );
  }

  getDjBookings(id): Observable<Booking[]> {
    return this.http.get<Booking[]>(
      this.baseUrl + "bookings/getdjbookings/" + id
    );
  }
}
