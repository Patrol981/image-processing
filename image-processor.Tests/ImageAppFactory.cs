using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ImageProcessor.Tests;
public class ImageAppFactory : WebApplicationFactory<Program> {
  private readonly Action<IServiceCollection>? _serviceOverride;

  public ImageAppFactory(Action<IServiceCollection>? overrides = null) {
    _serviceOverride = overrides;
  }

  protected override IHost CreateHost(IHostBuilder builder) {
    if (_serviceOverride != null) {
      builder.ConfigureServices(_serviceOverride);
    }

    return base.CreateHost(builder);
  }
}
