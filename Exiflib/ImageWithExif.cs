using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExifLib
{
    public class ImageWithExif
    {
        public string FileName { get; set; }

        public String Aperture { get; set; }

        public string ShutterSpeed { get; set; }

        public string FocalLength { get; set; }

        public string Iso { get; set; }
  
        public string ExposureProgram { get; set; }

        public string MeteringMode { get; set; }

        private string _lensModel;

        private string _lensMake;

        public String Lens { get { return _lensMake + " " + _lensModel; } }

        public string CameraMake { get; set; }

        public string CameraModel { get; set; }

        public string DateTaken
        {
            get; set;
        }
       

        public ImageWithExif(String FileName)
        {
            this.FileName = FileName;

            var directories = ImageMetadataReader.ReadMetadata(this.FileName);

            getTags(directories);
        }

        private void getTags(IEnumerable<Directory> directories)
        {
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

            if (subIfdDirectory != null)
            {
                Aperture = subIfdDirectory.GetDescription(ExifDirectoryBase.TagAperture);
                ShutterSpeed = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureTime);
                FocalLength = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFocalLength);
                Iso = subIfdDirectory.GetDescription(ExifDirectoryBase.TagIsoEquivalent);
                CameraMake = subIfdDirectory.GetDescription(ExifDirectoryBase.TagMake);
                _lensMake = subIfdDirectory.GetDescription(ExifDirectoryBase.TagLensMake);
                _lensModel = subIfdDirectory.GetDescription(ExifDirectoryBase.TagLensModel);
                ExposureProgram = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureProgram);
                MeteringMode = subIfdDirectory.GetDescription(ExifDirectoryBase.TagMeteringMode);
                DateTaken = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTimeDigitized);

            }

        }
    }
}
