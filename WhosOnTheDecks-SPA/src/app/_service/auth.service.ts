import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  baseUrl = environment.apiUrl + "auth/";

  jwtHelper = new JwtHelperService();

  decodedToken: any;

  constructor(private http: HttpClient) {}

  // Login method calles back to back end auth controller
  // The url login is usd and the model is supplied.
  // a pipe is used to map the token repsonse and assign tha to a const
  // the const is checked to make sure it exists
  // if so the token is stored in local storage
  // The token is then decoded using jwt helper
  login(model: any) {
    return this.http.post(this.baseUrl + "login", model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem("token", user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }

  // Register method makes a call to auth controller in backend using the promoterregister url
  // the model is supplied
  registerPromoter(model: any) {
    return this.http.post(this.baseUrl + "promoterregister", model);
  }

  // Logged in retrieves token form local storage and returns if the token is not expired
  loggedIn() {
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }
}
