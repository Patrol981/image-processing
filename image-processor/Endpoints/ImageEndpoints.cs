using ImageProcessor.Models;
using ImageProcessor.Services;
using ImageResult = Microsoft.AspNetCore.Http.HttpResults.Results<
                                                Microsoft.AspNetCore.Http.HttpResults.FileContentHttpResult,
                                                Microsoft.AspNetCore.Http.HttpResults.ContentHttpResult
                                              >;

namespace ImageProcessor.Endpoints;

public class ImageEndpoints : IEndpoint {
  public void DefineEndpoints(WebApplication app) {
    app.MapPost("/blurImage", BlurImage);
    app.MapPost("/sepiaImage", SepiaImage);
    app.MapPost("/contrastImage", ContrastImage);
    app.MapPost("/contrastImage/{value}", ContrastImageParam);
    app.MapPost("/brightenImage", BrightenImage);
    app.MapPost("/brightenImage/{value}", BrightenImageParam);
    app.MapPost("/grayscaleImage", GrayscaleImage);
    app.MapPost("/invertImage", InvertImage);
    app.MapPost("/saturateImage", SaturateImage);
    app.MapPost("/saturateImage/{value}", SaturateImageParam);
  }

  public async Task<ImageResult> BlurImage(IImageService imageService, Image image) {
    var result = await imageService.Blur(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> SepiaImage(IImageService imageService, Image image) {
    var result = await imageService.Sepia(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> ContrastImage(IImageService imageService, Image image) {
    image.EnsureCreated();
    var result = await imageService.Contrast(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> ContrastImageParam(IImageService imageService, float value, Image image) {
    image.EnsureCreated();
    image.ImageOptions!.Contrast = value;
    var result = await imageService.Contrast(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> BrightenImage(IImageService imageService, Image image) {
    image.EnsureCreated();
    var result = await imageService.AddBrightness(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> BrightenImageParam(IImageService imageService, int value, Image image) {
    image.EnsureCreated();
    image.ImageOptions!.Brightness = value;
    var result = await imageService.AddBrightness(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> GrayscaleImage(IImageService imageService, Image image) {
    var result = await imageService.Grayscale(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> InvertImage(IImageService imageService, Image image) {
    var result = await imageService.Invert(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> SaturateImage(IImageService imageService, Image image) {
    image.EnsureCreated();
    var result = await imageService.Saturate(image);
    return result.GetResult();
  }

  internal async Task<ImageResult> SaturateImageParam(IImageService imageService, float value, Image image) {
    image.EnsureCreated();
    image.ImageOptions!.Saturation = value;
    var result = await imageService.Saturate(image);
    return result.GetResult();
  }

  public void DefineServices(IServiceCollection services) {
    services.AddSingleton<IImageService, ImageService>();
  }
}