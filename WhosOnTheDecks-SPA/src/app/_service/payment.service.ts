import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { CardDetails } from "../_models/card-details";

@Injectable({
  providedIn: "root",
})
export class PaymentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  makePayment(promoterId, cardDetails: CardDetails) {
    return this.http.post(
      this.baseUrl + "payments/payment/" + promoterId,
      cardDetails
    );
  }
}
