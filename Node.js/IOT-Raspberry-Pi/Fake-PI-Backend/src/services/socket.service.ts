import { Injectable } from '@nestjs/common';
import { Server } from 'socket.io';

@Injectable()
export class SocketService {
  // To make sure we use server after it is initialized, not before
  public server: Server = null;
}
