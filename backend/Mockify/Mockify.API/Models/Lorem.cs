namespace Mockify.API.Models
{
    public class Lorem
    {
        public string Word { get; set; }
        public string [] Words { get; set; }
        public string Letter { get; set; }
        public string Sentence { get; set; }
        public string Text { get; set; }
        public string Slug { get; set; }

        public string GetEndPoint()
        {
            return "getLoremMock";
        }
    }
}
