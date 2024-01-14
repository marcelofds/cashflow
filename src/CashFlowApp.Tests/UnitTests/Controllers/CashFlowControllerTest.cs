using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using CashFlow.WebApi.Controllers;
using Moq;

namespace CashFlowApp.Tests.UnitTests.Controllers;

[TestFixture]
public class CashFlowControllerTest
{
    [SetUp]
    public void Setup()
    {
        _service = new Mock<ICashFlowService>();

        _controller = new CashFlowController(_service.Object);
    }

    private Mock<ICashFlowService> _service;
    private CashFlowController _controller;

    [Test]
    public async Task ConsolidateAsync_WithValidParams_ReturnsCashFlowSummarizingBills()
    {
        //Arrange
        var expected = new CashFlowAggDto();

        _service
            .Setup(s => s.ConsolidateAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(expected);

        //Act
        var actionResult = await _controller.ConsolidateAsync(It.IsAny<DateOnly>());

        //Assert
        Assert.That(actionResult.Value, Is.Not.Null);
    }
}