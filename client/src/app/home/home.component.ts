import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { UserDetailResponse } from '../_models/userDetailResponse';
import { FileItem, FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { DialogHelperService } from '../_helpers/dialogHelper.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PhotoResponse } from '../_models/photoResponse';
import { PhotoService } from '../_services/photo.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  postText = new FormControl('', [Validators.maxLength(400)]);
  loggedUser: UserDetailResponse;
  selectedFile: any;
  imagePreview: any;
  isUploading: boolean = false;
  photosOfFollowees: PhotoResponse[];

  apiUrl = environment.api + 'photo';
  uploader: FileUploader;

  @ViewChild('imageInput')
  imageInput: ElementRef;

  constructor(
    private snackBar: MatSnackBar,
    private photoService: PhotoService
  ) {}

  ngOnInit(): void {
    this.photosOfFollowees = [];
    this.loggedUser = JSON.parse(localStorage.getItem('dumplingram-user-info'));
    this.initializeUploader();
    this.getPhotosOfFollowees();
  }

  private initializeUploader() {
    this.uploader = new FileUploader({
      url: this.apiUrl,
      authToken: 'Bearer ' + localStorage.getItem('dumplingram-token-info'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onBeforeUploadItem = () => {
      this.isUploading = true;
      this.uploader.options.additionalParameter = {
        description: this.postText.value,
      };
    };

    this.uploader.onCompleteAll = () => {
      this.isUploading = false;
      this.clearAll();
      this.postText.setValue(null);
      this.snackBar.open('Thanks for sharing!', '', {
        duration: 1000,
        horizontalPosition: 'end',
        verticalPosition: 'top',
      });
    };

    this.uploader.onAfterAddingFile = (fileItem: FileItem) => {
      if (this.uploader.queue.length > 1) {
        this.uploader.removeFromQueue(this.uploader.queue[0]);
      }
    };
  }

  private getPhotosOfFollowees() {
    this.photoService.getPhotosOfFollowees().subscribe(
      (data) => {
        this.photosOfFollowees = data;
      },
      (error) => {
        this.snackBar.open(error, '', {
          duration: 1000,
        });
      }
    );
  }

  private readFileUrl() {
    const reader = new FileReader();
    reader.readAsDataURL(this.selectedFile);
    reader.onload = (_event) => {
      this.imagePreview = reader.result;
    };
  }

  private resetFileInput() {
    this.imageInput.nativeElement.value = '';
  }

  uploadFile() {
    const firstItem = this.uploader.queue[0];

    if (firstItem) {
      this.uploader.uploadItem(firstItem);
    }
  }

  getErrorMessage() {
    if (this.postText.hasError('maxlength')) {
      return 'Input must be max 400 length.';
    }
  }

  imageInputChange(fileInputEvent: any) {
    this.selectedFile =
      fileInputEvent.target.files[fileInputEvent.target.files.length - 1];

    this.resetFileInput();

    this.readFileUrl();
  }

  clearAll() {
    this.resetFileInput();
    this.uploader.clearQueue();
  }

  getFileName(name: string) {
    const length = name.length;
    if (length >= 40) {
      return `...${name.substring(length - 40, length)}`;
    } else {
      return name;
    }
  }
}
