using ImageProcessor.Models;
using FluentAssertions;
using System.Net.Http.Json;


namespace ImageProcessor.Tests;

public class SaturateTests : ImageTestBase {
  [Fact]
  public async void SaturateImage_ReturnsImage_WhenSaturateMethodCalled() {
    var imageBase = ImageHelper.LoadBase();
    imageBase.ImageOptions!.Saturation = 2.0f;

    var result = await _imageService.Saturate(imageBase);

    result.Should().NotBeNull();
  }

  [Fact]
  public async void SaturateImage_ShouldHaveSameOptions_WhenReturningImageFromService() {
    var imageBase = ImageHelper.LoadBase();
    imageBase.ImageOptions!.Saturation = 2.0f;

    var result = await _imageService.Saturate(imageBase);

    result.ImageOptions.Should().NotBeNull();
    result.ImageOptions.Should().BeSameAs(imageBase.ImageOptions);
  }

  [Fact]
  public async void SaturateImage_ShouldUpdateOptions_WhenUsingParamWithoutOptions() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions = null;
    float saturation = 2.0f;

    imageBase.EnsureCreated();
    imageBase.ImageOptions.Should().NotBeNull();
    imageBase.ImageOptions!.Saturation = saturation;
    imageBase.ImageOptions!.Saturation.Should().NotBe(0.0f);
    imageBase.ImageOptions!.Saturation!.Should().Be(saturation);

    var result = await _imageService.Saturate(imageBase);
    result.ImageOptions.Should().NotBeNull();
    result.ImageOptions!.Saturation.Should().NotBe(0.0f);
    result.ImageOptions!.Saturation!.Should().Be(saturation);
  }

  [Fact]
  public async void SaturateImage_ReturnsBase64_WhenPostWithStreamFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions!.Saturation = 2.0f;

    var result = await _httpClient.PostAsJsonAsync("/saturateImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void SaturateImage_ReturnsBase64_WhenPostWithStreamFlagUsingParam() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions = null;
    float saturation = 2.0f;

    var result = await _httpClient.PostAsJsonAsync($"/saturateImage/{saturation}", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void SaturateImage_ReturnsFile_WhenPostWithFileFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);
    imageBase.ImageOptions!.Saturation = 2.0f;

    var result = await _httpClient.PostAsJsonAsync("/saturateImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }

  [Fact]
  public async void SaturateImage_ReturnsFile_WhenPostWithFileFlagUsingParam() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);
    imageBase.ImageOptions = null;
    float saturation = 2.0f;

    var result = await _httpClient.PostAsJsonAsync($"/saturateImage/{saturation}", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }
}
