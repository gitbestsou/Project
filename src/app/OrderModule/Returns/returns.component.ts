import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import * as $ from "jquery";
import { GreatOutdoorsComponentBase } from '../../greatoutdoors1-component';
import { OrderDetail } from 'src/app/Models/order-detail';
import { Order } from 'src/app/Models/order';
import { Return } from 'src/app/Models/return';
import { ReturnDetail } from 'src/app/Models/return-detail';
import { OrderDetailsService } from '../../Services/order-details.service';
import { OrdersService } from '../../Services/orders.service';
import { error } from 'util';
import { ReturnService } from 'src/app/Services/returns.service';
import { ReturnDetailsService } from 'src/app/Services/return-detail.service';
import { GreatOutdoorsDataService } from 'src/app/InMemoryWebAPIService/greatoutdoors1-data.service';

/*
  
*
* Developer Name: Sourav Maji
  * Use Case - 1. Return a product from Orders which has been delivered
2. Cancel a product from Orders which has been placed but not deliverd
Creation Date - 10 / 10 / 2019
Last Modified Date - 12 / 10 / 2019

*/

/*The components required to render the template in browser*/ 
@Component({
  selector: 'app-returns',
  templateUrl: './returns.component.html',
  styleUrls: ['./returns.component.scss']

})



export class ReturnComponent extends GreatOutdoorsComponentBase implements OnInit {

  orders: Order[] = [];
  orderDetails: OrderDetail[] = [];
  returnDetails: ReturnDetail[] = [];
  returns: Return[] = [];
  showOrders: boolean = true;
  showOrderDetails: boolean = false;
  showReturnHistory: boolean = false;

  public orderDetailsTableDisplayController = 'normal';
  public indexComponent = null; //Variable to store the index of which order is being selected
  placeReturnForm: FormGroup;
  placeReturnDisabled: boolean = false;
  requestCancelForm: FormGroup;
  requestCancelDisabled: boolean = false;
  placeReturnFormErrorMessages: any;
  

  constructor(private ordersService: OrdersService, private orderDetailsService: OrderDetailsService, private returnService: ReturnService, private returnDetailsService: ReturnDetailsService) {
    super();
    this.placeReturnForm = new FormGroup({
      id: new FormControl(null),
      orderDetailID: new FormControl(null, [Validators.required]),
      orderID: new FormControl(null, [Validators.required]),
      productID: new FormControl(null, [Validators.required]),
      productName: new FormControl(null),
      quantity: new FormControl(null),
      unitPrice: new FormControl(null),
      totalPrice: new FormControl(null),
      status: new FormControl(null),
      reasonOfReturn: new FormControl("Incomplete")
      /*creationDateTime: new FormControl(null)*/
    });
    this.placeReturnFormErrorMessages = {
      /*reasonOfReturn: {required:"Reason Of Return has to be selected"}*/
    };
    this.requestCancelForm = new FormGroup({
      
      id: new FormControl(null),
      orderDetailID: new FormControl(null, [Validators.required]),
      orderID: new FormControl(null, [Validators.required]),
      productID: new FormControl(null, [Validators.required]),
      productName: new FormControl(null),
      quantity: new FormControl(null),
      unitPrice: new FormControl(null),
      totalPrice: new FormControl(null),
      status: new FormControl(null),
      creationDateTime: new FormControl(null)
    });
  }



  ngOnInit() {
    this.ordersService.GetAllOrders().subscribe((response) => {
      this.orders = response;

    }, (error) => {
      console.log(error);
    });


    /*This code is commented because we do not need the entire orderdetails table at once now,we need corresponding ordedetails of a particulaar OrderID*/
    /*this.orderDetailsService.GetAllOrderDetails().subscribe((response) => {
      this.orderDetails = response;

    }, (error) => {
      console.log(error);
      });*/

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


  /*This method will invoke when User will click the 'View Detail' button to view the corresponding products in a Order*/
  onViewDetailsClick(index) {
    this.showOrders = false;
    this.showReturnHistory = false;
    this.showOrderDetails = true;

    this.indexComponent = index;
    this.orderDetailsService.GetOrderDetailByOrderID(this.orders[index].orderID).subscribe((selectResponse) => {
      this.orderDetails = selectResponse;
      console.log(this.orderDetails);
    }, (error) => {
      console.log(error)
    });
    console.log(this.orderDetails);

  }

  goBackToOrders() {
    this.showOrderDetails = false;
    this.showReturnHistory = false;
    this.showOrders = true;
  }
  viewAllReturns() {
    console.log("ysysysysy ");
    this.showOrderDetails = false;
    this.showReturnHistory = true;
    this.showOrders = false;
  }

  /*This method will invoke when User initiate a return process for a particular product*/
  onPlaceReturnClick(index) {
    console.log(this.orderDetails[index].productName);
    this.placeReturnForm.reset();
    this.placeReturnForm["submitted"] = false;
    this.placeReturnForm.patchValue({
      id: this.orderDetails[index].id,
      orderDetailID: this.orderDetails[index].orderDetailID,
      orderID: this.orderDetails[index].orderID,
      productID: this.orderDetails[index].productID,
      productName: this.orderDetails[index].productName,
      quantity: this.orderDetails[index].quantity,
      unitPrice: this.orderDetails[index].unitPrice,
      totalPrice: this.orderDetails[index].totalPrice,
      status: this.orderDetails[index].status,
      creationDateTime: this.orderDetails[index].creationDateTime
    });
  }

  /*This method will invoke when User initiate a cancel process for a particular product*/
  onRequestCancelClick(index) {
    console.log();
    this.requestCancelForm.reset();
    this.requestCancelForm["submited"] = false;

    this.requestCancelForm.patchValue({
      id: this.orderDetails[index].id,
      orderDetailID: this.orderDetails[index].orderDetailID,
      orderID: this.orderDetails[index].orderID,
      productID: this.orderDetails[index].productID,
      productName: this.orderDetails[index].productName,
      quantity: this.orderDetails[index].quantity,
      unitPrice: this.orderDetails[index].unitPrice,
      totalPrice: this.orderDetails[index].totalPrice,
      status: this.orderDetails[index].status,
      creationDateTime: this.orderDetails[index].creationDateTime
    });
  }




  /*This method will invoke when User confirm the return of a particular product*/
  onPlaceReturnConfirmClick(event)
  {
    this.placeReturnForm["submitted"] = true;
    if (this.placeReturnForm.valid)
    {
      this.placeReturnDisabled = true;
      var returnDetail: ReturnDetail;
      returnDetail = this.placeReturnForm.value;
      console.log(returnDetail);
      this.returnDetailsService.AddReturnDetail(returnDetail).subscribe((someResponse) => {
        this.placeReturnForm.reset();
        $("#btnPlaceReturnCancel").trigger("click");
        this.placeReturnDisabled = false;

        this.returnDetailsService.GetAllReturnDetails().subscribe((getResponse) => {
          this.returnDetails = getResponse;
          console.log(this.returnDetails);
        }, (error) => {
            console.log(error);
          });
      },
        (error) => {
          console.log(error);
        });

      var orderDetail: OrderDetail = this.placeReturnForm.value;
      this.returnDetailsService.ReturnOrderDetail(orderDetail).subscribe((returnResponse) => {
        this.placeReturnForm.reset();
        $("#btnPlaceReturnCancel").trigger("click");
        this.placeReturnDisabled = false;

        this.orderDetailsService.GetOrderDetailByOrderID(this.orders[this.indexComponent].orderID).subscribe((getResponse) => {
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
    else
    {
      super.getFormGroupErrors(this.placeReturnForm);
    }
    
    
  }

  /*This method will invoke when User confirm the cancellation of a particular product*/
  onRequestCancelConfirmClick(event)
  {
    this.requestCancelForm["submitted"] = true;
    if (this.requestCancelForm.valid)
    {
      this.requestCancelDisabled = true;
      var orderDetail: OrderDetail = this.requestCancelForm.value;
      this.returnDetailsService.CancelOrderDetail(orderDetail).subscribe((cancelResponse) => {
        this.requestCancelForm.reset();
        $("#btnRequestCancelCancel").trigger("click");
        this.requestCancelDisabled = false;
        this.orderDetailsService.GetOrderDetailByOrderID(this.orders[this.indexComponent].orderID).subscribe((getResponse) => {
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
    else
    {
      super.getFormGroupErrors(this.requestCancelForm);
    }

  }



}
