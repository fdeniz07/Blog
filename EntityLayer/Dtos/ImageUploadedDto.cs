using CoreLayer.Entities.Abstract;

namespace EntityLayer.Dtos
{
    public class ImageUploadedDto : IDto
    {
        public string FullName { get; set; }
        public string OldName { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string FolderName { get; set; }
        public long Size { get; set; }
    }
}
