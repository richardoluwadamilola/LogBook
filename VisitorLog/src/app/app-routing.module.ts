import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VisitorFormComponent } from './visitor-form/visitor-form.component';
import { AdminComponent } from './admin/admin.component';
import { EntryComponent } from './entry/entry.component';
import { TagManagementComponent } from './tag-management/tag-management.component';
import { EmployeeManagementComponent } from './employee-management/employee-management.component';
import { VisitorManagementComponent } from './visitor-management/visitor-management.component';
import { AssignTagComponent } from './assign-tag/assign-tag.component';
import { HomeComponent } from './home/home.component';
import { CheckoutVisitorComponent } from './checkout-visitor/checkout-visitor.component';
import { DepartmentFormComponent } from './department-form/department-form.component';
import { LoginComponent } from './login/login.component';
import { authGuard } from './Auth/auth.guard';
import { SuperadminComponent } from './superadmin/superadmin.component';


const routes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: 'home', component: HomeComponent},
  { path: 'visitor-form', component: VisitorFormComponent},
  {path: 'superadmin', component: SuperadminComponent},
  { path: 'login', component: LoginComponent},
  { path: 'admin', component: AdminComponent, canActivate: [authGuard]},
  { path: 'entry', component: EntryComponent, canActivate: [authGuard]},
  { path: 'tag', component: TagManagementComponent},
  { path: 'employee', component: EmployeeManagementComponent},
  { path: 'visitor', component: VisitorManagementComponent},
  { path: 'assign', component: AssignTagComponent},
  { path: 'checkout', component: CheckoutVisitorComponent},
  { path: 'department', component: DepartmentFormComponent},

  
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }