<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" Theme="Theme1"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%="Capgemini" %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Label ID="Label1" runat="server" Text="First Name:     "></asp:Label>
        <asp:textbox runat="server" ID="FirstName"></asp:textbox>
        <br /><br />
        <asp:Label ID="Label2" runat="server" Text="Last Name:      "></asp:Label>
        <asp:textbox runat="server" ID="LastName"></asp:textbox>
        <br /><br />
            
            <table border="1">
                <tr>
                <td>Products</td>
                <td><asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Text="Apple" Value="1"/>
                <asp:ListItem Text="Samsung" Value="2"/>
                <asp:ListItem Text="Nokia" Value="3"/>
                </asp:DropDownList></td>
                </tr>

                <tr>
                    <td>Quantity</td>
                    <td>
                        <asp:TextBox ID="Quantity" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>Total Amount</td>
                    <td>
                        <asp:TextBox ID="TotalAmount" runat="server"></asp:TextBox>
                    </td>
                </tr>

            </table>  
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" onClick="Button1_Click"/>
            <br /><br />
            <asp:Button ID="Button2" runat="server" Text="Button" onClick="Button2_Click"/>
        </div>
        
        <div>
        <asp:GridView ID="GridViewProducts" runat="server"></asp:GridView>
        </div>
    </form>

    <div>
        <asp:Label ID="Label3" runat="server"></asp:Label>
        <br />
        
        <asp:Label ID="Label4" runat="server"></asp:Label>
    </div>

    <div>


    </div>



</body>
</html>

<script type="text/javascript">

</script>