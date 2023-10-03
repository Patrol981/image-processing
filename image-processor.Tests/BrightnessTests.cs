using ImageProcessor.Models;
using FluentAssertions;
using System.Net.Http.Json;

namespace ImageProcessor.Tests;

public class BrightnessTests : ImageTestBase {
  [Fact]
  public async void BrightenImage_ReturnsImage_WhenBrightenMethodCalled() {
    var imageBase = ImageHelper.LoadBase();
    imageBase.ImageOptions!.Brightness = 100;

    var result = await _imageService.AddBrightness(imageBase);

    result.Should().NotBeNull();
  }

  [Fact]
  public async void BrightenImage_ShouldHaveSameOptions_WhenReturningImageFromService() {
    var imageBase = ImageHelper.LoadBase();
    imageBase.ImageOptions!.Brightness = 100;

    var result = await _imageService.AddBrightness(imageBase);

    result.ImageOptions.Should().NotBeNull();
    result.ImageOptions.Should().BeSameAs(imageBase.ImageOptions);
  }

  [Fact]
  public async void BrightenImage_ShouldUpdateOptions_WhenUsingParamWithoutOptions() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions = null;
    int brightness = 100;

    imageBase.EnsureCreated();
    imageBase.ImageOptions.Should().NotBeNull();
    imageBase.ImageOptions!.Brightness = brightness;
    imageBase.ImageOptions!.Brightness.Should().NotBe(0);
    imageBase.ImageOptions!.Brightness!.Should().Be(brightness);

    var result = await _imageService.AddBrightness(imageBase);
    result.ImageOptions.Should().NotBeNull();
    result.ImageOptions!.Brightness.Should().NotBe(0);
    result.ImageOptions!.Brightness!.Should().Be(brightness);
  }

  [Fact]
  public async void BrightenImage_ReturnsBase64_WhenPostWithStreamFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions!.Brightness = 100;

    var result = await _httpClient.PostAsJsonAsync("/brightenImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void BrightenImage_ReturnsBase64_WhenPostWithStreamFlagUsingParam() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions = null;
    int brightness = 100;

    var result = await _httpClient.PostAsJsonAsync($"/brightenImage/{brightness}", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void BrightenImage_ReturnsFile_WhenPostWithFileFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);
    imageBase.ImageOptions!.Brightness = 100;

    var result = await _httpClient.PostAsJsonAsync("/brightenImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }

  [Fact]
  public async void BrightenImage_ReturnsFile_WhenPostWithFileFlagUsingParam() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);
    imageBase.ImageOptions = null;
    int brightness = 100;

    var result = await _httpClient.PostAsJsonAsync($"/brightenImage/{brightness}", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }
}
