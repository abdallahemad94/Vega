using System.IO;
using System.Linq;

namespace Vega.Models
{
    public class PhotosSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupportedFileType(string fileName)
        {
            return AcceptedFileTypes.Contains(Path.GetExtension(fileName).ToLower());
        }
    }
}