using NetArchTest.Rules;

namespace CashFlowApp.Tests.ArchitectTests;
[TestFixture]
public class ArchitectureTests
{
    private static readonly string DomainNamespace = typeof(CashFlow.Domain.BaseDefinitions.Entity).Assembly.GetName().Name!;
    private static readonly string ApplicationNamespace = typeof(CashFlow.Application.Utils.Settings).Assembly.GetName().Name!;
    private static readonly string PersistenceNamespace = typeof(CashFlow.Data.Context.ContextBase).Assembly.GetName().Name!;
    private static readonly string PresentationNamespace = typeof(CashFlow.WebApi.Controllers.HomeController).Assembly.GetName().Name!;
    private static readonly string InversionOfControlNamespace = typeof(CashFlow.IoC.Factories.ContainerFactory).Assembly.GetName().Name!;

    [Test]
    public void Domain_Should_Not_Depends_Others()
    {
        //Arrange
        var assembly = typeof(CashFlow.Domain.BaseDefinitions.Entity).Assembly;
        var others = new[]
        {
            ApplicationNamespace,
            InversionOfControlNamespace,
            PersistenceNamespace,
            PresentationNamespace
        };
        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(others)
            .GetResult();

        //Assert
        Assert.That(result.IsSuccessful, Is.EqualTo(true));
    }
    
    [Test]
    public void Application_Should_Not_Depends_Others()
    {
        //Arrange
        var assembly = typeof(CashFlow.Application.Utils.Settings).Assembly;
        var others = new[]
        {
            InversionOfControlNamespace,
            PersistenceNamespace,
            PresentationNamespace
        };
        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(others)
            .GetResult();

        //Assert
        Assert.That(result.IsSuccessful, Is.EqualTo(true));
    }
    
    [Test]
    public void Data_Should_Not_Depends_Others()
    {
        //Arrange
        var assembly = typeof(CashFlow.Data.Context.ContextBase).Assembly;
        var others = new[]
        {
            ApplicationNamespace,
            InversionOfControlNamespace,
            PresentationNamespace
        };
        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(others)
            .GetResult();

        //Assert
        Assert.That(result.IsSuccessful, Is.EqualTo(true));
    } 
    
    [Test]
    public void InversionOfControl_Should_Not_Depends_Others()
    {
        //Arrange
        var assembly = typeof(CashFlow.IoC.Factories.ContainerFactory).Assembly;
        var others = new[]
        {
            DomainNamespace,
            PresentationNamespace
        };
        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(others)
            .GetResult();

        //Assert
        Assert.That(result.IsSuccessful, Is.EqualTo(true));
    }
    
    [Test]
    public void Presentation_Should_Not_Depends_Others()
    {
        //Arrange
        var assembly = typeof(CashFlow.WebApi.Controllers.HomeController).Assembly;
        var others = new[]
        {
            DomainNamespace,
            PersistenceNamespace,
            ApplicationNamespace
        };
        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(others)
            .GetResult();

        //Assert
        Assert.That(result.IsSuccessful, Is.EqualTo(true));
    }
}