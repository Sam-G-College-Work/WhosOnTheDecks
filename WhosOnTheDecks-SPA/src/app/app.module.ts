import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { JwtModule } from "@auth0/angular-jwt";

import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { FormsModule } from "@angular/forms";
import { AuthService } from "./_service/auth.service";
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
  MatDatepickerModule,
  MatNativeDateModule,
} from "@angular/material";
import { HomeService } from "./_service/home.service";
import { PromoterService } from "./_service/promoter.service";
import { ViewBookedDjComponent } from "./view-events/view-booked-dj/view-booked-dj.component";
import { SelectADjComponent } from "./create-an-event/select-a-dj/select-a-dj.component";
import { EventStatusPipe } from "./event-status.pipe";
import { BookingResponseComponent } from "./view-bookings/booking-response/booking-response.component";
import { DjService } from "./_service/dj.service";
import { CreateEventService } from "./_service/create-event.service";
import { PaymentComponent } from "./create-an-event/payment/payment.component";
import { ConfirmEventsComponent } from "./create-an-event/confirm-events/confirm-events.component";

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
    ViewBookedDjComponent,
    SelectADjComponent,
    EventStatusPipe,
    BookingResponseComponent,
    ConfirmEventsComponent,
    PaymentComponent,
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
    MatDatepickerModule,
    MatNativeDateModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5000"],
        blacklistedRoutes: ["localhost:5000/api/auth"],
      },
    }),
  ],
  providers: [
    ErrorInterceptorProvider,
    AuthService,
    HomeService,
    PromoterService,
    DjService,
    CreateEventService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
