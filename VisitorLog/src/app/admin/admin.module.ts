import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { AdminComponent } from "./admin.component";
import { TagManagementComponent } from "../tag-management/tag-management.component";
import { EmployeeManagementComponent } from "../employee-management/employee-management.component";
import { GetVisitorslistComponent } from "../get-visitorslist/get-visitorslist.component";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";

@NgModule({
    declarations:[
        AdminComponent,
        TagManagementComponent,
        EmployeeManagementComponent,
        GetVisitorslistComponent
    ],
    imports:[
        CommonModule,
        RouterModule,
        ReactiveFormsModule
    ],
    exports:[AdminComponent],
})
export class AdminModule{}