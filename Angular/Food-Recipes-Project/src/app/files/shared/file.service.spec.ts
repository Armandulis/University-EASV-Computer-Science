import { TestBed } from '@angular/core/testing';

import { FileService } from './file.service';
import {Helper} from '../../../testing/recipes-helper';
import {of} from 'rxjs';
import {AngularFireModule} from '@angular/fire';
import {AngularFirestore, AngularFirestoreModule} from '@angular/fire/firestore';
import {AngularFireStorage} from '@angular/fire/storage';
describe('FileService', () => {
  //  private storage: AngularFireStorage, private db: AngularFirestore
  let firestoreMock: any;
  let firestoreCollection: any;
  let fireStorageMock: any;
  let helper: Helper;

  beforeEach(() => {
    helper = new Helper();
    firestoreMock = jasmine.createSpyObj('AngularFirestore', ['collection', 'add']);
    firestoreCollection = jasmine.createSpyObj('collection', ['snapshotChanges', 'valueChanges']);
    firestoreCollection.snapshotChanges.and.returnValue(of([]));
    firestoreMock.collection.and.returnValue(firestoreCollection);

    fireStorageMock = jasmine.createSpyObj('AngularFireStorage', ['ref', 'getDownloadURL', 'put']);

    TestBed.configureTestingModule({
    imports: [

      AngularFirestoreModule,
      AngularFireModule
    ],
    providers: [
      {provide: AngularFirestore, useValue: firestoreMock},
      {provide: AngularFireStorage, useValue: fireStorageMock},
    ]
  });
});
  it('should be created', () => {
    const service: FileService = TestBed.get(FileService);
    expect(service).toBeTruthy();
  });
});
