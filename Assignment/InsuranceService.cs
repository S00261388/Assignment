using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class InsuranceService
    {
        private readonly IDiscountService _discountService;

        public InsuranceService(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public double CalcPremium(int age, string location)
        {
            double premium;
            if (location == "rural")
            {
                if ((age >= 18) && (age < 30))
                {
                    premium = 5.0;
                }
                else
                {
                    premium = age >= 31 ? 2.50 : 0.0;
                }
            }
            else
            {
                premium = location == "urban" ? (age >= 18 && age <= 35 ? 6.0 : age >= 36 ? 5.0 : 0.0) : 0.0;
            }

            double discount = _discountService.GetDiscount();
            if (age >= 50)
            {
                premium *= discount;
            }
            return premium;
        }
    }
}
