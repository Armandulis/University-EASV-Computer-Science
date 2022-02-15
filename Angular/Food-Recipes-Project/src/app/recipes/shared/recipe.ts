import {Ingredient} from '../../shared/models/ingredient';

export interface Recipe {
  id?: string;
  type: string;
  name: string;
  portion: string;
  howTo: string;
  score?: number;
  cookTime: string;
  ingredients?: Ingredient[];

  picture?: string;
  url?: string;
}


