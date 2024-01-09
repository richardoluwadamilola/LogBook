import { Employee } from "./employee.model";

export enum ReasonForVisit {
    Official = 'Official',
    Personal = 'Personal'
}

export interface Visitor {
    id: number;
    firstName: string;
    middleName: string;
    lastName: string;
    contactAddress: string;
    phoneNumber: string;
    personHereToSee: string;
    reasonForVisit: ReasonForVisit;
    reasonForVisitDescription: string;
    photo: string;
    employeeId: number;
    employee: Employee;
}
