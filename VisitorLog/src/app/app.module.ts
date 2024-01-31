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
import { CheckoutVisitorComponent } from './checkout-visitor/checkout-visitor.component';
import { DepartmentFormComponent } from './department-form/department-form.component';
import { LoginComponent } from './login/login.component';
import { SuperadminComponent } from './superadmin/superadmin.component';


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
    CheckoutVisitorComponent,
    DepartmentFormComponent,
    LoginComponent,
    SuperadminComponent,
  ],
  // ...

    imports: [
      ReactiveFormsModule,
      FormsModule,
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      

    ],

  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }