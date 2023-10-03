using ImageProcessor.Models;

namespace ImageProcessor.Services;

public interface IImageService {
  Task<Image> Blur(Image image);
  Task<Image> Sepia(Image image);
  Task<Image> Contrast(Image image);
  Task<Image> AddBrightness(Image image);
  Task<Image> Grayscale(Image image);
  Task<Image> Invert(Image image);
  Task<Image> Saturate(Image image);
}