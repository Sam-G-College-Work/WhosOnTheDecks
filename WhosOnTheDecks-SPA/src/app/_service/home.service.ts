import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Dj } from "../_models/dj";

@Injectable({
  providedIn: "root",
})
export class HomeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getDjs(): Observable<Dj[]> {
    return this.http.get<Dj[]>(this.baseUrl + "home");
  }
}
