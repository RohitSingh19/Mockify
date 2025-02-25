using Mockify.API.Helper;
using System.ComponentModel;

namespace Mockify.API.Models
{
    public class FileSystem
    {

        [Description("Random file Name")]
        public string FileName { get; set; }

        [Description("Random directory path")]
        public string DirectoryPath { get; set; }
        
        [Description("Random directory path")]
        public string FilePath { get; set; }

        [Description("Random directory type")]
        public string FileType {  get; set; }
        
        [Description("Random file extention")]
        public string FileExtention { get; set; }
    }
}
