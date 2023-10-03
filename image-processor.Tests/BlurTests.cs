using ImageProcessor.Models;
using FluentAssertions;
using System.Net.Http.Json;

namespace ImageProcessor.Tests;

public class BlurTests : ImageTestBase {
  [Fact]
  public async void BlurImage_ReturnsImage_WhenBlurMethodCalled() {
    var imageBase = ImageHelper.LoadBase();

    var result = await _imageService.Blur(imageBase);

    result.Should().NotBeNull();
  }

  [Fact]
  public async void BlurImage_ReturnsBase64_WhenPostWithStreamFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);

    var result = await _httpClient.PostAsJsonAsync("/blurImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void BlurImage_ReturnsFile_WhenPostWithFileFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);

    var result = await _httpClient.PostAsJsonAsync("/blurImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }
}