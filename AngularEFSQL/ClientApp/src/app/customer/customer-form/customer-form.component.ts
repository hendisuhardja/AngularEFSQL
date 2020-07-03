import { Component, OnInit } from '@angular/core';
import { SaveCustomer } from 'src/app/models/customer-save';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from 'src/app/services/customer.service';
import { Customer } from 'src/app/models/customer';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit {
  customer: SaveCustomer = {
    id:0,
    address:'',
    firstName:'',
    lastName:'',
    invoices:[]

  };
  constructor(
    private route: ActivatedRoute,
    private router:Router,
    private customerService:CustomerService) { 

      route.params.subscribe(p =>
        {
          this.customer.id = +p['id'] || 0;
        });
    }

  ngOnInit() {
   if(this.customer.id)
   this.customerService.getCustomer(this.customer.id).subscribe(data => {
    if(this.customer.id){
      this.setCustomer(data);
    }
   }, err =>{
    if(err.status == 404){
        this.router.navigate(['/']);
      }
}
   );

    
  }

  private setCustomer(c: Customer){
    this.customer.id = c.customerId;
    this.customer.firstName = c.firstName;
    this.customer.lastName = c.lastName;
    this.customer.address = c.address;
    this.customer.invoices = [];
  }


  submit() {
    var result$ = (this.customer.id) ? this.customerService.update(this.customer) : this.customerService.create(this.customer); 
    result$.subscribe(customer => {
      alert('Data was successfully saved.');
      this.router.navigate(['/customer']);
    });
  }
  delete(){
    if(confirm("Are you sure?")){
      this.customerService.delete(this.customer.id).subscribe(x => 
        {
            this.router.navigate(["/customer"]);
        });
    }
  }
}
