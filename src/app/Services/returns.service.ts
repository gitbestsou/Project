import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Return } from '../Models/return';

@Injectable({
  providedIn: 'root'
})
export class ReturnService {
  constructor(private httpClient: HttpClient) {
  }

  AddReturn(returnObj: Return): Observable<boolean> {
    returnObj.creationDateTime = new Date().toLocaleDateString();
    returnObj.lastModifiedDateTime = new Date().toLocaleDateString();
    returnObj.returnID = this.uuidv4();
    return this.httpClient.post<boolean>(`/api/orders`, returnObj);
  }

  UpdateReturn(returnObj: Return): Observable<boolean> {
    returnObj.lastModifiedDateTime = new Date().toLocaleDateString();
    return this.httpClient.put<boolean>(`/api/returns`, returnObj);
  }

  DeleteReturn(returnID: string, id: number): Observable<boolean> {
    return this.httpClient.delete<boolean>(`/api/returns/${id}`);
  }

  GetAllReturns(): Observable<Return[]> {
    return this.httpClient.get<Return[]>(`/api/returns`);
  }

  GetReturnByReturnID(ReturnID: number): Observable<Return> {
    return this.httpClient.get<Return>(`/api/returns?returnID=${ReturnID}`);
  }

  uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}



