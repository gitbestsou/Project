export enum ReturnReasons {
  Incomplete, Wrong
}

export class ReturnDetail {
  id: number;
  returnDetailID: string;
  returnID: string;
  productID: string;
  productName: string;
  quantity: number;
  reasonOfReturn: ReturnReasons;
  unitPrice: number;
  totalPrice: number;
  creationDateTime: string;
  lastModifiedDateTime: string;

  constructor(ID: number, ReturnDetailID: string, ReturnID: string, ProductID: string, ProductName: string, Quantity: number, ReasonOfReturn: ReturnReasons, UnitPrice: number,TotalPrice: number, CreationDateTime: string, LastModifiedDateTime: string) {
    this.id = ID;
    this.returnDetailID = ReturnDetailID;
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
