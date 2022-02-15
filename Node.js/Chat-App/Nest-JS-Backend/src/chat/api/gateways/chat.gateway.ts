import {
  ConnectedSocket,
  MessageBody,
  OnGatewayConnection,
  OnGatewayDisconnect,
  SubscribeMessage,
  WebSocketGateway,
  WebSocketServer,
} from '@nestjs/websockets';
import { Socket } from 'socket.io';
import { WelcomeDto } from '../dtos/welcome.dto';
import { Inject } from '@nestjs/common';
import {
  IChatService,
  IChatServiceProvider,
} from '../../core/primary-ports/chat.service.interface';

@WebSocketGateway()
export class ChatGateway implements OnGatewayConnection, OnGatewayDisconnect {
  /**
   * ChatGateway constructor
   * @param chatService IChatService
   */
  constructor(
    @Inject(IChatServiceProvider) private chatService: IChatService,
  ) {}

  @WebSocketServer() server;

  @SubscribeMessage('newMessage')
  handleChatEvent(
    @MessageBody() receivedMessage: string,
    @ConnectedSocket() client: Socket,
  ): void {
    // Send to anyone who's listening for 'messages'
    const chatMessage = this.chatService.addMessage(receivedMessage, client.id);
    this.server.emit('newMessage', chatMessage);
  }

  @SubscribeMessage('username')
  handleUsernameEvent(
    @MessageBody() username: string,
    @ConnectedSocket() client: Socket,
  ): void {
    try {
      const chatClient = this.chatService.addClient(client.id, username);
      const welcomeDto: WelcomeDto = {
        clients: this.chatService.getClients(),
        messages: this.chatService.getAllMessages(),
        client: chatClient,
      };
      client.emit('welcome', welcomeDto);
      this.server.emit('clients', this.chatService.getClients());
    } catch (e) {
      // Send error package - something went wrong
      client.emit('error', e.message);
    }
  }

  @SubscribeMessage('isTyping')
  handleIsTyping(
    @MessageBody() isTyping: boolean,
    @ConnectedSocket() client: Socket,
  ): void {
    // Send chatClient that is typing
    const chatClient = this.chatService.updateIsTyping(isTyping, client.id);
    if (chatClient) {
      this.server.emit('clientIsTyping', chatClient);
    }
  }

  /**
   * Handles client connection
   * Sends all messages
   * @param client
   * @param args
   */
  handleConnection(client: Socket, ...args: any[]): any {
    client.emit('allMessages', this.chatService.getAllMessages());
    this.server.emit('clients', this.chatService.getClients());
  }

  /**
   * Handles client disconnection
   * @param client
   */
  handleDisconnect(client: Socket): any {
    this.chatService.deleteClient(client.id);
    this.server.emit('clients', this.chatService.getClients());
  }
}
