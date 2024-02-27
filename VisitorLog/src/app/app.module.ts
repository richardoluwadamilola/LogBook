import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VisitorFormComponent } from './visitor-form/visitor-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MenuComponent } from './menu/menu.component';
import { AdminComponent } from './admin/admin.component';
import { EntryComponent } from './entry/entry.component';
import { TagManagementComponent } from './tag-management/tag-management.component';
import { EmployeeManagementComponent } from './employee-management/employee-management.component';
import { VisitorManagementComponent } from './visitor-management/visitor-management.component';
import { DatePipe } from '@angular/common';
import { AssignTagComponent } from './assign-tag/assign-tag.component';
import { HomeComponent } from './home/home.component';
import { DepartmentFormComponent } from './department-form/department-form.component';
import { LoginComponent } from './login/login.component';
import { SuperadminComponent } from './superadmin/superadmin.component';
import { CameraComponent } from './camera/camera.component';
import { WebcamModule } from 'ngx-webcam';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { LightboxModule } from 'ngx-lightbox';
import { ReasonforvisitComponent } from './reasonforvisit/reasonforvisit.component';
import { CheckOutTagComponent } from './check-out-tag/check-out-tag.component';


@NgModule({
  declarations: [
    AppComponent,
    VisitorFormComponent,
    MenuComponent,
    AdminComponent,
    EntryComponent,
    TagManagementComponent,
    EmployeeManagementComponent,
    VisitorManagementComponent,
    AssignTagComponent,
    HomeComponent,
    DepartmentFormComponent,
    LoginComponent,
    SuperadminComponent,
    CameraComponent,
    ChangePasswordComponent,
    ReasonforvisitComponent,
    CheckOutTagComponent,
  ],
  // ...

    imports: [
      ReactiveFormsModule,
      FormsModule,
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      WebcamModule,
      LightboxModule
      

    ],

  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }