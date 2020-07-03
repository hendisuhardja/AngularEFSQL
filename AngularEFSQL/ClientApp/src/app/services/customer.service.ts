import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { SaveCustomer } from '../models/customer-save';
import { Customer } from '../models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private readonly customersEndpoint = '/api/customers';
  constructor(private http: HttpClient) { }

  create(customer: SaveCustomer) {
    return this.http.post(this.customersEndpoint, customer)
    .pipe(
      map(response => {
        return response;
      }),
      catchError(err => {
          return throwError(err);
      })
    );
  }

  update(customer: SaveCustomer){
    return this.http.put(this.customersEndpoint + '/' + customer.id, customer).pipe(
      map(response => {
        return response;
      }),
      catchError(err => {
          return throwError(err);
      })
    );
  }

  delete(id){
    return this.http.delete(this.customersEndpoint + '/' + id).pipe(
      map(response => {
        return response;
      }),
      catchError(err => {
          return throwError(err);
      })
    );
  }
getCustomer(id): Observable<Customer> {
  return this.http.get<Customer>(this.customersEndpoint + '/' + id, { observe: 'response'}).pipe(
    map(response => {
      return response.body;
    }),
    catchError(err => {
        return throwError(err);
    })
  );
}

getCustomers(filter): Observable<Customer[]> {
  return this.http.get<Customer[]>(this.customersEndpoint + '?' + this.toQueryString(filter), { observe: 'response'}).pipe(
    map(response => {
      return response.body;
    }),
    catchError(err => {
        return throwError(err);
    })
  );
}

toQueryString(obj){
  var parts = [];
  for(var property in obj){
    var value = obj[property];
    if(value != null && value != undefined){
      parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

  }
    return parts.join('&');
}
}
