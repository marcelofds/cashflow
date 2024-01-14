using System.Linq.Expressions;
using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using CashFlow.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace CashFlowApp.Tests.UnitTests.Controllers;

[TestFixture]
public class BillingToReceiveControllerTest
{
    [SetUp]
    public void Setup()
    {
        _service = new Mock<IBillingService>();
        _logger = new Mock<ILogger<BillingToReceiveController>>();

        _controller = new BillingToReceiveController(_service.Object, _logger.Object);
    }

    private Mock<IBillingService> _service;
    private Mock<ILogger<BillingToReceiveController>> _logger;
    private BillingToReceiveController _controller;

    [Test]
    public async Task GetBillToReceiveById_WithValidArgument_ReturnsCorrespondingBillToReceiveDto()
    {
        //Arrange
        var expected = new BillToReceiveDto();

        _service
            .Setup(s => s.GetBillingToReceiveById(It.IsAny<int>()))
            .ReturnsAsync(expected);

        //Act
        var actionResult = await _controller.GetBillToReceiveByIdAsync(It.IsAny<int>());

        //Assert
        Assert.That(actionResult.Value, Is.EqualTo(expected));
    }


    [Test]
    public async Task GetAll_WithoutParams_ReturnsAllBillsToReceive()
    {
        //Arrange
        var expected = new List<BillToReceiveDto>();

        _service
            .Setup(s => s.GetAllBillToReceiveAsync())
            .ReturnsAsync(expected);

        //Act
        var actionResult = await _controller.GetAllAsync();

        //Assert
        Assert.That(actionResult.Value, Is.Not.Null);
    }

    [Test]
    public async Task DeleteAsync_WithValidArguments_InvokesServiceAndReturnsNoContent()
    {
        //Arrange
        Expression<Func<IBillingService, Task>> deleteAsyncSetup = s => s.DeleteBillToReceiveAsync(It.IsAny<int>());
        _service.Setup(deleteAsyncSetup);

        //Act
        var actionResult = await _controller.DeleteAsync(0);

        //Assert
        _service.Verify(deleteAsyncSetup, Times.Once);
        Assert.That(actionResult, Is.Not.Null);
    }


    [Test]
    public async Task AddNewAsync_WithValidArguments_InvokesServiceAndReturnsNoContent()
    {
        //Arrange
        Expression<Func<IBillingService, Task>> addBillToReceiveAsyncSetup =
            s => s.IncludeNewBillToReceiveAsync(It.IsAny<BillToReceiveInsertDto>());
        _service.Setup(addBillToReceiveAsyncSetup);

        //Act
        var actionResult = await _controller.AddNewAsync(new BillToReceiveInsertDto());

        //Assert
        _service.Verify(addBillToReceiveAsyncSetup, Times.Once);
        Assert.That(actionResult, Is.Not.Null);
    }

    [Test]
    public async Task PutSkillAsync_WithValidArguments_InvokesServiceAndReturnsResponseResultWithMessage()
    {
        //Arrange
        Expression<Func<IBillingService, Task>> writeOffAsyntSetup =
            s => s.WriteOffBillToReceiveAsync(It.IsAny<BillToReceiveDto>());
        _service.Setup(writeOffAsyntSetup);

        //Act
        var actionResult = await _controller.WriteOffAsync(new BillToReceiveDto());

        //Assert
        _service.Verify(writeOffAsyntSetup, Times.Once);
        Assert.That(actionResult, Is.Not.Null);
    }
}