import { TestBed } from '@angular/core/testing';

import { RecipesService } from './recipes.service';
import {
  AngularFirestore,
} from '@angular/fire/firestore';
import {FileService} from '../../files/shared/file.service';
import {of} from 'rxjs';
import {Helper} from '../../../testing/recipes-helper';
import {AngularFireModule} from '@angular/fire';



describe('RecipesService', () => {
  // private db: AngularFirestore, private fs: FileService
  let firestoreMock: any;
  let firestoreCollection: any;
  let fileServiceMock: any;
  let service: RecipesService;
  let helper: Helper;

  beforeEach(() => {
    helper = new Helper();
    firestoreMock = jasmine.createSpyObj('AngularFirestore', ['collection']);
    firestoreCollection = jasmine.createSpyObj('collection', ['snapshotChanges', 'valueChanges']);
    firestoreMock.collection.and.returnValue(firestoreCollection);
    firestoreCollection.snapshotChanges.and.returnValue(of([]));


    fileServiceMock = jasmine.createSpyObj('FileService', ['getFileUrl', 'uploadImage']);
    TestBed.configureTestingModule({
      imports: [
        AngularFireModule
      ],
      providers: [
        {provide: AngularFirestore, useValue: firestoreMock},
        {provide: FileService, useValue: fileServiceMock},
      ]
    });
  });
  it('should be created', () => {
    service = TestBed.get(RecipesService);
    expect(service).toBeTruthy();
  });

  describe('Create Recipes', () => {
    beforeEach(() => {
      service.getRecipes();
    });
    it('should call collection on firestore once', () => {
      service.getRecipes();
      expect(firestoreMock.collection).toHaveBeenCalledTimes(1);
    });
    it('should call collection on firestore with recipes as a parameter', () => {
      expect(firestoreMock.collection).toHaveBeenCalledWith('recipes');
    });
    it('should call snapshotChanges firestore one time', () => {
      expect(firestoreCollection.snapshotChanges).toHaveBeenCalledTimes(1);
    });
  });
  describe('Create Recipes with Return values', () => {
    it('should call getRecipes and return one recipe', () => {
      firestoreCollection.snapshotChanges.and.returnValue(of(helper.createRecipes(1)));
      service.getRecipes().subscribe(recipes => {
        expect(recipes.length).toBe(1);
      });
    });
    it('should call getRecipes and return 20 recipe', () => {
      firestoreCollection.snapshotChanges.and.returnValue(of(helper.createRecipes(20)));
      service.getRecipes().subscribe(recipes => {
        expect(recipes.length).toBe(20);
      });
    });
    it('should call getRecipes and return one recipe with correct properties', () => {
      firestoreCollection.snapshotChanges.and.returnValue(of(helper.createRecipes(1)));
      service.getRecipes().subscribe(recipes => {
        expect(recipes[0]).toEqual({
          id: 'abc0',
          type: 'tast',
          name: 'name',
          portion: 'portion',
          howTo: 'string',
          cookTime: 'string'
        });
      });
    });
    it('should call getFileUrl if picture exists in recipe', () => {
      firestoreCollection.snapshotChanges.and.returnValue(of(helper.getActionsWithPicture(1)));
      service.getRecipes().subscribe(recipes => {
        expect(fileServiceMock.getFileUrl).toHaveBeenCalledTimes(1);
      });
    });
  });
  describe('Read Recipes', () => {
    it('should ', () => {

    });
  });
  describe('Upadate Recipes', () => {
    it('should ', () => {

    });
  });
  describe('Delete Recipes', () => {
    it('should ', () => {

    });
  });
});
