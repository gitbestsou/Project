/*
  
*
* Developer Name: Sourav Maji
  * Use Case - 1. Return a product from Orders which has been delivered
2. Cancel a product from Orders which has been placed but not deliverd
Creation Date - 10 / 10 / 2019
Last Modified Date - 12 / 10 / 2019

*/



export class Return {
  id: number;
  returnID: string;
  orderID: string;
  channelOfReturn: string;
  returnAmount: number;
  creationDateTime: string;
  lastModifiedDateTime: string;

  constructor(ID: number, ReturnID: string, OrderID: string, ChannelOfReturn: string, ReturnAmount: number, CreationDateTime: string, LastModifiedDateTime: string) {
    this.id = ID;
    this.returnID = ReturnID;
    this.orderID = OrderID;
    this.channelOfReturn = ChannelOfReturn;
    this.returnAmount = ReturnAmount;
    this.creationDateTime = CreationDateTime;
    this.lastModifiedDateTime = LastModifiedDateTime;
  }
}
