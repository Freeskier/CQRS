import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { routes } from './routes';

// Material
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDividerModule } from '@angular/material/divider';
import { FileUploadModule } from 'ng2-file-upload';

import { ToolbarComponent } from './toolbar/toolbar.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { httpInterceptor } from './_interceptors/http.interceptor';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { HomeComponent } from './home/home.component';
import { ConfirmDialogComponent } from './dialogs/confirm-dialog/confirm-dialog.component';
import { InfoDialogComponent } from './dialogs/info-dialog/info-dialog.component';
import { PostCardComponent } from './post-card/post-card.component';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { PhotoService } from './_services/photo.service';
import { AccountDetailsResolver } from './_resolvers/account-details.resolver';
import { AnonymousGuard } from './_guards/anonymous.guard';
import { FollowService } from './_services/follow.service';
import { UserListDialogComponent } from './dialogs/user-list-dialog/user-list-dialog.component';
import { PostPreviewDialogComponent } from './dialogs/post-preview-dialog/post-preview-dialog.component';
@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    ConfirmDialogComponent,
    InfoDialogComponent,
    PostCardComponent,
    AccountDetailsComponent,
    UserListDialogComponent,    
    PostPreviewDialogComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' }),
    HttpClientModule,
    FileUploadModule,
    MatNativeDateModule,
    MatSnackBarModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatIconModule,
    MatAutocompleteModule,
    MatDividerModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatGridListModule,
    MatCardModule,
    MatProgressBarModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    AuthGuard,
    AnonymousGuard,
    UserService,
    PhotoService,
    FollowService,
    AccountDetailsResolver
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
