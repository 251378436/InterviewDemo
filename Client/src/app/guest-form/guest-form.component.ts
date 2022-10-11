import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { Guest } from '../guest';

@Component({
  selector: 'app-guest-form',
  templateUrl: './guest-form.component.html',
  styleUrls: ['./guest-form.component.scss']
})
export class GuestFormComponent implements OnInit {
  guest: Guest = {
    firstName: 'Bikkk',
    lastName: 'Windstorm'
  };
  message: string = '';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  submit(): void {
    let self = this;
    const observer = {
      next(response: any) {
        self.message = 'succeed';
      },
      error(error: HttpErrorResponse) {
       self.message = 'failed';
      }
    };
    this.http.post('https://localhost:7208/api/Guests', this.guest, {})
    .subscribe(observer);
  }
}
