import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertly.service';

@Component({
  selector: 'app-register-promoter',
  templateUrl: './register-promoter.component.html',
  styleUrls: ['./register-promoter.component.css']
})
export class RegisterPromoterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.registerPromoter(this.model).subscribe(() => {
      this.alertify.success('registration successful');
    }, error => {
      this.alertify.error(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alertify.message('cancelled');
  }

}
