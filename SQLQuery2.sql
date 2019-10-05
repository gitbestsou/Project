USE [GreatOutdoors]
GO

/****** Object:  Schema [GreatOutdoors]    Script Date: 27-09-2019 18:24:43 ******/
CREATE SCHEMA [GreatOutdoors]
GO
/*----------*/


/*-----------------------------------------------Table Creation---------------------------------------------------------------*/
CREATE TABLE GreatOutdoors.Returns
(
ReturnID varchar(255) PK_Returns_ReturnID PRIMARY KEY,
OrderID varchar(255) NULL,
ChannelOfReturn varchar(7) NOT NULL CHECK (ChannelOfReturn = 'Online' OR ChannelOfReturn='Offline'),
ReturnAmount Money NOT NULL CHECK (ReturnAmount>0),
ReturnDateTime datetime NOT NULL
)
GO

Create Table GreatOutdoors.ReturnDetails
(
ReturnDetailID varchar(255) NOT NULL PRIMARY KEY,
ReturnID varchar(255) NOT NULL,
ProductID varchar(255) NOT NULL,
Quantity smallint NOT NULL CHECK (Quantity > 0),
ReasonOfReturn varchar(10) NOT NULL CHECK (ReasonOfReturn='Incomplete' OR ReasonOfReturn='Wrong'),
UnitPrice Decimal(15,2) NOT NULL CHECK (UnitPrice>0),
TotalPrice Decimal(15,2) NOT NULL CHECK (TotalPrice>0),
AddressID varchar(255) NOT NULL
)
GO
/*----------------------------------------------------------------------------------------------------------------------------------------------------*/



/*----------------------------------------------------------Stored Procedures for Return Method----------------------------------------------------*/
Create Procedure GreatOutdoors.AddReturn(@returnID varchar(255), @orderID varchar(255), @channelOfReturn varchar(7), @returnAmount money, @returnDateTime datetime)
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
 begin
 INSERT INTO GreatOutdoors.Returns(ReturnID, OrderID, ChannelOfReturn, ReturnAmount, ReturnDateTime)
 VALUES(@returnID, @orderID, @channelOfReturn, @returnAmount, @returnDateTime)
 end
end
GO
--CREATED PROCEDURE FOR Adding Order


Create procedure GreatOutdoors.GetAllReturns
 as 
begin
begin
select * from GreatOutdoors.Returns
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