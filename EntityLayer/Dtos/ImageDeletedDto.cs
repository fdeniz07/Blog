using CoreLayer.Entities.Abstract;

namespace EntityLayer.Dtos
{
    public class ImageDeletedDto : IDto
    {
        public string FullName { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}
