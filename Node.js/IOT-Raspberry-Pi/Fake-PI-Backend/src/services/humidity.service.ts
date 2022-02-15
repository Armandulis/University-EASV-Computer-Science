import { Inject, Injectable } from '@nestjs/common';
import { Temperature } from '../models/temperature.schema';
import { InjectModel } from '@nestjs/mongoose';
import { Humidity, HumidityDocument } from '../models/humidity.schema';
import { ClientMqtt } from '@nestjs/microservices';
import { Model } from 'mongoose';

@Injectable()
export class HumidityService {
  constructor(
    @Inject('MQTT_CLIENT') private client: ClientMqtt,
    @InjectModel(Humidity.name)
    private humidityModel: Model<HumidityDocument>,
  ) {}

  ////////////////////////////////////
  /////////// USING MQTTT ////////////
  ////////////////////////////////////

  /**
   * Save bew humidity data and send a message to the broker
   * @param type
   * @param sensorData
   */
  sendSensorData(type: string, sensorData: Temperature) {
    this.saveSensorData(sensorData).finally(() => {
      console.log('saved data');
    });
    this.client.emit('humidity/changed', JSON.stringify(sensorData));
    return '';
  }

  ////////////////////////////////////
  ///////// USING DATABASE ///////////
  ////////////////////////////////////

  /**
   * Saves new humidity's sensor's data
   * @param data
   */
  async saveSensorData(data: Humidity) {
    const model = new this.humidityModel(data);
    return model.save();
  }

  /**
   * Get all specific sensor's data
   * @param sensorId
   */
  async getSensorData(sensorId: string): Promise<Humidity[]> {
    return this.humidityModel.find({ sensorId: sensorId }).exec();
  }

  /**
   * Get latest humidity sensor's data
   */
  async getLatest(): Promise<Humidity> {
    return this.humidityModel.findOne().sort({ measurementTime: -1 }).exec();
  }
}
