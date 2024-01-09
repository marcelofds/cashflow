using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using CashFlow.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace CashFlow.Tests.Controllers;

[TestFixture]
public class CashFlowControllerTest
{

    private Mock<ICashFlowService> _service;
    private CashFlowController _controller;

    [SetUp]
    public void Setup()
    {
        _service = new Mock<ICashFlowService>();

        _controller = new CashFlowController(_service.Object);
    }

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