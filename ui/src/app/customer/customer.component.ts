import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomerService } from '../services/customer.service';
import { CustomerModel } from '../models/customer.model';

@Component({
  selector: 'app-job',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  customerForm: FormGroup;

  public customers: CustomerModel[] = [];
  public newCustomer: CustomerModel = {
    customerId: null,
    name: null,
    type: null
  };

  public types = ['Large', 'Small'];

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService) { }

  ngOnInit() {
    this.customerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(5)]],
      type: ['', [Validators.required]]
    });

    this.customerService.GetCustomers().subscribe(customers => this.customers = customers);
  }

  public createCustomer(): void {
    if (this.customerForm.invalid) {
      alert('form is not valid');
    } else {
      this.newCustomer = this.customerForm.value;
      this.customerService.CreateCustomer(this.newCustomer).then(() => {
        this.customerService.GetCustomers().subscribe(customers => this.customers = customers);
      });
    }
  }
}
