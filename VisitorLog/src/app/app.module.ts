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
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';


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


  ],
  // ...

    imports: [
      BrowserModule,
      AppRoutingModule, 
      ReactiveFormsModule,
      HttpClientModule,
      FormsModule,
      MatAutocompleteModule,
      MatFormFieldModule,

    ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
