namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDiscountService discountService = new DiscountService();
            InsuranceService insuranceService = new InsuranceService(discountService);

        }
    }
}
