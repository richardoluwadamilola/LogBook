import { Employee } from "./employee.model";

export enum ReasonForVisit {
    Official = 0,
    Personal = 1
}

export interface Visitor {
    id: number;
    fullName: string;
    contactAddress: string;
    phoneNumber: string;
    reasonForVisit: ReasonForVisit;
    reasonForVisitEnum: ReasonForVisit;
    reasonForVisitDescription: string;
    photo: string;
    employeeNumber: string;
    employee: Employee;
    arrivalTime: Date;
    departureTime: Date;
    
}
