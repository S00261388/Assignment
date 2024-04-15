using Moq;
using NUnit.Framework;

namespace Assignment
{
    [TestFixture]
    public class InsuranceServiceTests
    {

        private Mock<IDiscountService> _discountServiceMock;
        private InsuranceService _insuranceService;
        private const double DiscountRate = 0.9; // Assuming the discount rate is 0.9

        [SetUp]
        public void SetUp()
        {
            _discountServiceMock = new Mock<IDiscountService>();
            _discountServiceMock.Setup(service => service.GetDiscount()).Returns(DiscountRate);
            _insuranceService = new InsuranceService(_discountServiceMock.Object);
        }

        [Test]
        public void CalcPremium_WhenRuralAndUnder18_ShouldReturnZero()
        {
            double premium = _insuranceService.CalcPremium(17, "rural");
            Assert.That(premium, Is.EqualTo(0.0));
        }

        [Test]
        public void CalcPremium_WhenRuralAndBetween18And29_ShouldReturnFive()
        {
            double premium = _insuranceService.CalcPremium(25, "rural");
            Assert.That(premium, Is.EqualTo(5.0));
        }

        [Test]
        public void CalcPremium_WhenRuralAndOver30AndUnder50_ShouldReturnTwoPointFive()
        {
            double premium = _insuranceService.CalcPremium(31, "rural");
            Assert.That(premium, Is.EqualTo(2.5));
        }

        [Test]
        public void CalcPremium_WhenRuralAndOver50_ShouldReturnDiscountedRate()
        {
            double premium = _insuranceService.CalcPremium(50, "rural");
            double expected = 2.5 * DiscountRate;
            Assert.That(premium, Is.EqualTo(expected));
        }

        [Test]
        public void CalcPremium_WhenUrbanAndUnder18_ShouldReturnZero()
        {
            double premium = _insuranceService.CalcPremium(17, "urban");
            Assert.That(premium, Is.EqualTo(0.0));
        }

        [Test]
        public void CalcPremium_WhenUrbanAndBetween18And35AndUnder50_ShouldReturnSix()
        {
            double premium = _insuranceService.CalcPremium(25, "urban");
            Assert.That(premium, Is.EqualTo(6.0));
        }

        [Test]
        public void CalcPremium_WhenUrbanAndOver35AndUnder50_ShouldReturnFive()
        {
            double premium = _insuranceService.CalcPremium(36, "urban");
            Assert.That(premium, Is.EqualTo(5.0));
        }

        [Test]
        public void CalcPremium_WhenUrbanAndOver50_ShouldReturnDiscountedRate()
        {
            double premium = _insuranceService.CalcPremium(50, "urban");
            double expected = 5.0 * DiscountRate;
            Assert.That(premium, Is.EqualTo(expected));
        }

        [Test]
        public void CalcPremium_WhenLocationNotRuralOrUrbanAndUnder50_ShouldReturnZero()
        {
            double premium = _insuranceService.CalcPremium(25, "suburban");
            Assert.That(premium, Is.EqualTo(0.0));
        }

        [Test]
        public void CalcPremium_WhenLocationNotRuralOrUrbanAndOver50_ShouldReturnZero()
        {
            double premium = _insuranceService.CalcPremium(50, "suburban");
            Assert.That(premium, Is.EqualTo(0.0));
        }
    }
}
