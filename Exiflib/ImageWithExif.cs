﻿
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExifLib
{
    public class ImageWithExif
    {
        public string FileName { get; set; }

        public String Aperture { get; set; }

        public string ShutterSpeed { get; set; }

        public double ShutterSpeedDouble { get; set; }

        public string FocalLength { get; set; }

        public double FocalLengthDouble { get; set; }

        public string Iso { get; set; }
  
        public string ExposureProgram { get; set; }

        public string MeteringMode { get; set; }

        private string _lensModel;

        private string _lensMake;

        public String Lens { get { return _lensMake + " " + _lensModel; } }

        public string CameraMake { get; set; }

        public string CameraModel { get; set; }

        public string MakerNote { get; set; }

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

        private void getTags(IEnumerable<MetadataExtractor.Directory> directories)
        {
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

            if (subIfdDirectory != null)
            {
                Aperture = subIfdDirectory.GetDescription(ExifDirectoryBase.TagAperture);
                ShutterSpeed = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureTime);
                ShutterSpeedDouble = subIfdDirectory.GetDouble(ExifDirectoryBase.TagExposureTime);
                FocalLength = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFocalLength);
                FocalLengthDouble = subIfdDirectory.GetDouble(ExifDirectoryBase.TagFocalLength);
                Iso = subIfdDirectory.GetDescription(ExifDirectoryBase.TagIsoEquivalent);
                _lensMake = subIfdDirectory.GetDescription(ExifDirectoryBase.TagLensMake);
                _lensModel = subIfdDirectory.GetDescription(ExifDirectoryBase.TagLensModel);
                ExposureProgram = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureProgram);
                MeteringMode = subIfdDirectory.GetDescription(ExifDirectoryBase.TagMeteringMode);
                DateTaken = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTime);
                MakerNote = subIfdDirectory.GetDescription(ExifDirectoryBase.TagMakernote);
            }

            // todo search for directories
            var directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();

            if (directory != null)
            {
                CameraMake = directory.GetDescription(ExifDirectoryBase.TagMake);
                CameraModel = directory.GetDescription(ExifDirectoryBase.TagModel);
            }
        }
    }
}
