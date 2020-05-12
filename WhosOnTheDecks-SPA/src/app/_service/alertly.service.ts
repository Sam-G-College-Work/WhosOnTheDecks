import { Injectable } from "@angular/core";
import * as alertify from "alertifyjs";

@Injectable({
  providedIn: "root",
})
export class AlertifyService {
  constructor() {}

  // Sets oncfirm notification to be the ok return messgae from the back end
  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallback();
      } else {
      }
    });
  }

  // Sets the alertify message to be a success message
  success(message: string) {
    alertify.success(message);
  }

  // Sets the alertify errors message to be an error
  error(message: string) {
    alertify.error(message);
  }

  // Sets the alertify message to be a warning message
  warning(message: string) {
    alertify.warning(message);
  }

  // Sets the message to be a message
  message(message: string) {
    alertify.message(message);
  }
}
