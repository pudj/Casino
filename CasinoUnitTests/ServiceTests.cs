using Casino.Api.Domain.Entities;
using Casino.Api.Infrastructure.Persistence;
using Casino.Api.Infrastructure.Services;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUnitTests
{
    public class ServiceTests
    {
        private Mock<CasinoContext> context;
        private Mock<IResultService> resultService;

        [SetUp]
        public void Setup()
        {
            context = new Mock<CasinoContext>();
            resultService = new Mock<IResultService>();

        }

        [Test]
        public void GetResults()
        {
            resultService.Setup(x => x.GetResults().Result)
                .Returns(new List<Result> {
                    { new Result { Amount = 1, CreatedAt = DateTime.Now } }
                });



            Assert.IsNotNull(resultService.Object);

        }
    }
}
