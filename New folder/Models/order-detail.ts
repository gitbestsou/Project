

export class OrderDetail {
  id: number;
  orderDetailID: string;
  orderID: string;
  productID: string;
  productName: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  status: string;
  addressID: string;
  creationDateTime: string;
  lastModifiedDateTime: string;

  constructor(ID: number, OrderDetailID: string, OrderID: string, ProductID: string, ProductName: string, Quantity: number, UnitPrice: number, TotalPrice: number,Status:string, AddressID: string, CreationDateTime: string, LastModifiedDateTime: string) {
    this.id = ID;
    this.orderDetailID = OrderDetailID;
    this.orderID = OrderID;
    this.productID = ProductID;
    this.productName = ProductName;
    this.quantity = Quantity;
    this.unitPrice = UnitPrice;
    this.totalPrice = TotalPrice;
    this.status = Status;
    this.addressID = AddressID;
    this.creationDateTime = CreationDateTime;
    this.lastModifiedDateTime = LastModifiedDateTime;
  }
}
