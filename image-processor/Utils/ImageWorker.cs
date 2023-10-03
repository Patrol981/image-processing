using ImageProcessor.Models;
using SkiaSharp;

namespace ImageProcessor;

public class ImageWorker : IDisposable {
  private readonly SKBitmap _originalImage;
  private readonly SKSurface _surface;
  private readonly SKPaint _paint;

  public ImageWorker(Image image) {
    _originalImage = SKBitmap.Decode(image.Data);
    _surface = SKSurface.Create(new SKImageInfo {
      Width = _originalImage.Width,
      Height = _originalImage.Height,
      ColorType = SKImageInfo.PlatformColorType,
      AlphaType = SKAlphaType.Premul
    });
    _paint = new();
  }

  public byte[] GetResult() {
    _surface.Canvas.DrawBitmap(_originalImage, 0, 0, _paint);
    _surface.Canvas.Flush();

    var snap = _surface.Snapshot().Encode();
    var result = snap.AsSpan().ToArray();
    snap.Dispose();
    return result;
  }

  public nint Pixels {
    get { return _originalImage.GetPixels(); }
    set { _originalImage.SetPixels(value); }
  }

  public SKImageFilter PaintFilter {
    set { _paint.ImageFilter = value; }
  }

  public SKColor PixelAt(int x, int y) => _originalImage.GetPixel(x, y);
  public void SetPixel(int x, int y, SKColor color) {
    _originalImage.SetPixel(x, y, color);
  }

  public int Width => _originalImage.Width;
  public int Height => _originalImage.Height;

  public void Dispose() {
    _paint.Dispose();
    _surface.Dispose();
    _originalImage.Dispose();
  }
}
