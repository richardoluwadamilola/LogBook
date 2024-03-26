import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-messagesmodal',
  templateUrl: './messagesmodal.component.html',
  styleUrls: ['./messagesmodal.component.css']
})
export class MessagesmodalComponent {
  @Input() title: string = '';
  @Input() body: string = '';
  @Output() confirmAction: EventEmitter<void> = new EventEmitter<void>();

  constructor() { }

  confirm(): void {
    this.confirmAction.emit();
  }

}
