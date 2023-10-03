using ImageProcessor.Models;

using SkiaSharp;

namespace ImageProcessor.Services;

public class ImageService : IImageService {

  public Task<Image> Blur(Image image) {
    using var worker = new ImageWorker(image) {
      PaintFilter = SKImageFilter.CreateBlur(10.0f, 10.0f)
    };
    image.Data = worker.GetResult();
    return Task.FromResult(image);
  }
  public Task<Image> Sepia(Image image) {
    using var worker = new ImageWorker(image);
    for (int x = 0; x < worker.Width; x++) {
      for (int y = 0; y < worker.Height; y++) {
        var color = worker.PixelAt(x, y);

        int sepiaR = (int)(color.Red * 0.393 + color.Green * 0.769 + color.Blue * 0.189);
        int sepiaG = (int)(color.Red * 0.349 + color.Green * 0.686 + color.Blue * 0.168);
        int sepiaB = (int)(color.Red * 0.272 + color.Green * 0.534 + color.Blue * 0.131);

        sepiaR = Math.Min(255, sepiaR);
        sepiaG = Math.Min(255, sepiaG);
        sepiaB = Math.Min(255, sepiaB);

        var sepia = new SKColor((byte)sepiaR, (byte)sepiaG, (byte)sepiaB);
        worker.SetPixel(x, y, sepia);
      }
    }
    image.Data = worker.GetResult();
    return Task.FromResult(image);
  }

  public Task<Image> Contrast(Image image) {
    using var worker = new ImageWorker(image);
    for (int x = 0; x < worker.Width; x++) {
      for (int y = 0; y < worker.Height; y++) {
        var color = worker.PixelAt(x, y);

        var red = (byte)Math.Max(0, Math.Min(255, (color.Red - 128) * (float)image.ImageOptions!.Contrast!) + 128);
        var green = (byte)Math.Max(0, Math.Min(255, (color.Green - 128) * (float)image.ImageOptions!.Contrast!) + 128);
        var blue = (byte)Math.Max(0, Math.Min(255, (color.Blue - 128) * (float)image.ImageOptions!.Contrast!) + 128);

        var contrast = new SKColor(red, green, blue);
        worker.SetPixel(x, y, contrast);
      }
    }
    image.Data = worker.GetResult();
    return Task.FromResult(image);
  }

  public Task<Image> AddBrightness(Image image) {
    using var worker = new ImageWorker(image);
    for (int x = 0; x < worker.Width; x++) {
      for (int y = 0; y < worker.Height; y++) {
        var color = worker.PixelAt(x, y);

        var red = (byte)Math.Max(0, Math.Min(255, color.Red + (float)image.ImageOptions!.Brightness!));
        var green = (byte)Math.Max(0, Math.Min(255, color.Green + (float)image.ImageOptions!.Brightness!));
        var blue = (byte)Math.Max(0, Math.Min(255, color.Blue + (float)image.ImageOptions!.Brightness!));

        var brightness = new SKColor(red, green, blue);
        worker.SetPixel(x, y, brightness);
      }
    }
    image.Data = worker.GetResult();
    return Task.FromResult(image);
  }

  public Task<Image> Grayscale(Image image) {
    using var worker = new ImageWorker(image);
    for (int x = 0; x < worker.Width; x++) {
      for (int y = 0; y < worker.Height; y++) {
        var color = worker.PixelAt(x, y);
        var grayscale = (byte)((color.Red + color.Green + color.Blue) / 3);
        var grayscaleColor = new SKColor(grayscale, grayscale, grayscale);
        worker.SetPixel(x, y, grayscaleColor);
      }
    }
    image.Data = worker.GetResult();
    return Task.FromResult(image);
  }

  public Task<Image> Invert(Image image) {
    using var worker = new ImageWorker(image);
    for (int x = 0; x < worker.Width; x++) {
      for (int y = 0; y < worker.Height; y++) {
        var color = worker.PixelAt(x, y);

        byte red = (byte)(255 - color.Red);
        byte green = (byte)(255 - color.Green);
        byte blue = (byte)(255 - color.Blue);

        var invert = new SKColor(red, green, blue);
        worker.SetPixel(x, y, invert);
      }
    }
    image.Data = worker.GetResult();
    return Task.FromResult(image);
  }

  public Task<Image> Saturate(Image image) {
    using var worker = new ImageWorker(image);
    for (int x = 0; x < worker.Width; x++) {
      for (int y = 0; y < worker.Height; y++) {
        var color = worker.PixelAt(x, y);

        var avg = (byte)((color.Red + color.Green + color.Blue) / 3);

        int deltaR = color.Red - avg;
        int deltaG = color.Green - avg;
        int deltaB = color.Blue - avg;

        var red = (byte)Math.Max(0, Math.Min(255, avg + deltaR * (float)image.ImageOptions!.Saturation!));
        var green = (byte)Math.Max(0, Math.Min(255, avg + deltaG * (float)image.ImageOptions!.Saturation!));
        var blue = (byte)Math.Max(0, Math.Min(255, avg + deltaB * (float)image.ImageOptions!.Saturation!));

        var invert = new SKColor(red, green, blue);
        worker.SetPixel(x, y, invert);
      }
    }
    image.Data = worker.GetResult();
    return Task.FromResult(image);
  }
}
