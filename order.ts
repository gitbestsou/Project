
export class Order {
  id: number;
  orderID: string;
  totalQuantity: number;
  totalAmount: number;
  addressID: string;
  channelOfOrder: string;
  creationDateTime: string;
  lastModifiedDateTime: string;

  constructor(ID: number, OrderID: string, TotalQuantity: number, TotalAmount: number, AddressID: string, ChannelOfOrder: string, CreationDateTime: string, LastModifiedDateTime: string) {
    this.id = ID;
    this.orderID = OrderID;
    this.totalQuantity = TotalQuantity;
    this.totalAmount = TotalAmount;
    this.addressID = AddressID;
    this.channelOfOrder = ChannelOfOrder;
    this.creationDateTime = CreationDateTime;
    this.lastModifiedDateTime = LastModifiedDateTime;
  }
}
