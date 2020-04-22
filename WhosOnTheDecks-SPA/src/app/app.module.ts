import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
// import { BsDropdownModule } from "ngx-bootstrap/dropdown";

import { AppComponent } from "./app.component";
import { NavComponent } from "./nav/nav.component";
import { FormsModule } from "@angular/forms";
import { AuthService } from "./_service/auth.service";
import { HomeComponent } from "./home/home.component";
import { ErrorInterceptorProvider } from "./_service/error.interceptor";
import { RegisterPromoterComponent } from "./register-promoter/register-promoter.component";
import { ViewAllDjsComponent } from "./view-all-djs/view-all-djs.component";
import { CreateAnEventComponent } from "./create-an-event/create-an-event.component";
import { EditProfileComponent } from "./edit-profile/edit-profile.component";
import { MakeABookingComponent } from "./make-a-booking/make-a-booking.component";
import { ViewBookingsComponent } from "./view-bookings/view-bookings.component";
import { ViewEventsComponent } from "./view-events/view-events.component";
import { appRoutes } from "./routes";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
  MatSelectModule,
  MatFormFieldModule,
  MatInputModule,
} from "@angular/material";

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterPromoterComponent,
    ViewAllDjsComponent,
    CreateAnEventComponent,
    EditProfileComponent,
    MakeABookingComponent,
    ViewBookingsComponent,
    ViewEventsComponent,
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
    // BsDropdownModule.forRoot(),
  ],
  providers: [ErrorInterceptorProvider, AuthService],
  bootstrap: [AppComponent],
})
export class AppModule {}
