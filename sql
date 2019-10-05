USE [GreatOutdoors]
GO

/****** Object:  Schema [GreatOutdoors]    Script Date: 27-09-2019 18:24:43 ******/
CREATE SCHEMA [GOOrders]
GO
/*----------*/


/*-----------------------------------------------Table Creation---------------------------------------------------------------*/
CREATE TABLE GOOrders.ReturnTable
(
ReturnID uniqueidentifier constraint PK_GOOrders_ReturnTable_ReturnID PRIMARY KEY,
OrderID uniqueidentifier constraint FK_GOOrders_Orders_ReturnTable_OrderID foreign key  references GOOrders.Orders(OrderID),
ChannelOfReturn varchar(7) NOT NULL CHECK (ChannelOfReturn = 'Online' OR ChannelOfReturn='Offline'),
ReturnAmount decimal(15,2) NOT NULL CHECK (ReturnAmount>0),
ReturnDateTime datetime NOT NULL,
LastModifiedDateTime datetime NOT NULL
)
GO

Create Table GreatOutdoors.ReturnDetails
(
ReturnDetailID uniqueidentifier constraint PK_GOOrders_ReturnDetails_ReturnDetailID PRIMARY KEY,
ReturnID uniqueidentifier constraint FK_GOOrders_ReturnTable_ReturnDetails_ReturnID foreign key references GOOrders.ReturnTable(ReturnID),
ProductID uniqueidentifier,
Quantity int NOT NULL CHECK (Quantity >= 0),
ReasonOfReturn varchar(10) NOT NULL CHECK (ReasonOfReturn='Incomplete' OR ReasonOfReturn='Wrong'),
UnitPrice Decimal(15,2) NOT NULL CHECK (UnitPrice>0),
TotalPrice Decimal(15,2) NOT NULL CHECK (TotalPrice>0),
AddressID uniqueidentifier
)
GO
/*----------------------------------------------------------------------------------------------------------------------------------------------------*/



/*----------------------------------------------------------Stored Procedures for Return Method----------------------------------------------------*/
Create Procedure GreatOutdoors.AddReturn(@returnID varchar(255), @orderID varchar(255), @channelOfReturn varchar(7), @returnAmount decimal(15,2), @returnDateTime datetime,@lastModifiedDateTime datetime)
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
 INSERT INTO GOOrders.ReturnTable(ReturnID, OrderID, ChannelOfReturn, ReturnAmount, ReturnDateTime, LastModifiedDateTime)
 VALUES(@returnID, @orderID, @channelOfReturn, @returnAmount, @returnDateTime, @lastModifiedDateTime)
 end
end
GO
--CREATED PROCEDURE FOR Adding Order


Create procedure GOOrders.GetAllReturns
 as 
begin
begin
select * from GOOrders.ReturnTable inner join GOOrders.Orders on GOOrders.ReturnTable.OrderID = GOOrders.Orders.OrderID
end
end
Go

Create procedure GreatOutdoors.GetReturnByReturnID(@returnID varchar(255))
as 
begin

begin try
Select * from GreatOutdoors.Returns where ReturnID = @returnID
end try
begin catch
 PRINT @@ERROR;
 throw 5005,'Invalid values fetched.',2
 return 0
end catch

end
Go

Create procedure GreatOutdoors.GetReturnByOrderID(@orderID varchar(255))
as 
begin

begin try
Select * from GreatOutdoors.Returns where OrderID = @orderID
end try
begin catch
 PRINT @@ERROR;
 throw 5005,'Invalid values fetched.',2
 return 0
end catch

end
Go

Create Procedure GreatOutdoors.DeleteReturn(@returnID VARCHAR(255))
as
begin
begin try
DELETE FROM GreatOutdoors.Returns WHERE ReturnID = @returnID
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

Create Procedure GreatOutdoors.AddReturnDetails(@returnDetailID varchar(255), @returnID varchar(255),@productID varchar(255), @quantity ,@reasonOfReturn varchar(10), @unitPrice int, @totalPrice money, @addressID varchar(255))
assmallint
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
INSERT INTO GreatOutdoors.ReturnDetails(ReturnDetailID, ReturnID, ProductID, Quantity, ReasonOfReturn, UnitPrice, TotalPrice, AddressID)
VALUES(@returnDetailID, @returnID, @productID, @quantity,@reasonOfReturn, @UnitPrice, @totalPrice, @addressID)
end
GO

Create Procedure GreatOutdoors.GetReturnDetailsByReturnID(@returnID VARCHAR(255))
as
begin
begin try
Select * from GreatOutdoors.ReturnDetails where ReturnID = @returnID
end try
begin catch
 PRINT @@ERROR;
 throw 5007,'Invalid values fetched.',1
 return 0
end catch
end
GO



Create Procedure GreatOutdoors.GetReturnDetailsByProductID(@productID VARCHAR(255))
as
begin
begin try
Select * from GreatOutdoors.ReturnDetails where ProductID = @productID
end try
begin catch
 PRINT @@ERROR;
 throw 5000,'Invalid values fetched.',2
 return 0
end catch
end
GO
--created procedure





Create Procedure GreatOutdoors.DeleteReturnDetails(@returnID VARCHAR(255), @productID VARCHAR(255))
as
begin
begin try
DELETE FROM GreatOutdoors.ReturnDetails WHERE ReturnID = @returnID AND ProductID = @productID
end try
begin catch
 PRINT @@ERROR;
 throw 5000,'Values not deleted.',3
 return 0
end catch
end
GO
--created procedure


/*-------------------------------------------------------------------------------------------------------------------*/
