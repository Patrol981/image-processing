using ImageProcessor.Models;
using FluentAssertions;
using System.Net.Http.Json;

namespace ImageProcessor.Tests;

public class ContrastTests : ImageTestBase {
  [Fact]
  public async void ContrastImage_ReturnsImage_WhenContrastMethodCalled() {
    var imageBase = ImageHelper.LoadBase();
    imageBase.ImageOptions!.Contrast = 0.5f;

    var result = await _imageService.Contrast(imageBase);

    result.Should().NotBeNull();
  }

  [Fact]
  public async void ContrastImage_ShouldHaveSameOptions_WhenReturningImageFromService() {
    var imageBase = ImageHelper.LoadBase();
    imageBase.ImageOptions!.Contrast = 0.5f;

    var result = await _imageService.Contrast(imageBase);

    result.ImageOptions.Should().NotBeNull();
    result.ImageOptions.Should().BeSameAs(imageBase.ImageOptions);
  }

  [Fact]
  public async void ContrastImage_ShouldUpdateOptions_WhenUsingParamWithoutOptions() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions = null;
    float contrast = 0.5f;

    imageBase.EnsureCreated();
    imageBase.ImageOptions.Should().NotBeNull();
    imageBase.ImageOptions!.Contrast = contrast;
    imageBase.ImageOptions!.Contrast.Should().NotBe(0.0f);
    imageBase.ImageOptions!.Contrast!.Should().Be(contrast);

    var result = await _imageService.Contrast(imageBase);
    result.ImageOptions.Should().NotBeNull();
    result.ImageOptions!.Contrast.Should().NotBe(0.0f);
    result.ImageOptions!.Contrast!.Should().Be(contrast);
  }

  [Fact]
  public async void ContrastImage_ReturnsBase64_WhenPostWithStreamFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions!.Contrast = 0.5f;

    var result = await _httpClient.PostAsJsonAsync("/contrastImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void ContrastImage_ReturnsBase64_WhenPostWithStreamFlagUsingParam() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.Stream);
    imageBase.ImageOptions = null;
    float contrast = 0.5f;

    var result = await _httpClient.PostAsJsonAsync($"/contrastImage/{contrast}", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
  }

  [Fact]
  public async void ContrastImage_ReturnsFile_WhenPostWithFileFlag() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);
    imageBase.ImageOptions!.Contrast = 0.5f;

    var result = await _httpClient.PostAsJsonAsync("/contrastImage", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }

  [Fact]
  public async void ContrastImage_ReturnsFile_WhenPostWithFileFlagUsingParam() {
    var imageBase = ImageHelper.LoadBase(ImageReturnType.File);
    imageBase.ImageOptions = null;
    float contrast = 0.5f;

    var result = await _httpClient.PostAsJsonAsync($"/contrastImage/{contrast}", imageBase);

    result.Content.Headers.ContentType.Should().NotBeNull();
    result.Content.Headers.ContentType!.MediaType.Should().Be("image/png");
  }
}
