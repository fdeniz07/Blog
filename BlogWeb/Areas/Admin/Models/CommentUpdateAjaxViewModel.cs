using EntityLayer.Dtos;

namespace BlogWeb.Areas.Admin.Models
{
    public class CommentUpdateAjaxViewModel
    {
        public CommentUpdateDto CommentUpdateDto { get; set; }

        public string CommentUpdatePartial { get; set; }

        public CommentDto CommentDto { get; set; }
    }
}
