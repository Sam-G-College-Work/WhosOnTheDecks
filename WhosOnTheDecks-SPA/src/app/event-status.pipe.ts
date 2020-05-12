import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "eventStatus",
})
// This is a pipe i have created which will allow me to format the boolean Event status to show either live or cancelled
export class EventStatusPipe implements PipeTransform {
  transform(value: any, ...args: any[]): any {
    return value ? "Live" : "Cancelled";
  }
}
