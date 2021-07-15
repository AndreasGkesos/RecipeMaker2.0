import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'oidc-client';
import { ApiCallService } from './core/api-call.service';
import { AuthService } from './core/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  constructor(public authService: AuthService, private fb: FormBuilder, private apicall: ApiCallService) {
  }

  messages: string[] = [];
  get currentUserJson(): string {
    return JSON.stringify(this.currentUser, null, 2);
  }
  currentUser!: User;
  
  supplierForm!: FormGroup;

  supplierLength!: string;

  errorMessage!: string;
  
  ngOnInit(): void {
    this.supplierForm = this.fb.group({
      // id: ['', Validators.required]
    });

    // this.authService.getUser().then(user => {
    //   this.currentUser = user;

    //   if (user) {
    //     this.addMessage('User Logged In');
    //   } else {
    //     this.addMessage('User Not Logged In');
    //   }
    // }).catch(err => this.addError(err));

    if (localStorage.getItem('access_token')) {
      this.addMessage('User Logged In');
    } else {
      this.addMessage('User Not Logged In');
    }
  }

  clearMessages() {
    while (this.messages.length) {
      this.messages.pop();
    }
  }
  addMessage(msg: string) {
    this.messages.push(msg);
  }
  addError(msg: string | any) {
    this.messages.push('Error: ' + msg && msg.message);
  }

  public onLogin() {
    this.clearMessages();
    this.authService.login().catch(err => {
      this.addError(err);
    });
  }

  public onRenewToken() {
    this.clearMessages();
    this.authService.renewToken()
      .then(user => {
        this.currentUser = user;
        this.addMessage('Silent Renew Success');
      })
      .catch(err => this.addError(err));
  }

  public onLogout() {
    this.clearMessages();
    this.authService.logout().catch(err => this.addError(err));
  }

  public getAllSuppliers() {    
    this.apicall.getAllSuppliers()
      .subscribe(res => {
        this.supplierLength = res.length;
        this.errorMessage = '';
        console.log(res);
      }, err => {
        this.errorMessage = `Error status: ${err.status}`;
        console.log('error', err)
      });
  }
}
