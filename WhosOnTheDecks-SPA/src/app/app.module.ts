import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { JwtModule } from "@auth0/angular-jwt";

import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { FormsModule } from "@angular/forms";
import { AuthService } from "./_service/auth.service";
import { DjService } from "./_service/dj.service";
import { HomeComponent } from "./home/home.component";
import { ErrorInterceptorProvider } from "./_service/error.interceptor";
import { RegisterPromoterComponent } from "./register-promoter/register-promoter.component";
import { ViewAllDjsComponent } from "./view-all-djs/view-all-djs.component";
import { CreateAnEventComponent } from "./create-an-event/create-an-event.component";
import { ViewBookingsComponent } from "./view-bookings/view-bookings.component";
import { ViewEventsComponent } from "./view-events/view-events.component";
import { appRoutes } from "./routes";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
  MatSelectModule,
  MatFormFieldModule,
  MatInputModule,
  MatCardModule,
  MatGridListModule,
} from "@angular/material";
import { ViewEventBookingComponent } from "./view-event-booking/view-event-booking.component";
import { tap } from "rxjs/operators";

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterPromoterComponent,
    ViewAllDjsComponent,
    CreateAnEventComponent,
    ViewBookingsComponent,
    ViewEventsComponent,
    ViewEventBookingComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatGridListModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5000"],
        blacklistedRoutes: ["localhost:5000/api/auth"],
      },
    }),
  ],
  providers: [ErrorInterceptorProvider, AuthService, DjService],
  bootstrap: [AppComponent],
})
export class AppModule {}
