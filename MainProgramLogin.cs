using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatOutdoors.PresentationLayer
{
    class PresentationLayer
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Great Outdoors..");
            int choice = 0;
            do
            {
                Console.WriteLine("Enter your choice:");
                Console.WriteLine("1. Login as Admin");
                Console.WriteLine("2. Login as Salesperson");
                Console.WriteLine("3. Login as Retailer");
                Console.WriteLine("4. Register as a Retailer");
                Console.WriteLine("5. Exit");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("--Admin Login--");
                        Admin admin = new Admin();
                        Console.WriteLine("Enter Username");
                        string usernameAdmin = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        string passwordAdmin = Console.ReadLine();
                        if (!admin.authenticateAdmin(usernameAdmin, passwordAdmin))
                        {
                            Console.WriteLine("Invalid Credentials");
                        }
                        else
                        {

                            Console.WriteLine("Login Successful");
                            char choiceAdmin;
                            do
                            {
                                Console.WriteLine("Enter your choice:");
                                Console.WriteLine("a. View Sales Report");
                                Console.WriteLine("b. View Retailer Report");
                                Console.WriteLine("c. View Overall Report");
                                Console.WriteLine("d. Update Bonus");
                                Console.WriteLine("e. Update Discount");
                                Console.WriteLine("f. Log out");
                                choiceAdmin = Convert.ToChar(Console.ReadLine());
                                switch (choiceAdmin)
                                {
                                    case 'a':
                                        //code'for sales report
                                        break;
                                    case 'b':
                                        //code'for retailer report
                                        break;
                                    case 'c':
                                        //code for overall report
                                        break;
                                    case 'd':
                                        //code for update bonus
                                        break;
                                    case 'e':
                                        //code for update discount
                                        break;
                                    case 'f':
                                        //code for log out
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Option");
                                        break;

                                }//end of switch case for adminChoice

                            } while (choiceAdmin != 'f');

                        }
                        break;

                    case 2:
                        Console.WriteLine("--Salesperson Login--");
                        Salesperson salesperson = new Salesperson();
                        Console.WriteLine("Enter Username");
                        String usernameSalesperson = Console.ReadLine();

                        Console.WriteLine("Enter Password");
                        String passwordSalesperson = Console.ReadLine();
                        if (!salesperson.authenticateSalesperson(usernameSalesperson, passwordSalesperson))
                        {
                            Console.WriteLine("Invalid Credentials");
                        }
                        else
                        {

                            Console.WriteLine("Login Successful");
                            char choiceSalesperson;
                            do
                            {
                                Console.WriteLine("Enter your choice:");
                                Console.WriteLine("a. View your Sales history");
                                Console.WriteLine("b. Upload Offline Order");
                                Console.WriteLine("c. Accept Offline Order");
                                Console.WriteLine("d. Log out");

                                choiceSalesperson = Convert.ToChar(Console.ReadLine());
                                switch (choiceSalesperson)
                                {
                                    case 'a':
                                        //code'for sales history
                                        break;
                                    case 'b':
                                        //code'for offline order
                                        break;
                                    case 'c':
                                        //code for accept offline order
                                        break;
                                    case 'd':
                                        //code for log out
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Option");
                                        break;

                                }//end of switch case for adminChoice

                            } while (choiceSalesperson != 'd');

                        }
                        break;

                    case 3:
                        Console.WriteLine("--Retailer Login--");
                        Retailer retailer = new Retailer();
                        Console.WriteLine("Enter Username");
                        String usernameRetailer = Console.ReadLine();

                        Console.WriteLine("Enter Password");
                        String passwordRetailer = Console.ReadLine();
                        if (!retailer.authenticateRetailer(usernameRetailer, passwordRetailer))
                        {
                            Console.WriteLine("Invalid Credentials");
                        }
                        else
                        {

                            Console.WriteLine("Login Successful");
                            char choiceRetailer;
                            do
                            {
                                Console.WriteLine("Enter your choice:");
                                Console.WriteLine("a. Initiate Order");
                                Console.WriteLine("b. View Order History");
                                Console.WriteLine("c. Return Order");
                                Console.WriteLine("d. Cancel Order");
                                Console.WriteLine("e. Log out");
                                choiceRetailer = Convert.ToChar(Console.ReadLine());
                                switch (choiceRetailer)
                                {
                                    case 'a':
                                        //code for initiate order
                                        //Order.placeOrder(usernameRetailer);
                                        //Order.placeOrder(usernameRetailer);

                                        break;
                                    case 'b':
                                        //code for viewing order history
                                        break;
                                    case 'c':
                                        //code for return order
                                        break;
                                    case 'd':
                                        //code for cancel order
                                        break;
                                    case 'e':
                                        //code for log out
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Option");
                                        break;

                                }//end of switch case for adminChoice

                            } while (choiceRetailer != 'e');

                        }
                        break;

                    case 4:
                        Console.WriteLine("--Register as a Retailer--");

                        //Code for Registering as a retailer
                        
                        Retailer retailerToBeRegistered = new Retailer();
                        Console.WriteLine("Enter your Name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter your Username:");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter your Password:");
                        string password = Console.ReadLine();
                        Console.WriteLine("Enter your Email:");
                        string email = Console.ReadLine();
                        retailerToBeRegistered.RegisterRetailer(name,username,password,email);
                        break;

                }

            } while (choice != 5);

        }
    }




    public abstract class User
    {
        private string _name;
        private string _userName;
        private string _password;
        private string _email;

        public string Name { get => _name; set => _name = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public string Email { get => _email; set => _email = value; }

    }
    public class Admin : User
    {
        private int _adminId;
        public int AdminId { get => _adminId; set => _adminId = value; }
        public Admin(string name, string username, string password, string email, int id)
        {
            Name = name; UserName = username; Password = password; Email = email; AdminId = id;
        }
        List<Admin> adminList = new List<Admin>();
        public Admin()
        {
            adminList.Add(new Admin("Tyler", "admin", "admin123", "abc@gmail.com", 1));

        }
        public bool authenticateAdmin(string usernameAdmin, string Password)
        {
            // code for authenticating admin
            bool signal = false;
            foreach (Admin item in adminList)
            {
                if (item.UserName == usernameAdmin && item.Password == Password)
                {
                    signal = true;
                }
            }
            return signal;
        }


    }
    public class Salesperson : User
    {
        private int _salespersonId;
        public int SalespersonId { get => _salespersonId; set => _salespersonId = value; }
        public Salesperson(string name, string username, string password, string email, int id)
        {
            Name = name; UserName = username; Password = password; Email = email; SalespersonId = id;
        }
        List<Salesperson> salespersonList = new List<Salesperson>();
        public Salesperson()
        {
            salespersonList.Add(new Salesperson("Bucky", "saler1", "saler123", "qwe@gmail.com", 1));
            salespersonList.Add(new Salesperson("Andrew", "saler2", "saler456", "rty@gmail.com", 2));
            salespersonList.Add(new Salesperson("Brad", "saler3", "saler789", "uio@gmail.com", 3));

        }
        public bool authenticateSalesperson(string usernameSalesperson, string Password)
        {
            // code for authenticating admin
            bool signal = false;
            
            foreach (Salesperson item in salespersonList)
            {
                if (item.UserName == usernameSalesperson && item.Password == Password)
                {
                    signal = true;
                }
            }
            return signal;
        }


    }

    public class Retailer : User
    {
        private int _retailerId;
        public int RetailerId { get => _retailerId; set => _retailerId = value; }
        public Retailer(string name, string username, string password, string email, int id)
        {
            Name = name; UserName = username; Password = password; Email = email; RetailerId = id;
        }
        public static List<Retailer> retailerList = new List<Retailer>();
        public Retailer()
        {
            retailerList.Add(new Retailer("Madhuri", "buyer1", "buyer123", "mad@gmail.com", retailerCount++));
            retailerList.Add(new Retailer("Sarthak", "buyer2", "buyer456", "sar@gmail.com", retailerCount++));
            retailerList.Add(new Retailer("Ankush", "buyer3", "buyer789", "mad@gmail.com", retailerCount++));
            retailerList.Add(new Retailer("Shreyash", "buyer4", "buyer012", "shr@gmail.com", retailerCount++));
            retailerList.Add(new Retailer("Sourav", "buyer5", "buyer345", "sou@gmail.com", retailerCount++));

        }

            
        int retailerCount = 1;
        public bool authenticateRetailer(string usernameRetailer, string Password)
        {
            // code for authenticating admin


            bool signal = false;
            
            foreach (Retailer item in retailerList)
            {
                if (item.UserName == usernameRetailer && item.Password == Password)
                {
                    signal = true;
                }
            }
            return signal;
        }
        public void RegisterRetailer(string name, string username, string password, string email)
        {
            retailerList.Add(new Retailer(name,username,password,email,retailerCount++));
            System.Console.WriteLine("Retailer Registered Successfully");
            foreach(Retailer item in retailerList)
                {
                System.Console.WriteLine(item.Name);
                System.Console.WriteLine(item.UserName);
                System.Console.WriteLine(item.Password);
                System.Console.WriteLine(item.Email);
                System.Console.WriteLine(item.RetailerId);
                }
        }


    }





}
