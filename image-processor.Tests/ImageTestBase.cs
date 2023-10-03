using ImageProcessor.Models;
using ImageProcessor.Services;
using FluentAssertions;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace ImageProcessor.Tests;

public abstract class ImageTestBase {
  protected readonly IImageService _imageService;
  protected readonly ImageAppFactory _factory;
  protected readonly HttpClient _httpClient;

  public ImageTestBase() {
    _factory = new ImageAppFactory(x => {
      x.AddSingleton<IImageService, ImageService>();
    });
    _httpClient = _factory.CreateClient();
    _imageService = _factory.Services.GetRequiredService<IImageService>();
  }
}
