import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { DataService } from '../core/data.service';
import { ICustomer } from '../shared/interfaces';
import { ValidationService } from '../shared/validation.service';

@Component({
  selector: 'customer-edit-reactive',
  templateUrl: './customer-edit-reactive.component.html'
})
export class CustomerEditReactiveComponent implements OnInit {
  
  customerForm: FormGroup = {} as FormGroup;
  get f(): { [key: string]: AbstractControl } {
    return this.customerForm.controls;
  }
  customer: ICustomer = {
    judul: '',
    jenisBarang: '',
    deskripsi: '',
    idPemilik: 0,
    bukaLelang: '',
    tutupLelang: ''
  };
  errorMessage = '';
  deleteMessageEnabled: boolean = false;
  operationText: string = 'Insert';
  
  constructor(private router: Router, 
              private route: ActivatedRoute, 
              private dataService: DataService,
              private formBuilder: FormBuilder) { 

                console.log('masuk')
              }

  
  ngOnInit() {
    let id : string = this.route.snapshot.params['id'];
    if (id !== '0') {
      this.operationText = 'Update';
      this.getCustomer(id);
      
    }
    this.buildForm();
  }

  getCustomer(id: string) {
      this.dataService.getCustomer(id)
        .subscribe((customer: ICustomer) => {
          this.customer = customer;
          this.buildForm();
        },
        (err) => console.log(err));
  }

  buildForm() {
      this.customerForm = this.formBuilder.group({
        judul:  this.customer.judul,
        jenisBarang:   this.customer.jenisBarang,
        deskripsi:     this.customer.deskripsi,
        idPemilik:    this.customer.idPemilik,
        bukaLelang:       this.customer.bukaLelang,
        tutupLelang:    this.customer.tutupLelang
        
      });
  }


  
  submit({ value, valid }: { value: ICustomer, valid: boolean }) {
      
      value.idBarang = this.customer.idBarang; 

      if (value.idBarang) {

        this.dataService.updateCustomer(value)
          .subscribe((customer: ICustomer) => {
            if (customer) {
              console.log("Berhasil Masuk")
              console.log(value)
              this.router.navigate(['/customers']);
            }
            else {
              this.errorMessage = 'Unable to save customer';
            }
          },
          (err) => console.log(err));

      } else {

        this.dataService.insertCustomer(value)
          .subscribe((customer: ICustomer) => {
            if (customer) {
              console.log(value)
              this.router.navigate(['/customers']);
            }
            else {
              this.errorMessage = 'Unable to add customer';
            }
          },
          (err) => this.router.navigate(['/customers']));
          
      }
  }
  
  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/customers']);
  }

  delete(event: Event) {
    event.preventDefault();
    this.dataService.deleteCustomer(this.customer.idBarang?.toString() as string)
        .subscribe((status: boolean) => {
          if (status) {
            this.router.navigate(['/customers']);
          }
          else {
            this.errorMessage = 'Unable to delete customer';
          }
        },
        (err) => console.log(err));
  }

}