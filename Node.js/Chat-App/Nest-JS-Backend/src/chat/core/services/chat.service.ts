import { Injectable } from '@nestjs/common';
import { ChatClient } from '../models/chat-client.model';
import { ChatMessage } from '../models/chat-message.model';
import { IChatService } from '../primary-ports/chat.service.interface';

@Injectable()
export class ChatService implements IChatService{
  allMessages: ChatMessage[] = [];
  clients: ChatClient[] = [];

  /**
   * Adds a message to memory
   * @param receivedMessage string messages from user
   * @param clientId string client's id
   */
  public addMessage(receivedMessage: string, clientId: string) {

    const client = this.clients.find((c) => (c.id == clientId));
    const chatMessage: ChatMessage = {
      message: receivedMessage,
      sender: client,
    };
    this.allMessages.push(chatMessage);
    return chatMessage;
  }

  /**
   * Saves client
   * @param id id of the client
   * @param username user's name
   */
  public addClient(id: string, username: string): ChatClient {
    // Check if chat with such id and username already exists
    const chat = this.clients.find((c) => c.username == username && c.id == id);
    if (chat) {
      return chat;
    }

    if (this.clients.find((c) => c.username === username)) {
      throw new Error('Username already exists!');
    }

    const chatClient: ChatClient = {
      id: id,
      username: username,
      isTyping: false,
    };
    this.clients.push(chatClient);
    return chatClient;
  }

  /**
   * Returns all saved clients
   */
  public getClients(): ChatClient[] {
    return this.clients;
  }

  /**
   * Returns all saved messages
   */
  public getAllMessages(): ChatMessage[] {
    return this.allMessages;
  }

  /**
   * Removes client with certain id
   * @param id id of the client
   */
  deleteClient(id: string): void {
    this.clients = this.clients.filter((c) => c.id !== id);
  }

  updateIsTyping(isTyping: boolean, id: string): ChatClient {
    const chatClient = this.clients.find((c) => c.id === id);
    if (chatClient && chatClient.isTyping !== isTyping) {
      chatClient.isTyping = isTyping;
      return chatClient;
    }
  }
}
