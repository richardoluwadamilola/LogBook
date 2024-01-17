import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VisitorFormComponent } from './visitor-form/visitor-form.component';
import { AdminComponent } from './admin/admin.component';
import { EntryComponent } from './entry/entry.component';
import { TagManagementComponent } from './tag-management/tag-management.component';
import { EmployeeManagementComponent } from './employee-management/employee-management.component';
import { VisitorManagementComponent } from './visitor-management/visitor-management.component';


const routes: Routes = [
  { path: 'home', component: VisitorFormComponent},
  { path: 'admin', component: AdminComponent},
  { path: 'entry', component: EntryComponent},
  { path: 'tag', component: TagManagementComponent},
  { path: 'employee', component: EmployeeManagementComponent},
  { path: 'visitor', component: VisitorManagementComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
