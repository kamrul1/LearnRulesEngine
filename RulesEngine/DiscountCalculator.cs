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

        private decimal CalculateDiscountForVeteran(Customer customer)
        {

            if (customer.IsVeteran)
            {
                return .10m;
            }

            return 0m;

        }

        public decimal CalculateDiscountForSeniors(Customer customer)
        {

            if (customer.DateOfBirth < DateTime.Now.AddYears(-65))
            {
                return .05m;
            }

            return 0m;

        }

        public decimal CalculateDiscountForBirthday(Customer customer, decimal currentDiscount)
        {
            bool isBirthday = customer.DateOfBirth.HasValue &&
                    customer.DateOfBirth.Value.Month == DateTime.Today.Month &&
                    customer.DateOfBirth.Value.Day == DateTime.Today.Day;

            if (isBirthday) return currentDiscount + 0.10m;
            return currentDiscount;

        }

        private decimal CalcluateDiscountForLoyalCustomer(Customer customer)
        {
            if (customer.DateOfFirstPurchase.HasValue)
            {
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-15))
                {
                    return .15m;
                }
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-10))
                {
                    return .12m;
                }
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-5))
                {
                    return .10m;
                }
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-2))
                {
                    return .08m;
                }
                if (customer.DateOfFirstPurchase.Value < DateTime.Now.AddYears(-1))
                {
                    return .05m;
                }
            }
            return 0;
        }

        public decimal CalculateDiscountPercentage(Customer customer)
        {
            decimal discount = 0m;

            discount = Math.Max(discount, CalculateDiscountForFirstTimeCustomer(customer));
            discount = Math.Max(discount, CalcluateDiscountForLoyalCustomer(customer));
            discount = Math.Max(discount, CalculateDiscountForVeteran(customer));
            discount = Math.Max(discount, CalculateDiscountForSeniors(customer));
            discount = Math.Max(discount, CalculateDiscountForBirthday(customer, discount));

            return discount;

        }

    }
}
