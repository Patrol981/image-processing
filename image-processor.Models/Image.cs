namespace ImageProcessor.Models;

public class Image {
  public byte[]? Data { get; set; }
  public ImageOptions? ImageOptions { get; set; }
  public ImageReturnType ImageReturnType { get; set; } = ImageReturnType.File;
}