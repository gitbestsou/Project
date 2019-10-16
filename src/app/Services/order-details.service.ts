import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OrderDetail } from '../Models/order-detail';

/*
  
*
* Developer Name: Sourav Maji
  * Use Case - 1. Return a product from Orders which has been delivered
               2. Cancel a product from Orders which has been placed but not deliverd
Creation Date - 10 / 10 / 2019
Last Modified Date - 12 / 10 / 2019

*/




@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {
  constructor(private httpClient: HttpClient) {
  }

  AddOrderDetail(orderdetail: OrderDetail): Observable<boolean> {
    orderdetail.creationDateTime = new Date().toLocaleDateString();
    orderdetail.lastModifiedDateTime = new Date().toLocaleDateString();
    orderdetail.orderDetailID = this.uuidv4();
    return this.httpClient.post<boolean>(`/api/orderdetails`, orderdetail);
  }

  UpdateOrderDetail(orderdetail: OrderDetail): Observable<boolean> {
    orderdetail.lastModifiedDateTime = new Date().toLocaleDateString();
    return this.httpClient.put<boolean>(`/api/orderdetails`, orderdetail);
  }

  DeleteOrderDetail(orderDetailID: string, id: number): Observable<boolean> {
    return this.httpClient.delete<boolean>(`/api/orderdetails/${id}`);
  }

  GetAllOrderDetails(): Observable<OrderDetail[]> {
    return this.httpClient.get<OrderDetail[]>(`/api/orderdetails`);
  }

  GetOrderDetailByOrderDetailID(OrderDetailID: number): Observable<OrderDetail> {
    return this.httpClient.get<OrderDetail>(`/api/orderdetails?orderdetailID=${OrderDetailID}`);
  }

  GetOrderDetailByOrderID(orderID: string): Observable<OrderDetail[]> {
    return this.httpClient.get<OrderDetail[]>(`/api/orderdetails?orderID=${orderID}`);
  }

  uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}



