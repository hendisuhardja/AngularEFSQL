import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customer-view',
  templateUrl: './customer-view.component.html',
  styleUrls: ['./customer-view.component.css']
})
export class CustomerViewComponent implements OnInit {

  private readonly PAGE_SIZE = 10;
  queryResult: any = {};
  query:any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'First Name', key: 'firstName', isSortable:true },
    { title: 'Last Name', key: 'lastName', isSortable:true },
    { title: 'Address', key: 'address', isSortable:true },
    {}
  ];

  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.populateCustomers();
  }

  onFilterChange(){
    this.query.page = 1;
    this.populateCustomers();
  }
  private populateCustomers(){
    this.customerService.getCustomers(this.query)
      .subscribe(results => 
        { this.queryResult = results;
        });
  }
  resetFilter(){
    this.query ={
      page:1,
      pageSize:this.PAGE_SIZE
    };
    this.populateCustomers();
  }

  sortBy(columnName){
      if(this.query.sortBy !== columnName){
          this.query.sortBy = columnName;
      }
      this.query.isSortAscending = !this.query.isSortAscending;
      this.populateCustomers();
  }


}
