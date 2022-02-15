import { Injectable } from '@angular/core';
import {Recipe} from './recipe';
import {from, Observable, of} from 'rxjs';
import {AngularFirestore} from '@angular/fire/firestore';
import {map, switchMap} from 'rxjs/operators';
import {FileService} from '../../files/shared/file.service';
import {ImageMetadata} from '../../files/shared/image-metadata';

@Injectable({
  providedIn: 'root'
})
export class RecipesService {
  constructor(private db: AngularFirestore, private fs: FileService) { }

  getRecipeWithID(id: string): Observable<Recipe>  {

    return this.db.collection<Recipe>('recipes').doc(id).snapshotChanges().pipe(
      map(actions => {
        const data = actions.payload.data() as Recipe;
        const recipe: Recipe = {
          id: id,
          type: data.type,
          picture: data.picture,
          portion: data.portion,
          howTo: data.howTo,
          score: data.score,
          ingredients: data.ingredients,
          name: data.name,
          cookTime: data.cookTime
        };
        if (recipe.picture) {
          this.fs.getFileUrl(recipe.picture)
            .subscribe(url => {
              recipe.url = url;

            });
        }
        return recipe;
      })
    );

  }


  addRecipe(recipeData: Recipe): Observable<Recipe> {
      return from(this.db.collection<Recipe>('recipes').add(
        {
          name: recipeData.name,
          type: recipeData.type,
          portion: recipeData.portion,
          howTo: recipeData.howTo,
          ingredients: recipeData.ingredients,
          cookTime: recipeData.cookTime,
          picture: recipeData.picture
        }

      )
    ).pipe(
        map( recipeReference => {
            recipeData.id = recipeReference.id;
          console.log(recipeData.picture);
          return recipeData;
        })
      );
  }
  getAmountOfRecipes(amount: number): Recipe[] {
    const recipes: Recipe[] = [];
    const recipeRefer = this.db.collection('recipes');
    recipeRefer.ref.limit(amount).get().then( snapshot => {
      snapshot.forEach( doc => {
        const data = doc.data() as Recipe;
        const recipe: Recipe = {
          id: doc.id,
          name: data.name,
          type: data.type,
          howTo: data.howTo,
          portion: data.portion,
          cookTime: data.cookTime,
          picture: data.picture,
        };
        if (recipe.picture) {
          this.fs.getFileUrl(recipe.picture)
            .subscribe(url => {
              recipe.url = url;
              recipes.push(recipe);
            });
        }
      });
    });
    return recipes;
  }
/*
  searchRecipe(search: string): Observable<Recipe[]> {
    const recipes: Recipe[] = [];
    this.db.collection('recipes').ref.orderBy('name').
      .get().then( snapshots => {
        snapshots.forEach( snap => {
         const data = snap.data() as Recipe;
          const recipe: Recipe = {
            id: data.id,
            name: data.name,
            type: data.type,
            howTo: data.howTo,
            portion: data.portion,
            cookTime: data.cookTime,
            picture: data.picture,
          };
          if (recipe.picture) {
            this.fs.getFileUrl(recipe.picture)
              .subscribe(url => {
                recipe.url = url;
                recipes.push(recipe);
              });
          }
        });
    });
    return of(recipes);
  }

 */

  getRecipes(): Observable<Recipe[]> {
    return this.db
      .collection<Recipe>('recipes')
      .snapshotChanges()
      .pipe(
        map(actions => {
          return actions.map(action => {
            const data = action.payload.doc.data() as Recipe;
            const recipe: Recipe = {
              id: action.payload.doc.id,
              type: data.type,
              picture: data.picture,
              portion: data.portion,
              howTo: data.howTo,
              score: data.score,
              ingredients: data.ingredients,
              name: data.name,
              cookTime: data.cookTime
            };
              if (recipe.picture) {
                this.fs.getFileUrl(recipe.picture)
                  .subscribe(url => {
                    recipe.url = url;

                  });
              }
            return recipe;
          });
        })
      );
  }

  updateRecipe(recipeToUpdate: Recipe): Observable<void> {
   return Observable.create( obsUpdated => {
     this.db.doc('recipes/' + recipeToUpdate.id).
     update(recipeToUpdate)
       .then(() => { obsUpdated.next();})
       .catch(error =>{ obsUpdated.error(error);} )
       .finally( () => {  obsUpdated.complete();});
    });
  }


  deleteRecipe(id: string): Observable<void> {
    return Observable.create(obs => {
      this.db.doc<Recipe>('recipes/' + id)
        .delete()
        .then(() => { return obs.next(); })
        .catch(err => {return obs.error(err); })
        .finally(() => { return obs.complete(); });
    });
  }

  addRecipeWithImage(recipe: Recipe, imageMeta: ImageMetadata)
    : Observable<Recipe> {
    return this.fs.uploadImage(imageMeta)
      .pipe(
        switchMap(metadata => {
          recipe.picture = metadata.id;
          return this.addRecipe(recipe);
        })
      );
  }
}
