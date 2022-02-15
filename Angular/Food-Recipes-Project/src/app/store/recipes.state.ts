// recipes.actions.ts

import {Action, Selector, State, StateContext, Store} from '@ngxs/store';
import { asapScheduler, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import {Recipe} from '../recipes/shared/recipe';
import {RecipesService} from '../recipes/shared/recipes.service';
import {
  CreateRecipe,
  LoadRecipes,
  LoadRecipesFail,
  LoadRecipesSuccess,
  recipesActions,
  RemoveRecipe, RemoveRecipeFail, RemoveRecipeSuccess,
  SelectRecipe, UpdateRecipe, UpdateRecipeFail
} from './recipes.actions';

// recipes model
export interface RecipesStateModel {
  recipes: Recipe[];
  loaded: boolean;
  loading: boolean;
  selectedRecipeId: any;
}
// recipes initial state
@State<RecipesStateModel>({
  name: 'recipesState',
  defaults: {
    recipes: [],
    loaded: false,
    loading: false,
    selectedRecipeId: null
  }
})
export class RecipesState {
  constructor(private recipeService: RecipesService) {}
  @Selector()
  static recipes(state: RecipesStateModel) {
    return state.recipes;
  }
  @Selector()
  static loaded(state: RecipesStateModel) {
    return state.loaded;
  }
  @Selector()
  static SelectedRecipe(state: RecipesStateModel): Recipe {
    return state.recipes.find(
      (recipe: Recipe) => recipe.id === state.selectedRecipeId
    );
  }
  // load recipe xd
  @Action(LoadRecipes)
  loadRecipes({ patchState, dispatch }: StateContext<RecipesStateModel>) {
    patchState({ loading: true });
    return this.recipeService
      .getRecipes()
      .pipe(
        map((recipes: Recipe[]) =>
          asapScheduler.schedule(() =>
            dispatch(new LoadRecipesSuccess(recipes))
          )
        ),
        catchError(error =>
          of(
            asapScheduler.schedule(() =>
              dispatch(new LoadRecipesFail(error))
            )
          )
        )
      );
  }

  @Action(LoadRecipesSuccess)
  loadRecipesSuccess(
    { patchState }: StateContext<RecipesStateModel>,
    { payload }: LoadRecipesSuccess
  ) {
    patchState({ recipes: payload, loaded: true, loading: false });
  }

  @Action(LoadRecipesFail)
  loadRecipesFail(
    { dispatch }: StateContext<RecipesStateModel>,
    { payload }: LoadRecipesFail
  ) {
    dispatch({ loaded: false, loading: false });
  }

  @Action(RemoveRecipe)
  remove({getState, patchState, dispatch }: StateContext<RecipesStateModel>, { payload }: RemoveRecipe) {

    patchState({
      recipes: getState().recipes.filter(recp => recp.name !== payload.name)
    });
    return this.recipeService.deleteRecipe(payload.id).pipe(
      map(() => {
        asapScheduler.schedule(() =>
          dispatch(new RemoveRecipeSuccess())
        );
          catchError(error =>
            of(
              asapScheduler.schedule(() =>
                dispatch(new RemoveRecipeFail(error))
              )
            )
          );
      })
    );
  }

  @Action(RemoveRecipeSuccess)
  removeRecipeSuccess(
    { patchState }: StateContext<RecipesStateModel>,
  ) {
    patchState({ loaded: true, loading: false });
  }

  @Action(RemoveRecipeFail)
  removeRecipeFail(
    { dispatch }: StateContext<RecipesStateModel>,
  ) {
    dispatch({ loaded: false, loading: false });
    dispatch(new LoadRecipes());
  }
  @Action(CreateRecipe)
  add({getState, patchState, dispatch }: StateContext<RecipesStateModel>, { payload, imageMetaData }: CreateRecipe) {
    const state = getState();
    patchState({
      recipes: [...state.recipes, payload]
    });
    return this.recipeService.addRecipeWithImage(payload, imageMetaData).pipe(
      map(() => {
        catchError(error =>
          of(
            asapScheduler.schedule(() =>
              dispatch(new RemoveRecipeFail(error))
            )
          )
        );
      })
    );
  }
  @Action(RemoveRecipeFail)
  addRecipeFail(
    { dispatch }: StateContext<RecipesStateModel>,
  ) {
    dispatch({ loaded: false, loading: false });
    dispatch(new LoadRecipes());
  }
  @Action(SelectRecipe)
  selectedRecipe(
    { patchState }: StateContext<RecipesStateModel>,
    { payload }: SelectRecipe
  ) {
    patchState({ selectedRecipeId: payload });
  }

  @Action(UpdateRecipe)
  updateRecipe({getState, patchState, dispatch }: StateContext<RecipesStateModel>, { payload}: UpdateRecipe) {
    const state = getState();
    const index = state.recipes.findIndex(recp => recp.id === payload.id);
    state.recipes[index] = payload;
    return this.recipeService.updateRecipe(payload).pipe(
      map(() => {
        asapScheduler.schedule(() =>
          dispatch(new RemoveRecipeSuccess())
        );
        catchError(error =>
          of(
            asapScheduler.schedule(() =>
              dispatch(new UpdateRecipeFail(error))
            )
          )
        );
      })
    );
  }

  @Action(UpdateRecipeFail)
  updateRecipeFail(
    { dispatch }: StateContext<RecipesStateModel>,
  ) {
    dispatch({ loaded: false, loading: false });
    dispatch(new LoadRecipes());
  }



}
