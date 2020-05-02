export interface Event {
  eventId: number;
  dateCreated: Date;
  dateTimeOfEvent: Date;
  lengthOfEvent: number;
  totalCost: number;
  eventStatus: boolean;
  eventAddress: string;
  postcode: string;
}
