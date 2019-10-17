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

        private double _aperture;

        public String Aperture
        {
            get { return "F/" + _aperture.ToString(); }

        }
        private double _shutterSpeed;

        public string ShutterSpeed
        {
            get
            {
                var sb = new StringBuilder();
                if (_shutterSpeed < 1)
                {
                    sb.Append("1/");
                    sb.Append(1 / _shutterSpeed).ToString();
                    sb.Append("s");
                    return sb.ToString();
                }
                else
                {
                    return _shutterSpeed.ToString() + "s";
                }

                
            }
        }
        private double _focalLength;

        public double FocalLength
        {
            get { return _focalLength; }
            set { _focalLength = value; }
        }
        private ushort _iso;

        public ushort Iso
        {
            get { return _iso; }
            set { _iso = value; }
        }
        private ushort _ExposureProgram;

        /// <summary>
        /// Returns the exposure program as a string. The exif info is actually a short which is converted. found this info here:http://www.media.mit.edu/pia/Research/deepview/exif.html
        /// </summary>
        public string ExposureProgram
        {
            get
            {
                switch (_ExposureProgram)
                {
                    case 1:
                        return "Manual";
                    case 2:
                        return "Program";
                    case 3:
                        return "Aperture Priority";
                    case 4:
                        return "Shutter Priority";
                    case 5:
                        return "Program Creative";
                    default:
                        return "Other";
                }
            }

        }

        private ushort _meteringMode;


        /// <summary>
        /// Returns the exposure program as a string. The exif info is actually a short which is converted. found this info here:http://www.media.mit.edu/pia/Research/deepview/exif.html
        /// </summary>
        public string MeteringMode
        {
            get
            {
                switch (_meteringMode)
                {
                    case 1:
                        return "Evaluative";
                    case 2:
                        return "Center-Weighted Average";
                    case 3:
                        return "Spot";
                    case 4:
                        return "Multi-Spot";
                    case 5:
                        return "Multi-Segment";
                    default:
                        return "Other";
                }
            }

        }

        private string _lensModel;

        private string _lensMake;

        public String Lens { get { return _lensMake + " " + _lensModel; } }
        private string _cameraMake;

        public string CameraMake
        {
            get { return _cameraMake; }
            set { _cameraMake = value; }
        }
        private string _cameraModel;

        public string CameraModel
        {
            get { return _cameraModel; }
            set { _cameraModel = value; }
        }

        private DateTime _DateTaken;

        public DateTime DateTaken
        {
            get { return _DateTaken; }
            set { _DateTaken = value; }
        }
       

        public ImageWithExif(String FileName)
        {
            this.FileName = FileName;

            IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(this.FileName);

            //using (var reader = new ExifReader(FileName))
            //{
            //    setProperties(reader);
            //}
            setProperties2(directories);
        }

        private void setProperties2(IEnumerable<Directory> directories)
        {
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

            _aperture = (double) subIfdDirectory?.GetDouble(ExifDirectoryBase.TagAperture);
        }

        private void setProperties(ExifReader reader)
        {
            //try
            //{
                var goodTag = reader.GetTagValue(ExifTags.FNumber, out _aperture);

                goodTag = reader.GetTagValue(ExifTags.ExposureTime, out _shutterSpeed);

                goodTag = reader.GetTagValue(ExifTags.FocalLength, out _focalLength);
                goodTag = reader.GetTagValue(ExifTags.ISOSpeedRatings, out _iso);

                goodTag = reader.GetTagValue(ExifTags.ExposureProgram, out _ExposureProgram);
                goodTag = reader.GetTagValue(ExifTags.Make, out _cameraMake);
                goodTag = reader.GetTagValue(ExifTags.Model, out _cameraModel);
                goodTag = reader.GetTagValue(ExifTags.DateTime, out _DateTaken);
                goodTag = reader.GetTagValue(ExifTags.LensModel, out _lensModel);
                goodTag = reader.GetTagValue(ExifTags.LensMake, out _lensMake);
                goodTag = reader.GetTagValue(ExifTags.MeteringMode, out _meteringMode);
            //}
            //catch (Exception ex)
            //{
            //    //do something useful with the message
            //    throw;
            //}
        }
    }
}
