import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { Supplier } from '../Models/supplier';
import { Admin } from '../Models/admin';
import { RawMaterial } from '../Models/raw-material';
import { Return, ReturnChannel } from '../Models/return';
import { ReturnDetail, ReturnReasons } from '../Models/return-detail';
import { Order } from '../Models/order';

/*import {}*/


@Injectable({
  providedIn: 'root'
})
export class InventoryDataService implements InMemoryDbService
{
  constructor() { }

  createDb()
  {
    let admins = [
      new Admin(1, '101', 'Admin', 'admin@capgemini.com', 'manager')
    ];

    let suppliers = [
      new Supplier(1, "401476EE-0A3B-482E-BD5B-B94A32355959", "Scott", "9876543210", "scott@capgemini.com", "Scott123#", "10/3/2019", "10/4/2019"),
      new Supplier(2, "C628855C-FE7A-4D94-A1BB-167157D3F4EA", "Smith", "9988776655", "smith@capgemini.com", "Smith123#", "9/6/2019", "5/7/2019"),
      new Supplier(3, "6D68849C-8FA8-4049-A111-B431C76C6548", "Arun", "7781994834", "arun@capgemini.com", "Arun123#", "1/5/2017", "15/11/2018"),
      new Supplier(4, "53E8748F-61D6-494B-BF72-E18B27511EFA", "Jones", "6989493491", "jones@capgemini.com", "Jones123#", "2/7/2019", "12/1/2019")
    ];

    let rawmaterials = [
      new RawMaterial(1, "5D3034E3-ED22-4C24-9D2B-EEE4B187058E", "APP", "Apple", 20, "3/7/2012", "8/9/2019"),
      new RawMaterial(2, "ED3DCF6D-1A93-4F94-B91C-18546B04DB34", "SUG", "Sugar", 180, "8/1/2002", "8/9/2003"),
      new RawMaterial(3, "59D6BD5D-9B05-435F-80DF-74647301B835", "CIT", "Citric Acid", 17, "12/10/2009", "8/9/2017"),
      new RawMaterial(4, "E58BCC33-A90D-43B5-B365-D51CEA3B2A82", "BAN", "Banana", 12, "21/9/2010", "8/9/2018")
    ];

   
    let orders = [];

    let orderdetails = [];
    let returns = [
      new Return(1, "7A4B4DF6-5226-4138-B7AF-C5F139F81F62", "87CA73D9-EA84-483B-AF9E-39712DBDD76C", ReturnChannel.Offline, 2600, "3/7/2012", "8/9/2019"),
      new Return(2, "AB5E0C53-0A3E-4F9D-83CF-0735E4D42C95","CFB21EFD-9FA1-4709-A23E-7406E22C6CCC",ReturnChannel.Online, 2200, "8/1/2002", "8/9/2003"),
      new Return(3, "7A4B4DF6-5226-4138-B7AF-C5F139F81F62","2DF73D56-1299-42B4-9AA7-564FE4792D4F", ReturnChannel.Offline, 8000, "12/10/2009", "8/9/2017"),
      new Return(4, "56773A91-2C9F-47C8-9675-6CB7CA137C36", "C6DEB2C0-37C3-4189-8E88-524F1BA81122",ReturnChannel.Online, 2500, "21/9/2010", "8/9/2018"),
      new Return(5, "C193233B-FCE7-4860-A07C-3BBC325B3AAF", "9270EE70-7D6D-4116-8512-040500E24422",ReturnChannel.Online, 4400, "3/7/2019", "23/8/2019")
    ];

    let returndetails = [
      new ReturnDetail(1, "96C1199D-C161-4EFD-BC5C-C8879704340D", "7A4B4DF6-5226-4138-B7AF-C5F139F81F62", "0D34B1BA-DE79-41AC-A0FC-1378CC85B28B", "Backpack", 1, ReturnReasons.Incomplete, 1200, 1200, "3/7/2012", "8/9/2019"),
      new ReturnDetail(2, "025FFB2E-3389-4985-BE93-50AECDDC0F63", "7A4B4DF6-5226-4138-B7AF-C5F139F81F62", "BD923480-0321-4FD6-9236-304B4791554E", "Trekking Shoes", 1, ReturnReasons.Incomplete, 1400, 1400, "3/7/2012", "8/9/2019"),
      new ReturnDetail(3, "E3421F10-0FD6-427C-8691-22BC2B41BBA0", "AB5E0C53-0A3E-4F9D-83CF-0735E4D42C95", "9FE62255-5B2A-45BC-9744-82E0BF2B16BF", "Stick", 2, ReturnReasons.Incomplete, 700, 1400, "8/1/2002", "8/9/2003"),
      new ReturnDetail(4, "E1C82052-B6F9-445F-8635-BD41D1FAB836", "AB5E0C53-0A3E-4F9D-83CF-0735E4D42C95", "112369FE-FA59-45B2-B40A-4569295278C0", "Ball", 2, ReturnReasons.Incomplete, 400, 800, "8/1/2002", "8/9/2003"),
      new ReturnDetail(5, "AE19E7A9-F12F-4793-BCC5-73B7155E850D", "7A4B4DF6-5226-4138-B7AF-C5F139F81F62", "2D50A624-6419-48D8-8517-D0F8F86C1D82", "Mountaineering Helmet", 2, ReturnReasons.Incomplete, 2000, 4000, "12/10/2009", "8/9/2017"),
      new ReturnDetail(6, "D5D712D2-78A4-4CFE-8A4B-AC61C7C5D8B7", "7A4B4DF6-5226-4138-B7AF-C5F139F81F62", "C5BBD228-B3A3-4F19-8379-52DA8DE3CFE7", "Momentum Traction Spike", 2, ReturnReasons.Incomplete, 2000, 4000, "12/10/2009", "8/9/2017"),
      new ReturnDetail(7, "90F206AF-D2DE-49D1-9CCD-E3CDCB2E1027", "56773A91-2C9F-47C8-9675-6CB7CA137C36", "1E75244E-40D3-4C9B-9866-B334393986A5", "Gloves", 3, ReturnReasons.Incomplete, 300, 900, "21/9/2010", "8/9/2018"),
      new ReturnDetail(8, "D9D8C118-7959-475A-9E59-04017D94D36C", "56773A91-2C9F-47C8-9675-6CB7CA137C36", "8C050B7E-1C93-4E08-A52E-33F02C54BDB5", "Goggles", 2, ReturnReasons.Incomplete, 800, 1600, "21/9/2010", "8/9/2018"),
      new ReturnDetail(9, "DC12FE0D-3089-4649-B025-D259ED6C9CC3", "C193233B-FCE7-4860-A07C-3BBC325B3AAF", "F8F45F0B-89DD-4A61-A497-5466A116519C", "Wallet", 4, ReturnReasons.Incomplete, 400, 1600, "3/7/2019", "23/8/2019"),
      new ReturnDetail(10, "78696ED7-0898-4C19-930D-36DE1919B0C4", "C193233B-FCE7-4860-A07C-3BBC325B3AAF", "CB229BD8-5354-451B-B847-4C4232AFF52E", "Bag", 4, ReturnReasons.Incomplete, 700, 2800, "3/7/2019", "23/8/2019")
    ];

    return { admins, suppliers, rawmaterials, orders, orderdetails };
  }
}


