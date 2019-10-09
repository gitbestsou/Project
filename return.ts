export enum ReturnChannel {
  Offline,
  Online
}
export class Return {

  id: number;
  returnID: string;
  orderID: string;
  channelOfReturn: ReturnChannel;
  returnAmount: number;
  creationDateTime: string;
  lastModifiedDateTime: string;

  constructor(ID: number, ReturnID: string,OrderID:string, ChannelOfReturn: ReturnChannel, ReturnAmount: number, CreationDateTime: string, LastModifiedDateTime: string) {
    this.id = ID;
    this.returnID = ReturnID;
    this.orderID = OrderID;
    this.channelOfReturn = ChannelOfReturn;
    this.returnAmount = ReturnAmount;
    this.creationDateTime = CreationDateTime;
    this.lastModifiedDateTime = LastModifiedDateTime;
  }
}


