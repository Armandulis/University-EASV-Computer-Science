import { Controller, Inject } from '@nestjs/common';
import {
  ClientMqtt,
  Ctx,
  MessagePattern,
  MqttContext,
  Payload,
} from '@nestjs/microservices';
import { TemperatureService } from '../services/temperature.service';
import { Temperature } from '../models/temperature.schema';
import { SocketService } from '../services/socket.service';
import { OnGatewayInit } from '@nestjs/websockets';
import { Server } from 'socket.io';

@Controller()
export class TemperatureMqttController implements OnGatewayInit {
  constructor(
    @Inject('MQTT_CLIENT') private client: ClientMqtt,
    private readonly service: TemperatureService,
    private readonly socketService: SocketService,
  ) {}

  afterInit(server: Server) {
    this.socketService.server = server;
  }

  @MessagePattern('temperature/changed')
  async temperatureChanged(@Payload() data: any, @Ctx() context: MqttContext) {
    const sensorData: Temperature = JSON.parse(data);
    console.log('yes we received the message');
    this.socketService.server.emit(sensorData.sensorId, sensorData);
    return sensorData;
  }
}
