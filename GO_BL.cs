using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GO.Entities;
using GO.Exceptions;
using GO.DataAccessLayer;

namespace GreatOutdoors.BusinessLayer
{
        private static bool ValidateReturn(Return returnobj)
        {
            StringBuilder sb = new StringBuilder();
            bool validReturn = true;
            if (returnobj.ReturnID <= 0)
            {
                validReturn = false;
                sb.Append(Environment.NewLine + "Invalid Return ID");

            }
            if (returnobj.OrderID <= 0)
            {
                validReturn = false;
                sb.Append(Environment.NewLine + "Invalid Order ID");

            }
            if (returnobj.ProductID <= 0)
            {
                validReturn = false;
                sb.Append(Environment.NewLine + "Invalid Product ID");

            }
			if(returnobj.IncompleteOrder == null)
			{
                validReturn = false;
                sb.Append(Environment.NewLine + "Invalid IncompleteOrder status");
			}
			if(returnobj.WrongOrder == null)
			{
                validReturn = false;
                sb.Append(Environment.NewLine + "Invalid WrongOrder status");
			}

			if(returnobj.ReturnValue<=0)
			{
				validReturn = false;
				sb.Append(Environment.NewLine + "Invalid Return Value");
			}
            if (returnobj.ReturnQuantity <= 0)
            {
                validReturn = false;
                sb.Append(Environment.NewLine + "Invalid Return Quantity");

            }


            if (validReturn == false)
                throw new ReturnException(sb.ToString());
            return validReturn;
        }
		
		private static bool ValidateSpecification(Specification specification)
		{
            StringBuilder sb = new StringBuilder();
			bool validSpecification = true;
		
            if (specification.ProductID <= 0)
            {
                validSpecification = false;
                sb.Append(Environment.NewLine + "Invalid Product ID");

            }
            if (specification.SpecificationID <= 0)
            {
                validSpecification = false;
                sb.Append(Environment.NewLine + "Invalid Specification ID");

            }
			if(specification.Color == null)
			{
				validSpecification = false;
				sb.Append(Environment.NewLine + "Invalid Color Specification");
			}
			if(specification.Size <= 0)
			{
				validSpecification = false;
				sb.Append(Environment.NewLine + "Invalid Size Specification");
			}
			if(specification.TechSpec == null)
			{
				validSpecification = false;
				sb.Append(Environment.NewLine + "Invalid Technical Specification");
			}
		if(validSpecification==false)
			throw new SpecificationException(sb.ToString());

		return validSpecification;

		}

		public static bool OrderForReturn(Return returnobj)
		{
					


		}

		public static void TrackReturn(Return returnobj)
		{



		}
		public static void DeductMoneyFromRevenue(Return returnobj)
		{
		
		}		
		public static void CancelOrder(Return returnobj)
		{
	
		}





}



