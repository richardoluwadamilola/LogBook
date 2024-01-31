import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { WebcamImage, WebcamUtil } from 'ngx-webcam';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-camera',
  templateUrl: './camera.component.html',
  styleUrls: ['./camera.component.css']
})
export class CameraComponent {
  @ViewChild('webcam', { static: true }) webcamElement: ElementRef | undefined;

  @Output() photoCaptured: EventEmitter<string> = new EventEmitter<string>();

  public webcamImage: WebcamImage | undefined = undefined;
  public trigger: Subject<void> = new Subject<void>();
  
  public triggerSnapshot(): void {
    this.trigger.next();
  }

  public handleImage(webcamImage: WebcamImage): void {
    this.webcamImage = webcamImage;
    if (webcamImage) {
      this.photoCaptured.emit(webcamImage.imageAsBase64);
    }
  }

  public ngOnInit(): void {
    WebcamUtil.getAvailableVideoInputs()
      .then((mediaDevices: MediaDeviceInfo[]) => {
        if (mediaDevices && mediaDevices.length > 0) {
          const selectedDevice = mediaDevices[0];
          navigator.mediaDevices.getUserMedia({
            video: {
              deviceId: selectedDevice.deviceId
            }
          })
          .then((stream: MediaStream) => {
            if (this.webcamElement && this.webcamElement.nativeElement) {
              this.webcamElement.nativeElement.srcObject = stream;
            }
          })
          .catch((error) => {
            console.error('Error accessing the camera:', error);
          });
        }
      });
  }
}
