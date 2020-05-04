import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "bookingStatus",
})
export class BookingStatusPipe implements PipeTransform {
  transform(value: any, ...args: any[]): any {
    return value ? "Accepted" : "Declined";
  }
}
