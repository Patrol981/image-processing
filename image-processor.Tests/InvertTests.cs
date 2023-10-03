using ImageProcessor.Models;
using FluentAssertions;
using System.Net.Http.Json;

namespace ImageProcessor.Tests;

public class InvertTests : ImageTestBase {
  [Fact]
  public async void InvertImage_ReturnsImage_WhenInvertMethodCalled() {
    var imageBase = ImageHelper.LoadBase();

    var result = await _imageService.Invert(imageBase);

    result.Should().NotBeNull();
  }

  [Fact]
  public async void InvertImage_ReturnsBase64_WhenPostWithStreamFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);

    var result = await _httpClient.PostAsJsonAsync("/invertImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void InvertImage_ReturnsFile_WhenPostWithFileFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);

    var result = await _httpClient.PostAsJsonAsync("/invertImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }
}
