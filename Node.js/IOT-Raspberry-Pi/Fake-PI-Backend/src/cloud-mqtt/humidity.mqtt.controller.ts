import { Controller, Inject } from '@nestjs/common';
import {
  ClientMqtt,
  Ctx,
  MessagePattern,
  MqttContext,
  Payload,
} from '@nestjs/microservices';
import { SocketService } from '../services/socket.service';
import {
  ConnectedSocket,
  OnGatewayInit,
  WebSocketServer,
} from '@nestjs/websockets';
import { Server, Socket } from 'socket.io';
import { HumidityService } from '../services/humidity.service';
import { Humidity } from '../models/humidity.schema';

@Controller()
export class HumidityMqttController implements OnGatewayInit {
  constructor(
    @Inject('MQTT_CLIENT') private client: ClientMqtt,
    private readonly service: HumidityService,
    private readonly socketService: SocketService,
  ) {}

  afterInit(server: Server) {
    this.socketService.server = server;
  }

  @MessagePattern('humidity/changed')
  async humidityChanged(@Payload() data: any, @Ctx() context: MqttContext) {
    console.log('HELOOO');
    const sensorData: Humidity = JSON.parse(data);
    console.log('yes we received the message');
    this.socketService.server.emit(sensorData.sensorId, sensorData);
    return sensorData;
  }
}
