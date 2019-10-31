using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExifLib
{
    public class ImageList
    {
        private readonly string _path;
        private readonly string[] _fileTypes;
        public readonly List<ImageWithExif> list;

        /// <summary>
        /// Distinct focal lengths cast to a string
        /// </summary>
        public IOrderedEnumerable<string> FocalLengths => FocalLengthsDouble.Select(d => d.ToString("0")).OrderBy(a => 1);

        /// <summary>
        /// Distinct Focal Lengths as a double
        /// </summary>
        private IOrderedEnumerable<double> FocalLengthsDouble => list.GroupBy(l => l.FocalLengthDouble)
                                  .Select(g => g.Key).OrderBy(g => g);

        /// <summary>
        /// Distinct focal length counts
        /// </summary>
        public IEnumerable<int> FocalLengthCounts => list.GroupBy(l => l.FocalLengthDouble)
                                  .Select(g => g.Select(l => l.FocalLengthDouble).Count());

        /// <summary>
        /// Distinct Shutter Speeds cast to string
        /// </summary>
        public IOrderedEnumerable<string> ShutterSpeeds => ShutterSpeedsDouble.Select(d => d).OrderBy(a => 1);

        private IOrderedEnumerable<string> ShutterSpeedsDouble => list.GroupBy(l => l.ShutterSpeed)
            .Select(g => g.Key).OrderBy(g => g);

        /// <summary>
        /// Distinct Shutter Speed Counts
        /// </summary>
        public IEnumerable<int> ShutterSpeedCounts => list.GroupBy(l => l.ShutterSpeedDouble)
                                  .Select(g => g.Select(l => l.ShutterSpeedDouble).Count());


        public IOrderedEnumerable<string> Apertures => list.GroupBy(l => l.Aperture)
            .Select(g => g.Key).OrderBy(g => g);

        public IEnumerable<int> Aperturecounts => list.GroupBy(l => l.Aperture)
                                  .Select(g => g.Select(l => l.Aperture).Count());

        public ImageList(string path)
        {
            _path = path;
            list = new List<ImageWithExif>();
            _fileTypes = new string[] { "*.jpg", "*.cr2" };
        }

        public async Task Load()
        {
            list.Clear();
            await Task.Run(() =>
            {

                foreach (var file in MyDirectory.GetFiles(_path, _fileTypes, SearchOption.AllDirectories))
                {
                    try
                    {
                        list.Add(new ImageWithExif(file));
                    }
                    catch
                    {

                    }
                }
            });
        }
    }
}
