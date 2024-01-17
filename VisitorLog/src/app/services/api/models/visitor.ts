import { Employee } from "./employee.model";

export enum ReasonForVisit {
    Official = 0,
    Personal = 1
}

export interface Visitor {
    id: number;
    firstName: string;
    middleName: string;
    lastName: string;
    contactAddress: string;
    phoneNumber: string;
    reasonForVisit: number;
    reasonForVisitEnum: ReasonForVisit;
    reasonForVisitDescription: string;
    photo: string;
    employeeNumber: string;
    employee: Employee;
    arrivalTime: Date;
    departureTime: Date;
}
