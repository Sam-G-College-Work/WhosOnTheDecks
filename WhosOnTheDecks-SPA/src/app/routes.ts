import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { CreateAnEventComponent } from "./create-an-event/create-an-event.component";
import { ViewAllDjsComponent } from "./view-all-djs/view-all-djs.component";
import { ViewBookingsComponent } from "./view-bookings/view-bookings.component";
import { ViewEventsComponent } from "./view-events/view-events.component";
import { AuthGuard } from "./_guards/auth.guard";
import { ViewBookedDjComponent } from "./view-events/view-booked-dj/view-booked-dj.component";

export const appRoutes: Routes = [
  { path: "", component: HomeComponent },
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      { path: "create-an-event", component: CreateAnEventComponent },
      { path: "view-all-djs", component: ViewAllDjsComponent },
      { path: "view-bookings", component: ViewBookingsComponent },
      { path: "view-events", component: ViewEventsComponent },
      { path: "view-events/:id", component: ViewBookedDjComponent },
    ],
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];
