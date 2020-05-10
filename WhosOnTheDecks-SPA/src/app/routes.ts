import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { CreateAnEventComponent } from "./create-an-event/create-an-event.component";
import { ViewAllDjsComponent } from "./view-all-djs/view-all-djs.component";
import { ViewBookingsComponent } from "./view-bookings/view-bookings.component";
import { ViewEventsComponent } from "./view-events/view-events.component";
import { AuthGuard } from "./_guards/auth.guard";
import { ViewBookedDjComponent } from "./view-events/view-booked-dj/view-booked-dj.component";
import { BookingResponseComponent } from "./view-bookings/booking-response/booking-response.component";
import { SelectADjComponent } from "./create-an-event/select-a-dj/select-a-dj.component";
import { ConfirmEventsComponent } from "./create-an-event/confirm-events/confirm-events.component";
import { PaymentComponent } from "./create-an-event/payment/payment.component";

export const appRoutes: Routes = [
  { path: "", component: HomeComponent },
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      { path: "create-an-event", component: CreateAnEventComponent },
      { path: "select-a-dj", component: SelectADjComponent },
      { path: "confirm-events/:id", component: ConfirmEventsComponent },
      { path: "payment", component: PaymentComponent },
      { path: "view-all-djs", component: ViewAllDjsComponent },
      { path: "view-bookings/:id", component: BookingResponseComponent },
      { path: "view-bookings", component: ViewBookingsComponent },
      { path: "view-events/:id", component: ViewBookedDjComponent },
      { path: "view-events", component: ViewEventsComponent },
    ],
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];
