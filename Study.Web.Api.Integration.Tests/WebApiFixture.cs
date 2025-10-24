using Microsoft.AspNetCore.Mvc.Testing;

[assembly: AssemblyFixture(typeof(Study.Web.Api.Integration.Tests.WebApiFixture))]

namespace Study.Web.Api.Integration.Tests;
public sealed class WebApiFixture : IDisposable, IAsyncDisposable
{
    public readonly WebApplicationFactory<Program> Factory = new();

    public void Dispose()
    {
        Factory.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await Factory.DisposeAsync();
    }
}