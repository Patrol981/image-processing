
# Image Processor

## Simple Web API for processing images

### Available endpoints:
#### POST
``
/blurImage
``
``
/sepiaImage
``
``
/contrastImage
``
``
/contrastImage/{value}
``
``
/brightenImage
``
``
/brightenImage/{value}
``
``
/grayscaleImage
``
``
/invertImage
``
``
/saturateImage
``
``
/saturateImage/{value}
``

### Datatypes

#### Image
```csharp
// class
byte[] Data // there goes base64 image data
ImageOptions // image processing options
ImageReturnType ImageReturnType // define how image is being returned from API
```

#### ImageOptions
```csharp
// class
float Contrast
float Saturation
int Brightness
```

#### ImageReturnType
```csharp
// enum
File,
Stream
```

### How does POST body look's like?

```javascript
body: {
  ...
  data: "", // base64 encoded image
  imageOptions: {
    brightness: 50
    saturation: 0.5
    contrast: 2.0
  }
  imageReturnType: 0
}
```
