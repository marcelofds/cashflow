using System.Linq.Expressions;
using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using CashFlow.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace CashFlowApp.Tests.UnitTests.Controllers;

[TestFixture]
public class BillingToPayControllerTest
{
    [SetUp]
    public void Setup()
    {
        _service = new Mock<IBillingService>();
        _logger = new Mock<ILogger<BillingToPayController>>();

        _controller = new BillingToPayController(_service.Object, _logger.Object);
    }

    private Mock<IBillingService> _service;
    private Mock<ILogger<BillingToPayController>> _logger;
    private BillingToPayController _controller;

    [Test]
    public async Task GetBillToPayById_WithValidArgument_ReturnsCorrespondingBillToPayDto()
    {
        //Arrange
        var expected = new BillToPayDto();

        _service
            .Setup(s => s.GetBillingToPayByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(expected);

        //Act
        var actionResult = await _controller.GetBillToPayByIdAsync(It.IsAny<int>());

        //Assert
        Assert.That(actionResult.Value, Is.EqualTo(expected));
    }


    [Test]
    public async Task GetAll_WithoutParams_ReturnsAllBillsToPay()
    {
        //Arrange
        var expected = new List<BillToPayDto>();

        _service
            .Setup(s => s.GetAllBillToPayAsync())
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
        Expression<Func<IBillingService, Task>> deleteAsyncSetup = s => s.DeleteBillToPayAsync(It.IsAny<int>());
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
        Expression<Func<IBillingService, Task>> addBillToPayAsyncSetup =
            s => s.IncludeNewBillToPayAsync(It.IsAny<BillToPayInsertDto>());
        //Arrange
        _service.Setup(addBillToPayAsyncSetup);

        //Act
        var actionResult = await _controller.AddNewAsync(new BillToPayInsertDto());

        //Assert
        _service.Verify(addBillToPayAsyncSetup, Times.Once);
        Assert.That(actionResult, Is.Not.Null);
    }

    [Test]
    public async Task PutSkillAsync_WithValidArguments_InvokesServiceAndReturnsResponseResultWithMessage()
    {
        //Arrange
        Expression<Func<IBillingService, Task>> writeOffAsyntSetup =
            s => s.WriteOffBillToPayAsync(It.IsAny<BillToPayDto>());
        _service.Setup(writeOffAsyntSetup);

        //Act
        var actionResult = await _controller.WriteOffAsync(new BillToPayDto());

        //Assert
        _service.Verify(writeOffAsyntSetup, Times.Once);
        Assert.That(actionResult, Is.Not.Null);
    }
}