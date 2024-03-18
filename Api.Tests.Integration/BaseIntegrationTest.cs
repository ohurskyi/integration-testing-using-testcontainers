namespace Api.Tests.Integration;

public abstract class BaseIntegrationTest : IClassFixture<DevicesWebApiFactory>
{
    private readonly DevicesWebApiFactory _webApiFactory;

    protected BaseIntegrationTest(DevicesWebApiFactory webApiFactory)
    {
        _webApiFactory = webApiFactory;
    }

    protected HttpClient HttpClient => _webApiFactory.CreateClient();

    protected IServiceScopeFactory ServiceScopeFactory =>
        _webApiFactory.Services.GetRequiredService<IServiceScopeFactory>();
}