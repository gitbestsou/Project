import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import * as $ from "jquery";
import { GreatOutdoorsComponentBase } from '../../greatoutdoors-component';          
import { OrderDetail } from 'src/app/Models/order-detail';
import { Order } from 'src/app/Models/order';
import { Return } from 'src/app/Models/return';
import { ReturnDetail } from 'src/app/Models/return-detail';
import { OrderDetailsService } from '../../Services/order-details.service';
import { OrdersService } from '../../Services/orders.service';
import { error } from 'util';
import { ReturnService } from 'src/app/Services/returns.service';
import { ReturnDetailsService } from 'src/app/Services/return-detail.service';

@Component({
  selector: 'app-returns',
  templateUrl: './returns.component.html',
  styleUrls: ['./returns.component.scss']

})

export class ReturnComponent extends GreatOutdoorsComponentBase implements OnInit
{
  orders: Order[] = [];
  orderDetails: OrderDetail[] = [];

  returnObj = Return;
  returnDetail = ReturnDetail;

  placeReturnForm: FormGroup;
  placeReturnDisabled: boolean = false;
  placeReturnFormErrorMessages: any;

  requestCancelForm: FormGroup;
  requestCancelDisabled: boolean = false;
  requestCancelFormErrorMessages: any;


  constructor(private ordersService: OrdersService, private orderDetailsService: OrderDetailsService, private returnService: ReturnService, private returnDetailsService: ReturnDetailsService)
  {
    super();
    this.placeReturnForm = new FormGroup({
      id: new FormControl(null),
      /*returnID: new FormControl(null),*/
      orderDetailID: new FormControl(null),
      /*productID: new FormControl(null, [Validators.required]),*/
      productName: new FormControl(null),
      quantity: new FormControl(null),
      /*reasonOfReturn: new FormControl(null, [Validators.required]),*/
      unitPrice: new FormControl(null),
      totalPrice: new FormControl(null)
      
    });
    this.placeReturnFormErrorMessages = {
      /*reasonOfReturn: {required:"Reason Of Return has to be selected"}*/};

    this.requestCancelForm = new FormGroup({
      id: new FormControl(null),
      orderDetailID: new FormControl(null),
      productName: new FormControl(null),
      quantity: new FormControl(null),
      unitPrice: new FormControl(null),
      totalPrice: new FormControl(null),

    });
  }

  ngOnInit()
  {
    this.ordersService.GetAllOrders().subscribe((response) =>
    {
      this.orders = response;

    }, (error) => {
      console.log(error);
      });

    this.orderDetailsService.GetAllOrderDetails().subscribe((response) => {
      this.orderDetails = response;

    }, (error) => {
      console.log(error);
    });

  }


  getFormControlCssClass(formControl: FormControl, formGroup: FormGroup): any {
    return {
      'is-invalid': formControl.invalid && (formControl.dirty || formControl.touched || formGroup["submitted"]),
      'is-valid': formControl.valid && (formControl.dirty || formControl.touched || formGroup["submitted"])
    };
  }

  getFormControlErrorMessage(formControlName: string, validationProperty: string): string {
    return this.placeReturnFormErrorMessages[formControlName][validationProperty];
  }

  getCanShowFormControlErrorMessage(formControlName: string, validationProperty: string, formGroup: FormGroup): boolean {
    return formGroup.get(formControlName).invalid && (formGroup.get(formControlName).dirty || formGroup.get(formControlName).touched || formGroup['submitted']) && formGroup.get(formControlName).errors[validationProperty];
  }



  onPlaceReturnClick(index)
  {
    this.placeReturnForm.reset();
    this.placeReturnForm["submitted"] = false;
    this.placeReturnForm.patchValue({
      id: this.orderDetails[index].id,
      orderDetailID: this.orderDetails[index].orderDetailID,
      productName: this.orderDetails[index].productName,
      quantity: this.orderDetails[index].quantity,
      unitPrice: this.orderDetails[index].unitPrice,
      totalPrice: this.orderDetails[index].totalPrice,

    });
  }

  onRequestCancelClick(index) {
    this.requestCancelForm.reset();
    this.requestCancelForm["submited"] = false;

    this.requestCancelForm.patchValue({
      id: this.orderDetails[index].id,
      orderDetailID: this.orderDetails[index].orderDetailID,
      productName: this.orderDetails[index].productName,
      quantity: this.orderDetails[index].quantity,
      unitPrice: this.orderDetails[index].unitPrice,
      totalPrice: this.orderDetails[index].totalPrice
      
    });

    console.log(this.requestCancelForm.value);
  }


  onPlaceReturnConfirmClick(event) {
    this.placeReturnForm["submitted"] = true;
    if (this.placeReturnForm.valid) {
      this.placeReturnDisabled = true;
      var orderDetail: OrderDetail = this.placeReturnForm.value;

      this.orderDetailsService.ReturnOrderDetail(orderDetail).subscribe((returnResponse) => {
        this.placeReturnForm.reset();
        $("#btnPlaceReturnCancel").trigger("click");
        this.placeReturnDisabled = false;
        //this.showSuppliersSpinner = true;

        this.orderDetailsService.GetAllOrderDetails().subscribe((getResponse) => {
          //this.showSuppliersSpinner = false;
          this.orderDetails = getResponse;
        }, (error) => {
          console.log(error);
        });
      },
        (error) => {
          console.log(error);
          this.placeReturnDisabled = false;
        });
    }
    else {
      super.getFormGroupErrors(this.placeReturnForm);
    }



  }


    



  

  onRequestCancelConfirmClick(event) {

    this.requestCancelForm["submitted"] = true;
    if (this.requestCancelForm.valid) {
      this.requestCancelDisabled = true;
      var orderDetail: OrderDetail = this.requestCancelForm.value;

      this.orderDetailsService.CancelOrderDetail(orderDetail).subscribe((cancelResponse) => {
        this.requestCancelForm.reset();
        $("#btnRequestCancelCancel").trigger("click");
        this.requestCancelDisabled = false;
        //this.showSuppliersSpinner = true;

        this.orderDetailsService.GetAllOrderDetails().subscribe((getResponse) => {
          //this.showSuppliersSpinner = false;
          this.orderDetails = getResponse;
        }, (error) => {
            console.log(error);
          });
      },
        (error) => {
          console.log(error);
          this.requestCancelDisabled = false;
        });
    }
    else {
      super.getFormGroupErrors(this.requestCancelForm);
    }

     

  }


  
}
