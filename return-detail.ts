/*
  
*
* Developer Name: Sourav Maji
  * Use Case - 1. Return a product from Orders which has been delivered
2. Cancel a product from Orders which has been placed but not deliverd
Creation Date - 10 / 10 / 2019
Last Modified Date - 12 / 10 / 2019

*/




export enum ReturnReasons {
  Incomplete, Wrong
}

export class ReturnDetail {
  id: number;
  returnDetailID: string;
  orderDetailID: string;
  returnID: string;
  productID: string;
  productName: string;
  quantity: number;
  reasonOfReturn: ReturnReasons;
  unitPrice: number;
  totalPrice: number;
  creationDateTime: string;
  lastModifiedDateTime: string;

  constructor(ID: number, ReturnDetailID: string,OrderDetailID:string, ReturnID: string, ProductID: string, ProductName: string, Quantity: number, ReasonOfReturn: ReturnReasons, UnitPrice: number, TotalPrice: number, CreationDateTime: string, LastModifiedDateTime: string) {
    this.id = ID;
    this.returnDetailID = ReturnDetailID;
    this.orderDetailID = OrderDetailID;
    this.returnID = ReturnID;
    this.productID = ProductID;
    this.productName = ProductName;
    this.quantity = Quantity;
    this.reasonOfReturn = ReasonOfReturn;
    this.unitPrice = UnitPrice;
    this.totalPrice = TotalPrice;
    this.creationDateTime = CreationDateTime;
    this.lastModifiedDateTime = LastModifiedDateTime;
  }
}
