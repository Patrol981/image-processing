using ImageProcessor.Models;
using FluentAssertions;
using System.Net.Http.Json;

namespace ImageProcessor.Tests;

public class GrayscaleTests : ImageTestBase {
  [Fact]
  public async void GrayscaleImage_ReturnsImage_WhenGrayscaleMethodCalled() {
    var imageBase = ImageHelper.LoadBase();

    var result = await _imageService.Grayscale(imageBase);

    result.Should().NotBeNull();
  }

  [Fact]
  public async void GrayscaleImage_ReturnsBase64_WhenPostWithStreamFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);

    var result = await _httpClient.PostAsJsonAsync("/grayscaleImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void GrayscaleImage_ReturnsFile_WhenPostWithFileFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);

    var result = await _httpClient.PostAsJsonAsync("/grayscaleImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }
}