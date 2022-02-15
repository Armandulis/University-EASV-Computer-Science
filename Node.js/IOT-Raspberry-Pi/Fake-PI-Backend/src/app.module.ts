import { Module } from '@nestjs/common';
import { TemperatureController } from './controllers/temperature.controller';
import { MongooseModule } from '@nestjs/mongoose';
import { Humidity, HumiditySchema } from './models/humidity.schema';
import { Temperature, TemperatureSchema } from './models/temperature.schema';
import { TemperatureService } from './services/temperature.service';
import { HumidityService } from './services/humidity.service';
import { HumidityController } from './controllers/humidity.controller';
import { ClientsModule, Transport } from '@nestjs/microservices';
import { SocketService } from './services/socket.service';
import { ChatGateway } from './cloud-mqtt/chat.gateway';
import { TemperatureMqttController } from './cloud-mqtt/temperature.mqtt.controller';
import { HumidityMqttController } from './cloud-mqtt/humidity.mqtt.controller';
import { Settings, SettingsSchema } from './models/settings.schema';
import { SettingsMqttController } from './cloud-mqtt/settings.mqtt.controller';
import { SettingsService } from './services/settings.service';
import { SettingsController } from './controllers/settings.controller';

@Module({
  imports: [
    ClientsModule.register([
      {
        name: 'MQTT_CLIENT',
        transport: Transport.MQTT,
        options: {
          url: 'mqtts://driver.cloudmqtt.com:28692',
          username: 'guhpfdxf',
          password: 'jcceQ1eHsoAg',
        },
      },
    ]),
    MongooseModule.forRoot(
      'mongodb+srv://classroom:9Npt4YrmO6E1hxFt@cluster0.dg2lq.mongodb.net/pidata?retryWrites=true&w=majority',
    ),
    MongooseModule.forFeature([
      { name: Temperature.name, schema: TemperatureSchema },
      { name: Humidity.name, schema: HumiditySchema },
      { name: Settings.name, schema: SettingsSchema },
    ]),
  ],
  controllers: [
    TemperatureController,
    HumidityController,
    SettingsController,
    TemperatureMqttController,
    HumidityMqttController,
    SettingsMqttController,
  ],
  providers: [
    TemperatureService,
    HumidityService,
    SocketService,
    ChatGateway,
    SettingsService,
  ],
  exports: [SocketService],
})
export class AppModule {}
