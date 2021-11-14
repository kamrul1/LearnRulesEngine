using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine
{
    public class DiscountCalculator
    {
        public interface IDiscountRule
        {
            decimal CalculateDiscount(Customer customer);
        }

        private decimal CalculateDiscountForFirstTimeCustomer(Customer customer)
        {

            if (!customer.DateOfFirstPurchase.HasValue)
            {
                return .15m;
            }
            return 0m;
        }
    

        public decimal CalculateDiscountPercentage(Customer customer)
        {
            bool isBirthday = customer.DateOfBirth.HasValue && 
                customer.DateOfBirth.Value.Month == DateTime.Today.Month && 
                customer.DateOfBirth.Value.Day == DateTime.Today.Day;

            decimal discount = 0m;

            discount = CalculateDiscountForFirstTimeCustomer(customer);

            if (customer.DateOfFirstPurchase.HasValue)
            {
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-15))
                {
                    if (isBirthday) return .25m;
                    return .15m;
                }
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-10))
                {
                    if (isBirthday) return .22m;
                    return .12m;
                }
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-5))
                {
                    if (isBirthday) return .20m;
                    return .10m;
                }
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-2) &&
                    !customer.IsVeteran)
                {
                    if (isBirthday) return .18m;
                    return .08m;
                }
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-1) &&
                    !customer.IsVeteran)
                {
                    if (isBirthday) return .15m;
                    return .05m;
                }
            }


            if (customer.IsVeteran)
            {
                if (isBirthday) return .20m;
                return .10m;
            }
            
            var d = DateTime.Now.AddYears(-65);

            var tst = customer.DateOfBirth < DateTime.Now.AddYears(-65);

            if (customer.DateOfBirth < DateTime.Now.AddYears(-65))
            {
                if (isBirthday) return .15m;
                return Math.Max(discount,.05m);
            }

            if (isBirthday) return .10m;

  
            return discount;
        }
    }
}
