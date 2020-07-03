import { SaveInvoice } from './invoice-save';

export interface SaveCustomer {
    id: number;
    firstName: string;
    lastName: string;
    address: string;
    invoices: SaveInvoice[];
}
