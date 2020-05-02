import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Event } from "../_models/event";
import { Dj } from "../_models/dj";

@Injectable({
  providedIn: "root",
})
export class PromoterService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getPromoterEvents(id): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseUrl + "promoters/events/" + id);
  }

  getDj(id): Observable<Dj> {
    return this.http.get<Dj>(this.baseUrl + "promoters/booking/" + id);
  }
}
