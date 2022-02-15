import { Inject, Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { ClientMqtt } from '@nestjs/microservices';
import { Model } from 'mongoose';
import { Settings, SettingsDocument } from '../models/settings.schema';

@Injectable()
export class SettingsService {
  constructor(
    @Inject('MQTT_CLIENT') private client: ClientMqtt,
    @InjectModel(Settings.name)
    private settingsModel: Model<SettingsDocument>,
  ) {}

  ////////////////////////////////////
  /////////// USING MQTTT ////////////
  ////////////////////////////////////

  /**
   * Send save new settings and send a message to broker
   * @param settings
   */
  sendNewSetting(settings: Settings) {
    console.log(settings);
    this.saveSettings(settings).finally(() => {
      console.log('saved data');
    });
    this.client.emit('settings/changed', JSON.stringify(settings));
    return '';
  }

  ////////////////////////////////////
  ///////// USING DATABASE ///////////
  ////////////////////////////////////

  /**
   * Save new sensor settings
   * @param data
   */
  async saveSettings(data: Settings) {
    const model = new this.settingsModel(data);
    return model.save();
  }

  /**
   * Get latest settings
   */
  async getLatest(): Promise<Settings> {
    return this.settingsModel.findOne().sort({ insertTime: -1 }).exec();
  }
}
