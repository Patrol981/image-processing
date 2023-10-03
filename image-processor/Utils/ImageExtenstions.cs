using ImageProcessor.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ImageProcessor;

public static class ImageExtensions {
  public static void EnsureCreated(this Image image) {
    image.ImageOptions ??= new() {
      Brightness = 1,
      Saturation = 1.0f,
      Contrast = 1.0f
    };
  }

  public static Results<FileContentHttpResult, ContentHttpResult> GetResult(this Image image) {
    if (image.ImageReturnType == ImageReturnType.File) {
      return TypedResults.File(image.Data!, "image/png", "result.png");
    } else {
      return TypedResults.Content(image.ToBase64());
    }
  }

  public static string ToBase64(this Image image) {
    return Convert.ToBase64String(image.Data!);
  }
}
