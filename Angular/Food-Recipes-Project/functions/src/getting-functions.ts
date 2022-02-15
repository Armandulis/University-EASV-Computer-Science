import * as functions from 'firebase-functions';
import * as admin from 'firebase-admin';

exports.recipes = functions.https.onRequest((request, response) => {
  admin.firestore().collection('recipes').get()
    .then(listOfRecipes =>{
      const recipesList: any = [];
        listOfRecipes.forEach(recipesDB =>{
          const recipes = recipesDB.data();
          recipes.id = recipesDB.id;
          recipesList.push(recipes);
        });
      response.json(recipesList);
    })
    .catch(error => {
      console.log(error);
    })
});
