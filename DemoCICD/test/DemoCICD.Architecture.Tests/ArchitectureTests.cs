using DemoCICD.Contract.Abstractions.Message;
using FluentAssertions;
using NetArchTest.Rules;

namespace DemoCICD.Architecture.Tests;

public class ArchitectureTests
{
    #region Tên namespaces
    private const string DomainNamespace = "DemoCICD.Domain";
    private const string ApplicationNamespace = "DemoCICD.Application";
    private const string InfrastructureNamespace = "DemoCICD.Infrastructure";
    private const string PersistenceNamespace = "DemoCICD.Persistence";
    private const string PresentationNamespace = "DemoCICD.Presentation";
    private const string ApiNamespace = "DemoCICD.API";
    #endregion

    #region =============== Infrastructure Leyer ===============
    /// <summary>
    /// Domain không được tham chiếu đến các namespaces tương ứng
    /// </summary>
    [Fact]
    public void Domain_Should_No_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Domain.AssemblyReference.Assembly;

        // Những domain không được phép tham chiếu tới
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            // PersistenceNamespace, // Lí do rem lại là để phục vụ cơ chế transaction _context
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly)
                              .ShouldNot() // Không cho phép
                              .HaveDependencyOnAny(otherProjects) // Chỉ cần tham chiếu đến 1 trong những project thì => Error
                              .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Application không được tham chiếu đến các namespaces tương ứng
    /// </summary>
    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            //PersistenceNamespace, // Due to Implement sort multi columns by apply RawQuery with EntityFramework
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Infrastructure không được tham chiếu đến các namespaces tương ứng
    /// </summary>
    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastructure.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Persistence không được tham chiếu đến các namespaces tương ứng
    /// </summary>
    [Fact]
    public void Persistence_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Persistence.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Presentation không được tham chiếu đến các namespaces tương ứng
    /// </summary>
    [Fact]
    public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Presentation.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion =============== Infrastructure Layer ===============

    #region ======================= Command =======================
    /// <summary>
    /// Class kế thừa ICommand thì cần có key sealed (mục đích tăng performent)
    /// </summary>
    [Fact]
    public void Command_Should_Have_BeSealed()
    {
        // Arrange
        // Truy cập Assembly Aplication
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommand))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Class kế thừa ICommand thì cần có key sealed (mục đích tăng performent), Có tham số
    /// </summary>
    [Fact]
    public void CommandT_Should_Have_BeSealed()
    {
        // Arrange
        // Truy cập Assembly Aplication
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Class kế thừa ICommandHandler thì cần có key sealed (mục đích tăng performent), Có tham số
    /// </summary>
    [Fact]
    public void CommandHandler_Should_Have_BeSealed()
    {
        // Arrange
        // Truy cập Assembly Aplication
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Class kế thừa ICommandHandler thì cần có key sealed (mục đích tăng performent), Có tham số
    /// </summary>
    [Fact]
    public void CommandHandlerT_Should_Have_BeSealed()
    {
        // Arrange
        // Truy cập Assembly Aplication
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Kiểm tra tên của class implement interface có đặt đúng convention (chữ cuối là Command)
    /// </summary>
    //[Fact]
    //public void Command_Should_Have_NamingConventionEndingCommand()
    //{
    //    // Arrange
    //    // Tham chiếu đến tầng Application
    //    var assembly = Application.AssemblyReference.Assembly;

    //    // Act
    //    var testResult = Types
    //        .InAssembly(assembly) // Truy cập assembly
    //        .That()
    //        .ImplementInterface(typeof(ICommand)) // Lựa chọn interface cần xử lí
    //        .Should().HaveNameEndingWith("Command")
    //        .GetResult();

    //    // Assert
    //    testResult.IsSuccessful.Should().BeTrue();
    //}

    /// <summary>
    /// Kiểm tra tên của class implement interface có đặt đúng convention (chữ cuối là Command), Có tham số
    /// </summary>
    //[Fact]
    //public void CommandT_Should_Have_NamingConventionEndingCommand()
    //{
    //    // Arrange
    //    // Tham chiếu đến tầng Application
    //    var assembly = Application.AssemblyReference.Assembly;

    //    // Act
    //    var testResult = Types
    //        .InAssembly(assembly) // Truy cập assembly
    //        .That()
    //        .ImplementInterface(typeof(ICommand<>)) // Lựa chọn interface cần xử lí (<> Có tham số)
    //        .Should().HaveNameEndingWith("Command")
    //        .GetResult();

    //    // Assert
    //    testResult.IsSuccessful.Should().BeTrue();
    //}

    /// <summary>
    /// Kiểm tra tên của class implement interface có đặt đúng convention (chữ cuối là Command), Cos1 tham số
    /// </summary>
    [Fact]
    public void CommandHandler_Should_Have_NamingConventionEndingCommandHandler()
    {
        // Arrange
        // Tham chiếu đến tầng Application
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly) // Truy cập assembly
            .That()
            .ImplementInterface(typeof(ICommandHandler<>)) // Lựa chọn interface cần xử lí (<> để nói là có 1 tham số)
            .Should().HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Kiểm tra tên của class implement interface có đặt đúng convention (chữ cuối là Command), Có 2 tham số
    /// </summary>
    [Fact]
    public void CommandHandlerT_Should_Have_NamingConventionEndingCommandHandler()
    {
        // Arrange
        // Tham chiếu đến tầng Application
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly) // Truy cập assembly
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>)) // Lựa chọn interface cần xử lí (<> để nói là có 2 tham số)
            .Should().HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    #endregion

    #region =============== Query ===============

    [Fact]
    public void Query_Should_Have_NamingConventionEndingQuery()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should().HaveNameEndingWith("Query")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_Should_Have_NamingConventionEndingQueryHandler()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion End Query
}
