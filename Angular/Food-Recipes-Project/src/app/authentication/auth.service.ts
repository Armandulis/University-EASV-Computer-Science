/* tslint:disable:no-trailing-whitespace */
import {Injectable, OnInit} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {User} from '../shared/models/user';
import {AngularFireDatabase} from '@angular/fire/database';
import {Router} from '@angular/router';
import {AngularFireAuth} from 'angularfire2/auth';
import * as firebase from 'firebase';
import {map} from 'rxjs/operators';
import {DataSharingService} from '../shared/services/data-sharing.service';
@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {

  private user: Observable<User>;
  constructor(private router: Router,
              private dataShare: DataSharingService) {
  }

  login(email: string, password: string) {
    const auth = firebase.auth();
    auth.signInWithEmailAndPassword(email, password).catch(function(error) {
      console.log(error.code);
      console.log(error.message);
    });
    const user = auth.currentUser;
  }

  signOut() {
    const auth = firebase.auth();
    auth.signOut();
    const user = auth.currentUser;
  }

  createUser(email: string, password: string) {
    const auth = firebase.auth();
    auth.createUserWithEmailAndPassword(email, password).catch(function(error) {
      alert(error.message);
    });

  }

  ngOnInit(): void {

  }
}
