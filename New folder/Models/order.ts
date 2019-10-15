
export class Order {
  id: number;
  orderID: string;
  retailerID: string;
  salespersonID: string;
  totalQuantity: number;
  totalAmount: number;
  channelOfOrder: string;
  creationDateTime: string;
  lastModifiedDateTime: string;

  constructor(ID: number, OrderID: string, RetailerID: string, SalespersonID: string, TotalQuantity: number, TotalAmount: number, ChannelOfOrder: string, CreationDateTime: string, LastModifiedDateTime: string) {
    this.id = ID;
    this.orderID = OrderID;
    this.retailerID = RetailerID;
    this.salespersonID = SalespersonID;
    this.totalQuantity = TotalQuantity;
    this.totalAmount = TotalAmount;
    this.channelOfOrder = ChannelOfOrder;
    this.creationDateTime = CreationDateTime;
    this.lastModifiedDateTime = LastModifiedDateTime;
  }
}
