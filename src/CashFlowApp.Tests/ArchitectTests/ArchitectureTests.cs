using CashFlow.Application.Utils;
using CashFlow.Data.Context;
using CashFlow.Domain.BaseDefinitions;
using CashFlow.IoC.Factories;
using CashFlow.WebApi.Controllers;
using NetArchTest.Rules;

namespace CashFlowApp.Tests.ArchitectTests;

[TestFixture]
public class ArchitectureTests
{
    private static readonly string DomainNamespace = typeof(Entity).Assembly.GetName().Name!;
    private static readonly string ApplicationNamespace = typeof(Settings).Assembly.GetName().Name!;
    private static readonly string PersistenceNamespace = typeof(ContextBase).Assembly.GetName().Name!;
    private static readonly string PresentationNamespace = typeof(HomeController).Assembly.GetName().Name!;
    private static readonly string InversionOfControlNamespace = typeof(ContainerFactory).Assembly.GetName().Name!;

    [Test]
    public void Domain_Should_Not_Depends_Others()
    {
        //Arrange
        var assembly = typeof(Entity).Assembly;
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
        var assembly = typeof(Settings).Assembly;
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
        var assembly = typeof(ContextBase).Assembly;
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
        var assembly = typeof(ContainerFactory).Assembly;
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
        var assembly = typeof(HomeController).Assembly;
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