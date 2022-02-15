import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';
import {ChatService} from './shared/chat.service';
import {Observable, Subject} from 'rxjs';
import {count, take, takeUntil} from 'rxjs/operators';
import {ChatClient} from './shared/chat-client.model';
import {ChatMessage} from './shared/chat-message.model';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
/**
 * Class ChatComponent
 */
export class ChatComponent implements OnInit, OnDestroy {

  private unsubscriber$ = new Subject();
  messageForm = new FormControl('');
  usernameForm = new FormControl('');
  messages: ChatMessage[] = [];
  clientsTyping: ChatClient[] = [];
  chatClient: ChatClient | undefined;
  error$: Observable<string> | undefined;
  onlineClients: ChatClient[] = [];

  /**
   * ChatComponent Constructor
   * @param chatService ChatService - helps to handle actions with messages
   */
  constructor(private chatService: ChatService) {
  }

  /**
   * ChatComponent initializer
   * Opens connections
   */
  ngOnInit(): void {
    this.messageForm.valueChanges.pipe(takeUntil(this.unsubscriber$)).subscribe((input) => {
      this.chatService.sendIsTyping(input.length > 0);
    });
    // Listen for all of the clients and errors
    this.error$ = this.chatService.listenForErrors();

    this.chatService.listenForClients().pipe(takeUntil(this.unsubscriber$)).subscribe(
      (onlineClients) => {
        this.onlineClients = onlineClients;
      }
    );


    // Get welcome package
    this.chatService.listenForWelcome().pipe(takeUntil(this.unsubscriber$)).subscribe(welcome => {
      this.messages = welcome.messages;
      this.chatClient = this.chatService.chatClient = welcome.client;
    });

    if (this.chatService.chatClient) {
      this.chatService.sendUsername(this.chatService.chatClient.username);
    }

    // Listen to new messages
    this.chatService.listenForMessages()
      .pipe(takeUntil(this.unsubscriber$))
      .subscribe(message => {
        this.messages.push(message);
        console.log(message.sender.username);
      });

    this.chatService.listenForClientTyping().pipe(takeUntil(this.unsubscriber$)).subscribe(
      (chatClient) => {
        if (chatClient.isTyping && !this.clientsTyping.find((c) => c.id === chatClient.id)) {
          this.clientsTyping.push(chatClient);
        } else {
          this.clientsTyping = this.clientsTyping.filter((c) => c.id !== chatClient.id);
        }
      }
    );

    // Connect if we were disconnected
    // this.chatService.connect();
  }

  /**
   * ChatComponent Destroy
   * Closes connections to avoid memory leaks
   */
  ngOnDestroy(): void {
    this.unsubscriber$.next();
    this.unsubscriber$.complete();


    // Disconnect if we want
    // this.chatService.disconnect();
  }

  /**
   * Sends message to the back end
   */
  sendMessage(): void {
    // Make sure we don't send empty messages
    if (this.messageForm.value) {
      this.chatService.sendMessage(this.messageForm.value);
      this.messageForm.patchValue('');
    }
  }

  /**
   * Set user's name
   */
  sendUsername(): void {
    // Make sure we don't send empty username
    if (this.usernameForm.value) {
      this.chatService.sendUsername(this.usernameForm.value);
    }
  }
}
