import { WebSocketGateway, WebSocketServer } from '@nestjs/websockets';
import { SocketService } from '../services/socket.service';
import { Server } from 'socket.io';

@WebSocketGateway({ cors: true })
export class ChatGateway {
  @WebSocketServer() public server: Server;

  constructor(private socketService: SocketService) {}

  afterInit(server: Server) {
    this.socketService.server = server;
  }
}
