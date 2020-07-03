import { Invoice } from './invoice';

export interface Customer {
    customerId: number;
    firstName: string;
    lastName: string;
    address: string;
    invoices: Invoice[];
}
