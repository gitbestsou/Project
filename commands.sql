use DatabaseName;


DROP TABLE TeamA.ReturnDetails;
Go;

Create Table TeamA.ReturnDetails
(
ReturnDetailID uniqueidentifier constraint PK_TeamA_ReturnDetails_ReturnDetailID PRIMARY KEY,
ReturnID uniqueidentifier constraint FK_TeamA_ReturnTable_ReturnDetails_ReturnID foreign key references TeamA.ReturnTable(ReturnID),
ProductID uniqueidentifier constraint FK_TeamA_Products_ReturnDetails_ProductID foreign key references TeamA.Products(ProductID),
Quantity int NOT NULL CHECK (Quantity >= 0),
ReasonOfReturn varchar(10) NOT NULL CHECK (ReasonOfReturn='Incomplete' OR ReasonOfReturn='Wrong'),
UnitPrice Decimal(15,2) NOT NULL CHECK (UnitPrice>0),
TotalPrice Decimal(15,2) NOT NULL CHECK (TotalPrice>0),
AddressID uniqueidentifier constraint FK_TeamA_Address_ReturnDetails_AddressID foreign key references TeamA.Addresses(AddressID)
);
Go
