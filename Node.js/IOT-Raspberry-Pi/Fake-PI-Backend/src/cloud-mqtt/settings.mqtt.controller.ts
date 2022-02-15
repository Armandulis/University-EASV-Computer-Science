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
import { SettingsService } from '../services/settings.service';
import { Settings } from '../models/settings.schema';

@Controller()
export class SettingsMqttController implements OnGatewayInit {
  constructor(
    @Inject('MQTT_CLIENT') private client: ClientMqtt,
    private readonly service: SettingsService,
    private readonly socketService: SocketService,
  ) {}

  afterInit(server: Server) {
    this.socketService.server = server;
  }

  @MessagePattern('settings/changed')
  async settingsChanged(@Payload() data: any, @Ctx() context: MqttContext) {
    console.log('settings/changed');
    const settings: Settings = JSON.parse(data);
    console.log('settings/changed YES');
    this.socketService.server.emit(settings.sensorId, settings);
    return settings;
  }
}
