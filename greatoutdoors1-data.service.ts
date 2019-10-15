import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { Order } from '../Models/order';
import { OrderDetail } from '../Models/order-detail';
import { Return } from '../Models/return'
import { ReturnDetail } from '../Models/return-detail';



@Injectable({
  providedIn: 'root'
})
export class GreatOutdoorsDataService implements InMemoryDbService {
  constructor() { }

  createDb() {


    let orders = [
      new Order(1, "7A4B4DF6-5226-4138-B7AF-C5F139F81F62", 2, 2600, "87CA73D9-EA84-483B-AF9E-39712DBDD76C", "Offline", "3/9/2012", "8/9/2019"),
      new Order(2, "AB5E0C53-0A3E-4F9D-83CF-0735E4D42C95", 4, 2200, "CFB21EFD-9FA1-4709-A23E-7406E22C6CCC", "Online", "8/1/2002", "8/9/2003"),
      new Order(3, "5351729E-17D6-4CCE-8A21-68D798CC460A", 4, 8000, "2DF73D56-1299-42B4-9AA7-564FE4792D4F", "Offline", "12/10/2009", "8/9/2017"),
      new Order(4, "56773A91-2C9F-47C8-9675-6CB7CA137C36", 5, 2500, "C6DEB2C0-37C3-4189-8E88-524F1BA81122", "Online", "21/9/2010", "8/9/2018"),
      new Order(5, "C193233B-FCE7-4860-A07C-3BBC325B3AAF", 8, 4400, "9270EE70-7D6D-4116-8512-040500E24422", "Online", "3/7/2019", "23/8/2019")
    ];


    let orderdetails = [
      new OrderDetail(1, "96C1199D-C161-4EFD-BC5C-C8879704340D", "7A4B4DF6-5226-4138-B7AF-C5F139F81F62", "0D34B1BA-DE79-41AC-A0FC-1378CC85B28B", "Backpack", 1, 1200, 1200, "Underprocessing", "D7F9336C-ADB4-4A7D-9987-9D504103F740", "3/9/2012", "8/9/2019"),
      new OrderDetail(2, "025FFB2E-3389-4985-BE93-50AECDDC0F63", "7A4B4DF6-5226-4138-B7AF-C5F139F81F62", "BD923480-0321-4FD6-9236-304B4791554E", "Trekking Shoes", 1, 1400, 1400, "Delivered", "B94D9651-18E2-4BCB-976E-7FA98ED03011", "3/9/2012", "8/9/2019"),
      new OrderDetail(3, "E3421F10-0FD6-427C-8691-22BC2B41BBA0", "AB5E0C53-0A3E-4F9D-83CF-0735E4D42C95", "9FE62255-5B2A-45BC-9744-82E0BF2B16BF", "Stick", 2, 700, 1400, "Underprocessing", "D7F9336C-ADB4-4A7D-9987-9D504103F740", "8/1/2002", "8/9/2003"),
      new OrderDetail(4, "E1C82052-B6F9-445F-8635-BD41D1FAB836", "AB5E0C53-0A3E-4F9D-83CF-0735E4D42C95", "112369FE-FA59-45B2-B40A-4569295278C0", "Ball", 2, 400, 800, "Delivered", "B94D9651-18E2-4BCB-976E-7FA98ED03011", "8/1/2002", "8/9/2003"),
      new OrderDetail(5, "AE19E7A9-F12F-4793-BCC5-73B7155E850D", "5351729E-17D6-4CCE-8A21-68D798CC460A", "2D50A624-6419-48D8-8517-D0F8F86C1D82", "Mountaineering Helmet", 2, 2000, 4000, "Underprocessing", "D7F9336C-ADB4-4A7D-9987-9D504103F740", "12/10/2009", "8/9/2017"),
      new OrderDetail(6, "D5D712D2-78A4-4CFE-8A4B-AC61C7C5D8B7", "5351729E-17D6-4CCE-8A21-68D798CC460A", "C5BBD228-B3A3-4F19-8379-52DA8DE3CFE7", "Momentum Traction Spike", 2, 2000, 4000, "Delivered", "B94D9651-18E2-4BCB-976E-7FA98ED03011", "12/10/2009", "8/9/2017"),
      new OrderDetail(7, "90F206AF-D2DE-49D1-9CCD-E3CDCB2E1027", "56773A91-2C9F-47C8-9675-6CB7CA137C36", "1E75244E-40D3-4C9B-9866-B334393986A5", "Gloves", 3, 300, 900, "Underprocessing", "D7F9336C-ADB4-4A7D-9987-9D504103F740", "21/9/2010", "8/9/2018"),
      new OrderDetail(8, "D9D8C118-7959-475A-9E59-04017D94D36C", "56773A91-2C9F-47C8-9675-6CB7CA137C36", "8C050B7E-1C93-4E08-A52E-33F02C54BDB5", "Goggles", 2, 800, 1600, "Delivered", "B94D9651-18E2-4BCB-976E-7FA98ED03011", "21/9/2010", "8/9/2018"),
      new OrderDetail(9, "DC12FE0D-3089-4649-B025-D259ED6C9CC3", "C193233B-FCE7-4860-A07C-3BBC325B3AAF", "F8F45F0B-89DD-4A61-A497-5466A116519C", "Wallet", 4, 400, 1600, "Underprocessing", "D7F9336C-ADB4-4A7D-9987-9D504103F740", "3/7/2019", "23/8/2019"),
      new OrderDetail(10, "78696ED7-0898-4C19-930D-36DE1919B0C4", "C193233B-FCE7-4860-A07C-3BBC325B3AAF", "CB229BD8-5354-451B-B847-4C4232AFF52E", "Bag", 4, 700, 2800, "Delivered", "B94D9651-18E2-4BCB-976E-7FA98ED03011", "3/7/2019", "23/8/2019")

    ];
    let returns = [];
    let returndetails = [];
    let temporaryOrderDetails = [

      new OrderDetail(0,null,null,null,null,0,0,0,null,null,null,null)
    ];

    return { orders, orderdetails, returns, returndetails, temporaryOrderDetails};
  }
}

