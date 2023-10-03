namespace ImageProcessor.Endpoints;

public interface IEndpoint {
  public void DefineEndpoints(WebApplication app);
  public void DefineServices(IServiceCollection services);
}