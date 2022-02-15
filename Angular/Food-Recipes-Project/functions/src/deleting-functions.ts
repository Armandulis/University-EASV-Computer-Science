import * as functions from 'firebase-functions';
import * as admin from 'firebase-admin';

exports.deleteProducts =
  functions.firestore
    .document('recipes/{id}')
    .onDelete((snapshot, context) => {
      return new Promise(async (resolve, reject) =>{
        const deletedValue = snapshot.data();
        if (deletedValue){

          try{
            await admin.firestore().collection('files')
              .doc(deletedValue.picture)
              .delete()
              .then();

            const resultFromStorage = await admin.storage().bucket()
              .file('recipe-pictures/' + deletedValue.picture)
              .delete()
              .then();

            resolve(resultFromStorage);
          }
          catch (error) {
            reject(error);
          }
        } else {
          reject('No recipes');
        }

      });

    });
