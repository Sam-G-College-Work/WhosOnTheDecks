import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {

  // Class property to store variable from method getValues
  values: any;

  // The addition of the httpclient in the constructor will allow API calls to our backend application
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  getValues() {
    // Making a direct call to the api via the url address
    // The server information is subcribed to and the repsonse is stored as the property values
    // An error handler is added to give the right notification if an error is hit
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }

}
