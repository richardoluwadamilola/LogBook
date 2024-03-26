import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'; // Import NgbModal
import { MessagesmodalComponent } from '../messagesmodal/messagesmodal.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private modalService: NgbModal) { }

  async showDialog(title: string, body: string): Promise<void> {
    const modalRef = this.modalService.open(MessagesmodalComponent, { centered: true });
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.body = body;

    return new Promise<void>((resolve, reject) => {
      modalRef.componentInstance.confirmAction.subscribe(() => {
        modalRef.close();
        resolve();
      });
    });
  } 
}
