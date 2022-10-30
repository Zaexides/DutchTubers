using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DutchTubers.Server.Services
{
    [TestFixture]
    public class DateTimeProviderTests
    {
        [Test]
        public void Now()
        {
            var fixture = new DateTimeProvider();
            var actual = DateTime.UtcNow;
            var obtained = fixture.Now;
            var difference = actual - obtained;
            Assert.IsTrue(difference.Milliseconds < 1);
        }
    }
}
