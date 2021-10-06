using System;
using System.Collections.Generic;
using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class Blog : EntityBase, IEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int ViewCount { get; set; } = 0; //Baslangic degeri
        public int CommentCount { get; set; } = 0; //Baslangic degeri
        public string SeoAuthor { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTags { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; } //Bir makale birden fazla yoruma sahip olabilir
    }
}
