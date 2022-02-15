import { Inject, Injectable } from '@nestjs/common';
import { Temperature, TemperatureDocument } from '../models/temperature.schema';
import { Model } from 'mongoose';
import { InjectModel } from '@nestjs/mongoose';
import { ClientMqtt } from '@nestjs/microservices';
import { Humidity } from '../models/humidity.schema';

@Injectable()
export class TemperatureService {
  constructor(
    @Inject('MQTT_CLIENT') private client: ClientMqtt,
    @InjectModel(Temperature.name)
    private temperatureModel: Model<TemperatureDocument>,
  ) {}

  ////////////////////////////////////
  /////////// USING MQTTT ////////////
  ////////////////////////////////////

  sendSensorData(type: string, sensorData: Temperature) {
    console.log('we reached this');

    // Save sensor's data
    this.saveSensorData(sensorData).finally(() => {
      console.log('saved data');
    });

    // Emit message about temperature change
    this.client.emit('temperature/changed', JSON.stringify(sensorData));
    return '';
  }

  ////////////////////////////////////
  ///////// USING DATABASE ///////////
  ////////////////////////////////////

  /**
   * Get Temperature values for a specific sensor
   * @param sensorId
   */
  async getSensorData(sensorId: string): Promise<Temperature[]> {
    return this.temperatureModel.find({ sensorId: sensorId }).exec();
  }

  /**
   * Get Latest temperature values
   */
  async getLatest(): Promise<Humidity> {
    return this.temperatureModel.findOne().sort({ measurementTime: -1 }).exec();
  }

  /**
   * Save new temperature sensor data
   * @param data
   */
  async saveSensorData(data: Temperature) {
    const model = new this.temperatureModel(data);
    return model.save();
  }
}
