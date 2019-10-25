using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExifLib
{
    public class ImageList
    {
        private readonly string _path;
        private string[] _fileTypes;
        public List<ImageWithExif> list;

        public IOrderedEnumerable<string> FocalLengths => FocalLengthsDouble.Select(d => d.ToString("0")).OrderBy(a => 1);

        public IOrderedEnumerable<double> FocalLengthsDouble => list.GroupBy(l => l.FocalLengthDouble)
                                  .Select(g => g.Key).OrderBy(g => g);

        public IEnumerable<int> FocalLengthCounts => list.GroupBy(l => l.FocalLengthDouble)
                                  .Select(g => g.Select(l => l.FocalLengthDouble).Count());


        public ImageList(string path)
        {
            _path = path;
            list = new List<ImageWithExif>();
            _fileTypes = new string[] { "*.jpg", "*.cr2" };
        }

        public async Task Load()

        {
            await Task.Run(() =>
            {
                foreach (var type in _fileTypes)
                {
                    foreach (var file in Directory.EnumerateFiles(_path, type, SearchOption.AllDirectories))
                    {
                        try
                        {
                            list.Add(new ImageWithExif(file));
                        }
                        catch
                        {

                        }
                    }
                }
            });
        }
    }
}
