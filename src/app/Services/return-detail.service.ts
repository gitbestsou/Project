import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OrderDetail } from '../Models/order-detail';
import { ReturnDetail } from '../Models/return-detail';

@Injectable({
  providedIn: 'root'
})
export class ReturnDetailsService {
  constructor(private httpClient: HttpClient) {
  }
  AddReturnDetail(returndetail: ReturnDetail): Observable<boolean> {
    
    returndetail.creationDateTime = new Date().toLocaleDateString();
    returndetail.lastModifiedDateTime = new Date().toLocaleDateString();
    returndetail.returnDetailID = this.uuidv4();
    
    return this.httpClient.post<boolean>(`/api/returndetails`, returndetail);
  }

  UpdateReturnDetail(returndetail: ReturnDetail): Observable<boolean> {
    returndetail.lastModifiedDateTime = new Date().toLocaleDateString();
    return this.httpClient.put<boolean>(`/api/returndetails`, returndetail);
  }

  DeleteReturnDetail(returnDetailID: string, id: number): Observable<boolean> {
    return this.httpClient.delete<boolean>(`/api/returndetails/${id}`);
  }

  GetAllReturnDetails(): Observable<ReturnDetail[]> {
    return this.httpClient.get<ReturnDetail[]>(`/api/returndetails`);
  }

  GetReturnDetailByReturnDetailID(ReturnDetailID: number): Observable<ReturnDetail> {
    return this.httpClient.get<ReturnDetail>(`/api/returndetails?returndetailID=${ReturnDetailID}`);
  }

  GetReturnDetailByReturnID(ReturnID: number): Observable<ReturnDetail> {
    return this.httpClient.get<ReturnDetail>(`/api/returndetails?returnID=${ReturnID}`);
  }

  GetReturnDetailByOrderDetailID(OrderDetailID: number): Observable<ReturnDetail> {
    return this.httpClient.get<ReturnDetail>(`/api/returndetails?returndetailID=${OrderDetailID}`);
  }


  /*The business logic necessary for updating the status of a order when it is returned*/
  ReturnOrderDetail(orderdetail: OrderDetail): Observable<boolean> {
    console.log(orderdetail.status);
    if (orderdetail.status == "Cancelled") {
      alert("This product has been cancelled");
    }
    else {
      orderdetail.status = "Returned";
    }
    console.log(orderdetail.status);

    return this.httpClient.post<boolean>(`/api/orderdetails`, orderdetail);
  }

  /*The business logic necessary for updating the status of a order when it is cancelled*/
  CancelOrderDetail(orderdetail: OrderDetail): Observable<boolean> {
    console.log(orderdetail.status);
    if (orderdetail.status == "Returned") {
      alert("Already Retuned");
    }
    else {
      orderdetail.status = "Cancelled";
    }
    console.log(orderdetail.status);


    return this.httpClient.post<boolean>(`/api/orderdetails`, orderdetail);
  }

  uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}



