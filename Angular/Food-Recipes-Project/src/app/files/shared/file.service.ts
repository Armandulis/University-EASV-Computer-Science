import { Injectable } from '@angular/core';
import {defer, from, Observable, pipe} from 'rxjs';
import {FileMetaData} from './file-metadata';
import {AngularFireStorage} from '@angular/fire/storage';
import {AngularFirestore} from '@angular/fire/firestore';
import {map, switchMap} from 'rxjs/operators';
import {ImageMetadata} from './image-metadata';
@Injectable({
  providedIn: 'root'
})
export class FileService {

  croppedBlob: Blob;
  imageChangedEvent: any = '';

  constructor(private storage: AngularFireStorage, private db: AngularFirestore) {
  }

  uploadImage(imageMetaData: ImageMetadata): Observable<FileMetaData> {
    if (imageMetaData.imageBlob) {
      const fileToUpload = new File([imageMetaData.imageBlob], imageMetaData.fileMeta.name,
        {type: imageMetaData.imageBlob.type});
      return this.upload(fileToUpload);
    }
  }
  upload(file: File): Observable<FileMetaData> {
    return this.addFileMetadata(
      {
        name: file.name,
        type: file.type,
        size: file.size,
        lastModified: file.lastModified
      }
    ).pipe(
      switchMap(fileMeta => {
        return defer(() =>
          this.storage.ref('recipe-pictures/' + fileMeta.id)
            .put(file)
            .then()
        ).pipe(
          map(fileRef => {
            fileRef.id = fileMeta.id;
            return fileRef;
          })
        );
      })
    );

  }

  addFileMetadata(meta: FileMetaData): Observable<FileMetaData> {
    return defer(() =>
      this.db.collection('files')
        .add(meta)
    ).pipe(
      map(documentRef => {
        meta.id = documentRef.id;
        return meta;
      })
    );
  }
  getFileUrl(id: string ): Observable<any> {
    return this.storage.ref('recipe-pictures/' + id)
      .getDownloadURL();
  }

  public setUpFileInfo(): ImageMetadata {

    let imageMetadata: ImageMetadata;


    if (this.imageChangedEvent &&
      this.imageChangedEvent.target &&
      this.imageChangedEvent.target.files &&
      this.imageChangedEvent.target.files.length > 0) {
      const imageBeforeCropped = this.imageChangedEvent.target.files[0];

      imageMetadata = {
        imageBlob: this.croppedBlob,
        fileMeta: {
          name: imageBeforeCropped.name,
          type: imageBeforeCropped.type,
          size: imageBeforeCropped.size
        }
      };
      return imageMetadata;
    }
  }

  chosenImage(event): ImageMetadata {
    this.imageChangedEvent = event;
    return this.setUpFileInfo();
  }
  imageCropped(event): ImageMetadata {
    this.croppedBlob = event.file;
    return this.setUpFileInfo();
  }


}
