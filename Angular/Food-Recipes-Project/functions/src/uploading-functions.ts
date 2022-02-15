import * as functions from 'firebase-functions';
import * as admin from 'firebase-admin';

exports.uploadedNewCerealsImage =
  functions.storage.object().onFinalize( object => {

    return new Promise((resolve, reject) =>{

      if(object && object.name && object.metadata) {
        const fileMeta = {
          lastModified: object.updated,
          name: object.metadata.originalName,
          type: 'image.png',
          size: object.size
        };
        const nameForDoc = object.name.split('/')[1];
        console.log(nameForDoc);
        admin.firestore().collection('files').doc(nameForDoc).set(fileMeta)
          .then(value => resolve(value))
          .catch(error => reject(error));

      } else{
        reject('not enought metadata or file data');
      }

    });
  });
