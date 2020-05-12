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

  // Create event calls to the createevents controller and the create method
  // a promtoer id and djid are supplied in the url with an event json being passed
  createEvent(promoterId, djId, ev: CreateEvent) {
    return this.http.post(
      this.baseUrl + "createevents/create/" + promoterId + "/" + djId,
      ev
    );
  }

  // Get promoter orders calls to the createevents service to the get orders method and suplies the promoter id
  // this will return all the promoters orders still due to be paid
  getPromoterOrders(promoterId) {
    return this.http.get(this.baseUrl + "createevents/getorders/" + promoterId);
  }

  // Cancel orders calls to the createevents service to the cancel method and supplies it with a promoter id
  // this will then delete all the promoters current orders
  cancelOrders(promoterId) {
    return this.http.delete(this.baseUrl + "createevents/cancel/" + promoterId);
  }

  // Get avaliable djs makes a call to the create events service to the avaliabledjs method
  // an event json is passed and using its date the avliable djs are returned
  getAvaliableDjs(evNew: CreateEvent): Observable<Dj[]> {
    return this.http.post<Dj[]>(
      this.baseUrl + "createevents/avaliabledjs",
      evNew
    );
  }

  // Shooping exisists calls to the createveents service to the shopping exists method and using the promoter id will return a boolean
  shoppingExists(promoterId) {
    return this.http.get(
      this.baseUrl + "createevents/shoppingexists/" + promoterId
    );
  }
}
