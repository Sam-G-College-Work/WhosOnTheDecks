import { CurrencyPipe } from "@angular/common";

export interface Event {
  eventId: number;
  dateCreated: Date;
  dateOfEvent: Date;
  eventStartTime: Date;
  eventEndTime: Date;
  entryCost: number;
  eventStatus: boolean;
  eventAddress: string;
  postcode: string;
  totalCost: number;
}
