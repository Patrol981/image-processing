using ImageProcessor.Models;

namespace ImageProcessor.Tests;

public static class ImageHelper {
  public static string LoadAsBase64(string imageName) {
    var bytes = File.ReadAllBytes($"./mocks/{imageName}.png");
    return Convert.ToBase64String(bytes);
  }

  public static byte[] Base64ToBytes(string data) {
    return Convert.FromBase64String(data);
  }

  public static Image LoadImage(string imageName, ImageReturnType imageReturnType) {
    var bytes = File.ReadAllBytes($"./mocks/{imageName}.png");
    var image = new Image {
      Data = bytes,
      ImageOptions = new(),
      ImageReturnType = imageReturnType
    };
    image.ImageOptions.Contrast = 0;
    image.ImageOptions.Saturation = 0;
    image.ImageOptions.Brightness = 0;
    return image;
  }

  public static Image LoadBase(ImageReturnType imageReturnType = ImageReturnType.File) {
    return LoadImage("base", imageReturnType);
  }
}
