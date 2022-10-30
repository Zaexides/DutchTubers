using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DutchTubers.Server.Services
{
    [TestFixture]
    public class RandomProviderTests
    {
        //NOTE Good luck testing this.
        [Test]
        public void Next()
        {
            var fixture = new RandomProvider();
            var randomNumbers = new int[]
            {
                fixture.Next(),
                fixture.Next(),
                fixture.Next(),
                fixture.Next(),
                fixture.Next(),
                fixture.Next(),
                fixture.Next(),
                fixture.Next(),
                fixture.Next(),
                fixture.Next(),
            }.Distinct();

            try
            {
                Assert.Greater(randomNumbers.Count(), 1);
            }
            catch (AssertionException e)
            {
                Assert.Inconclusive("No distinct numbers were found, this could be by pure chance. Another test run recommended.", e);
            }
        }
    }
}
