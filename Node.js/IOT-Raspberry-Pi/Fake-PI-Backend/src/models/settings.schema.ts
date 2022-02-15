import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';

/**
 * Settings Model
 */
export type SettingsDocument = Settings & Document;

@Schema()
export class Settings {
  @Prop()
  sensorId: string;

  @Prop()
  maxTemperature: number;

  @Prop()
  minTemperature: number;

  @Prop()
  insertTime: Date;
}

export const SettingsSchema = SchemaFactory.createForClass(Settings);
