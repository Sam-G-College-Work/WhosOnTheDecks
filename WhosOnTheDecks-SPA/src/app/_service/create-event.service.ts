import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { CreateEvent } from "../_models/create-event";
import { Observable } from "rxjs";
import { Dj } from "../_models/dj";

@Injectable({
  providedIn: "root",
})
export class CreateEventService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createEvent(promoterId, djId, ev: CreateEvent) {
    return this.http.post(
      this.baseUrl + "createevents/create/" + promoterId + "/" + djId,
      ev
    );
  }

  getPromoterOrders(promoterId) {
    return this.http.get(this.baseUrl + "createevents/getorders/" + promoterId);
  }

  cancelOrders(promoterId) {
    return this.http.delete(this.baseUrl + "createevents/cancel/" + promoterId);
  }

  getAvaliableDjs(evNew: CreateEvent): Observable<Dj[]> {
    return this.http.post<Dj[]>(
      this.baseUrl + "createevents/avaliabledjs",
      evNew
    );
  }

  shoppingExists(promoterId) {
    return this.http.get(
      this.baseUrl + "createevents/shoppingexists/" + promoterId
    );
  }
}
