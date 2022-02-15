import {Recipe} from '../app/recipes/shared/recipe';
import {Observable, of} from 'rxjs';

export class Helper {
  recipesList: Recipe[] = [];

  createRecipes(amount: number): Recipe[] {
    for (let i = 0; i < amount; i++) {
      this.recipesList.push({
        id: 'recipesID',
        name: 'recipe' + i,
        type: 'fish',
        howTo: 'cook',
        picture: 'wdas',
        url: 'picsulr',
        cookTime: 'time: ' + i,
        portion: i + ' people',
        ingredients: [
          { name: ' cabbage',
            amount: '1'},
          { name: ' salt',
            amount: '20 g'}
        ]
      });
    }
    return this.recipesList;
  }
  // --- recipe service helper --- //
  actions: any[] = [];
  getActions(amount: number): Observable<any[]>{
    for (let i = 0; i < 0; i++){
      this.actions.push({
        payload: {
          doc: {
            id: 'abc' + i,
            data: () => {
              return {
                type: 'tast',
              name: 'name',
              portion: 'portion',
              howTo: 'string',
              cookTime: 'string'
              };
            }
          }
        }
      });
    }
    return of(this.actions);
  }
  actionsWithPicture: any[] = [];
  getActionsWithPicture(amount: number): Observable<any[]>{
    for (let i = 0; i < 0; i++) {
      this.actionsWithPicture.push({
        payload: {
          doc: {
            id: 'abc' + i,
            data: () => {
              return {
                type: 'tast',
                name: 'name',
                portion: 'portion',
                howTo: 'string',
                cookTime: 'string',
                picture: 'wow'
              };
            }
          }
        }
      });
    }
    return of(this.actionsWithPicture);
  }
}
