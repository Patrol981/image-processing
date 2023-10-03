using ImageProcessor.Models;
using ImageProcessor.Services;
using FluentAssertions;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace ImageProcessor.Tests;

public class SepiaTests : ImageTestBase {
  [Fact]
  public async void SepiaImage_ReturnsImage_WhenSepiaMethodCalled() {
    var imageBase = ImageHelper.LoadBase();

    var result = await _imageService.Sepia(imageBase);

    result.Should().NotBeNull();
  }

  [Fact]
  public async void SepiaImage_ReturnsBase64_WhenPostWithStreamFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);

    var result = await _httpClient.PostAsJsonAsync("/sepiaImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void SepiaImage_ReturnsFile_WhenPostWithFileFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);

    var result = await _httpClient.PostAsJsonAsync("/sepiaImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }
}
