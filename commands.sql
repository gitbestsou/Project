use DatabaseName;




/*1.Delete the tables ReturnDetails first and then ReturnTable manually*/


/*2.Execte the below commands*/
CREATE TABLE TeamA.[Return]
(
ReturnID uniqueidentifier constraint PK_TeamA_ReturnTable_ReturnID PRIMARY KEY,
OrderID uniqueidentifier constraint FK_TeamA_Orders_ReturnTable_OrderID foreign key  references TeamA.Orders(OrderID),
ChannelOfReturn varchar(7) NOT NULL CHECK (ChannelOfReturn = 'Online' OR ChannelOfReturn='Offline'),
ReturnAmount decimal(15,2) NOT NULL CHECK (ReturnAmount>0),
ReturnDateTime datetime NOT NULL,
LastModifiedDateTime datetime NOT NULL
);
Go





Create Table TeamA.ReturnDetails
(
ReturnDetailID uniqueidentifier constraint PK_TeamA_ReturnDetails_ReturnDetailID PRIMARY KEY,
ReturnID uniqueidentifier constraint FK_TeamA_ReturnTable_ReturnDetails_ReturnID foreign key references TeamA.[Return](ReturnID),
ProductID uniqueidentifier constraint FK_TeamA_Products_ReturnDetails_ProductID foreign key references TeamA.Products(ProductID),
Quantity int NOT NULL CHECK (Quantity >= 0),
ReasonOfReturn varchar(10) NOT NULL CHECK (ReasonOfReturn='Incomplete' OR ReasonOfReturn='Wrong'),
UnitPrice Decimal(15,2) NOT NULL CHECK (UnitPrice>0),
TotalPrice Decimal(15,2) NOT NULL CHECK (TotalPrice>0),
AddressID uniqueidentifier constraint FK_TeamA_Address_ReturnDetails_AddressID foreign key references TeamA.Addresses(AddressID)
);
Go


/*3.Delete all the stored procedures associated with Return and ReturnDetails manually*/

/*4. Execute*/


Create Procedure TeamA.AddReturn(@returnID uniqueidentifier, @orderID uniqueidentifier, @channelOfReturn varchar(7), @returnAmount decimal(15,2), @returnDateTime datetime,@lastModifiedDateTime datetime)
as
begin
if @returnID is null
throw 5002,'Invalid Return ID',1
if @orderID = ' '
throw 5002,'Invalid Order ID',2
if @channelOfReturn != 'Offline' AND @channelOfReturn != 'Online'
throw 5002,'Invalid Channel Of Return',3
if @returnAmount = 0
throw 5002,'Invalid Return Amount', 4 
if @returnDateTime is null
throw 5002, 'Date Time cannot be null.',0
if @returnDateTime is null
throw 5002, 'Date Time cannot be null.',0
if @lastModifiedDateTime is null
throw 5002, 'Date Time cannot be null.',0

 begin
 INSERT INTO TeamA.[Return](ReturnID, OrderID, ChannelOfReturn, ReturnAmount, ReturnDateTime, LastModifiedDateTime)
 VALUES(@returnID, @orderID, @channelOfReturn, @returnAmount, @returnDateTime, @lastModifiedDateTime)
 end
end
GO
--CREATED PROCEDURE FOR Adding Order


Create procedure TeamA.GetAllReturns
 as 
begin
begin
select * from TeamA.[Return]
end
end
Go



Create procedure TeamA.GetReturnByReturnID(@returnID varchar(255))
as 
begin

begin try
Select * from TeamA.[Return] where ReturnID = @returnID
end try
begin catch
 PRINT @@ERROR;
 throw 5005,'Invalid values fetched.',2
 return 0
end catch

end
Go




Create procedure TeamA.GetAllReturnDetails
 as 
begin
begin
select * from TeamA.ReturnDetails
end
end
Go










Create procedure TeamA.GetReturnByOrderID(@orderID uniqueidentifier)
as 
begin

begin try
Select * from TeamA.[Return] where OrderID = @orderID
end try
begin catch
 PRINT @@ERROR;
 throw 5005,'Invalid values fetched.',2
 return 0
end catch

end
Go

Create Procedure TeamA.DeleteReturn(@returnID uniqueidentifier)
as
begin
begin try
DELETE FROM TeamA.[Return] WHERE ReturnID = @returnID
end try
begin catch
 PRINT @@ERROR;
 throw 5006,'Values not deleted.',3
 return 0
end catch
end
GO

/*-----------------------------------------------------------------------------------------------------------------------------*/







/*-------------------------------------------------Stored Procedures for Return Details-----------------------------*/

Create Procedure TeamA.AddReturnDetails(@returnDetailID uniqueidentifier, @returnID uniqueidentifier,@productID uniqueidentifier, @quantity int ,@reasonOfReturn varchar(10), @unitPrice int, @totalPrice money, @addressID uniqueidentifier) as
begin
if @returnDetailID is null OR @returnDetailID = ' '
throw 5001,'Invalid Return Detail ID',1
if @returnID is null OR @returnID = ' '
throw 5001,'Invalid Return ID',2
if @productID is null OR @productID = ' '
throw 5001, 'Invalid Product ID',3
if @quantity <= 0
throw 5001,'Quantity Entered is 0',4
if @reasonOfReturn !='Online' AND @reasonOfReturn != 'Offline'
throw 5001, 'Invalid Reason',5
if @UnitPrice = 0
throw 5001, 'Invalid Unit Price',5
if @totalPrice = 0
throw 5001,'Invalid Total Price',6
if @addressID is null OR @addressID = ' '
throw 5001, 'Invalid Address ID',0
INSERT INTO TeamA.ReturnDetails(ReturnDetailID, ReturnID, ProductID, Quantity, ReasonOfReturn, UnitPrice, TotalPrice, AddressID)
VALUES(@returnDetailID, @returnID, @productID, @quantity,@reasonOfReturn, @UnitPrice, @totalPrice, @addressID)
end
GO





Create Procedure TeamA.GetReturnDetailsByReturnID(@returnID uniqueidentifier)
as
begin
begin try
Select * from TeamA.ReturnDetails where ReturnID = @returnID
end try
begin catch
 PRINT @@ERROR;
 throw 5007,'Invalid values fetched.',1
 return 0
end catch
end
GO

drop procedure TeamA.GetReturnDetailsByProductID;
Go;
Create Procedure TeamA.GetReturnDetailsByProductID(@productID uniqueidentifier)
as
begin
begin try
Select * from TeamA.ReturnDetails where ProductID = @productID
end try
begin catch
 PRINT @@ERROR;
 throw 5000,'Invalid values fetched.',2
 return 0
end catch
end
GO
--created procedure





Create Procedure TeamA.DeleteReturnDetails(@returnID uniqueidentifier, @productID uniqueidentifier)
as
begin
begin try
DELETE FROM TeamA.ReturnDetails where ReturnID = @returnID AND ProductID = @productID
end try
begin catch
 PRINT @@ERROR;
 throw 5000,'Values not deleted.',3
 return 0
end catch
end
GO





