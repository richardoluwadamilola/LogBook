import { Employee } from "./employee.model";


export interface Visitor {
    id: number;
    fullName: string;
    contactAddress: string;
    phoneNumber: string;
    emailAddress: string;
    reasonForVisit: string;
    reasonForVisitDescription: string;
    photo: string;
    employeeNumber: string;
    employeeName: string;
    employee: Employee;
    arrivalTime: Date;
    departureTime: Date;
    tagAssignedDateTime: Date;
    
}
