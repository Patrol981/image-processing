namespace ImageProcessor.Utils;

public static class StreamExtensions {
  public static async Task<byte[]> ToByteStream(this Stream stream) {
    Console.WriteLine(stream.Length);
    var memStream = new MemoryStream();
    await memStream.CopyToAsync(stream);
    return memStream.ToArray();
  }
}
