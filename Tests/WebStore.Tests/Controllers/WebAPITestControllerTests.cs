using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.Api;
using Xunit.Sdk;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class WebAPITestControllerTests
    {
        private WebAPITestController _Controller;

        [TestInitialize]
        public void Initialize()
        {
            var value_service_mock = new Mock<IValuesService>();

            value_service_mock
                .Setup(service => service.GetAsync())
                .ReturnsAsync(new[] { "123", "456", "789" });

            _Controller = new WebAPITestController(value_service_mock.Object);
        }

        [TestMethod]
        public async Task Index_Method_Returns_View_With_Values()
        {
            var result = await _Controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

            const int expected_count = 3;
            Assert.Equal(expected_count, model.Count());
        }
    }
}
