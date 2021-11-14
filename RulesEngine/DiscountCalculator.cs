using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine
{
    public interface IDiscountRule
    {
        decimal CalculateDiscount(Customer customer, decimal currentDiscount);
    }

    public class FirstTimeCustomerRule : IDiscountRule
    {
        public decimal CalculateDiscount(Customer customer, decimal currentDiscount)
        {
            if (!customer.DateOfFirstPurchase.HasValue)
            {
                return .15m;
            }
            return 0m;
        }
    }

    public class VeteranDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(Customer customer, decimal currentDiscount)
        {
            if (customer.IsVeteran)
            {
                return .10m;
            }

            return 0m;
        }
    }

    public class SeniorDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(Customer customer, decimal currentDiscount)
        {
            if (customer.DateOfBirth < DateTime.Now.AddYears(-65))
            {
                return .05m;
            }

            return 0m;
        }
    }

    public class LoyalCustomerDisountRule : IDiscountRule
    {
        public decimal CalculateDiscount(Customer customer, decimal currentDiscount)
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
    }

    public class BirthdayDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(Customer customer, decimal currentDiscount)
        {
            bool isBirthday = customer.DateOfBirth.HasValue &&
                    customer.DateOfBirth.Value.Month == DateTime.Today.Month &&
                    customer.DateOfBirth.Value.Day == DateTime.Today.Day;

            if (isBirthday) return currentDiscount + 0.10m;
            return currentDiscount;
        }
    }


    public class DiscountRuleEngine
    {
        List<IDiscountRule> rules = new List<IDiscountRule>();

        public DiscountRuleEngine(IEnumerable<IDiscountRule> rule)
        {
            rules.AddRange(rules);
        }

        public decimal CalculateDiscountPercentage(Customer customer)
        {
            decimal discount = 0m;
            foreach (var rule in rules)
            {
                discount = Math.Max(discount, rule.CalculateDiscount(customer, discount));
            }
            return discount;
        }
    }

    public class DiscountCalculator
    {

        public decimal CalculateDiscountPercentage(Customer customer)
        {
            decimal discount = 0m;

            discount = Math.Max(discount, new FirstTimeCustomerRule().CalculateDiscount(customer, discount));
            discount = Math.Max(discount, new LoyalCustomerDisountRule().CalculateDiscount(customer, discount));
            discount = Math.Max(discount, new VeteranDiscountRule().CalculateDiscount(customer, discount));
            discount = Math.Max(discount, new SeniorDiscountRule().CalculateDiscount(customer, discount));
            discount = Math.Max(discount, new BirthdayDiscountRule().CalculateDiscount(customer, discount));

            return discount;

        }

    }
}
