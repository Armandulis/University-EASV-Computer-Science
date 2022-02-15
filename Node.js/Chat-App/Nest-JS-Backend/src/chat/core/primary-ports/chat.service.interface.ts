import { ChatMessage } from '../models/chat-message.model';
import { ChatClient } from '../models/chat-client.model';

export const IChatServiceProvider = 'IChatServiceProvider';
export interface IChatService {
  /**
   * Adds a message to memory
   * @param receivedMessage
   * @param clientId
   */
  addMessage(receivedMessage: string, clientId: string): ChatMessage;

  /**
   * Saves client
   * @param id id of the client
   * @param username user's name
   */
  addClient(id: string, username: string): ChatClient;

  /**
   * Returns all saved clients
   */
  getClients(): ChatClient[];

  /**
   * Returns all saved messages
   */
  getAllMessages(): ChatMessage[];

  /**
   * Removes client with certain id
   * @param id id of the client
   */
  deleteClient(id: string): void;

  /**
   * Update client to typing status
   * @param isTyping
   * @param id
   */
  updateIsTyping(isTyping: boolean, id: string): ChatClient;
}
