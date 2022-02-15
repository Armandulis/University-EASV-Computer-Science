import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';

/**
 * Humidity Model
 */

export type HumidityDocument = Humidity & Document;

@Schema()
export class Humidity {
  @Prop()
  sensorId: string;

  @Prop()
  value: number;

  @Prop()
  measurementTime: Date;
}

export const HumiditySchema = SchemaFactory.createForClass(Humidity);
