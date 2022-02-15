import {Injectable} from '@angular/core';
import {Socket} from 'ngx-socket-io';
import {Observable} from 'rxjs';
import {ChatClient} from './chat-client.model';
import {ChatMessage} from './chat-message.model';
import {WelcomeDto} from './welcome.dto';

@Injectable({
  providedIn: 'root'
})

/**
 * Class ChatService
 */
export class ChatService {

  chatClient: ChatClient | undefined;
  /**
   * ChatService constructor
   * @param socket Socket - a tool to send messages to the backend. (ngx-socket-io)
   */
  constructor(private socket: Socket) {
  }

  /**
   * Sends a message to the back end
   * @param message String that user provided
   */
  public sendMessage(message: string): void {
    this.socket.emit('newMessage', message);
  }

  /**
   * Listens for messages
   * @return Observable<string> messages
   */
  public listenForMessages(): Observable<ChatMessage> {
    return this.socket.fromEvent<ChatMessage>('newMessage');
  }

  /**
   * Listens for welcome package
   * @return Observable<WelcomeDto> WelcomeDto
   */
  public listenForWelcome(): Observable<WelcomeDto> {
    return this.socket.fromEvent<WelcomeDto>('welcome');
  }

  /**
   * Listens for errors that might appear
   * @return Observable<string> error message
   */
  public listenForErrors(): Observable<string> {
    return this.socket.fromEvent<string>('error');
  }

  /**
   * Listens for any clients typing
   * @return Observable<ChatClient> ChatClient that is typing
   */
  public listenForClientTyping(): Observable<ChatClient> {
    return this.socket.fromEvent<ChatClient>('clientIsTyping');
  }

  /**
   * Gets all available messages
   * @return Observable<string[]> messages
   */
  public getAllMessages(): Observable<ChatMessage[]> {
    return this.socket.fromEvent<ChatMessage[]>('allMessages');
  }

  /**
   * Disconnects from socket
   */
  public disconnect(): void {
    this.socket.disconnect();
  }

  /**
   * Connects to socket
   */
  public connect(): void {
    this.socket.connect();
  }

  /**
   * Saves user's name
   * @param username input user provided
   */
  public sendUsername(username: string): void {
    this.socket.emit('username', username);
  }

  /**
   * Gets all of the client names
   */
  public listenForClients(): Observable<ChatClient[]> {
    return this.socket.fromEvent<ChatClient[]>('clients');
  }

  sendIsTyping( isTyping: boolean): void {
    this.socket.emit( 'isTyping', isTyping );
  }
}
