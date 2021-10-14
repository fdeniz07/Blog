using EntityLayer.Dtos;

namespace BlogWeb.Models
{
    public class CommentAddAjaxViewModel
    {
        public CommentAddDto CommentAddDto { get; set; }

        public string CommentAddPartial { get; set; }

        public CommentDto CommentDto { get; set; }
    }
}
