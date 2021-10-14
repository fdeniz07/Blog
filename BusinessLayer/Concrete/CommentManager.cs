using System;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using CoreLayer.Utilities.Results.Concrete;
using DataAccessLayer.Abstract.UnitOfWorks;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Utilities;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BusinessLayer.Concrete
{
    public class CommentManager :ManagerBase, ICommentService
    {


        public CommentManager(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork, mapper)
        {
        }


        /////////////////////// GetAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentDto>> GetAsync(int commentId)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId);
            if (comment != null)
            {
                return new DataResult<CommentDto>(ResultStatus.Success, new CommentDto
                {
                    Comment = comment,
                });
            }

            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false),
                new CommentDto
                {
                    Comment = null,
                });
        }


        /////////////////////// GetCommentUpdateDtoAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentUpdateDto>> GetCommentUpdateDtoAsync(int commentId)
        {
            var result = await UnitOfWork.Comments.AnyAsync(c => c.Id == commentId);
            if (result)
            {
                var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId);
                var commentUpdateDto = Mapper.Map<CommentUpdateDto>(comment);
                return new DataResult<CommentUpdateDto>(ResultStatus.Success, commentUpdateDto);
            }
            else
            {
                return new DataResult<CommentUpdateDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false),
                    null);
            }
        }


        /////////////////////// GetAllAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentListDto>> GetAllAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(null,c=>c.Blog);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }

            return new DataResult<CommentListDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: true),
                new CommentListDto
                {
                    Comments = null,
                });
        }


        /////////////////////// GetAllByDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentListDto>> GetAllByDeletedAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c => c.IsDeleted, c => c.Blog);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }

            return new DataResult<CommentListDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: true),
                new CommentListDto
                {
                    Comments = null,
                });

        }
        /////////////////////// GetAllByNonDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentListDto>> GetAllByNonDeletedAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c => !c.IsDeleted, c => c.Blog);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }

            return new DataResult<CommentListDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: true),
                new CommentListDto
                {
                    Comments = null,
                });
        }


        /////////////////////// GetAllByNonDeletedAndActiveAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Blog);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }

            return new DataResult<CommentListDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: true),
                new CommentListDto
                {
                    Comments = null,
                });
        }


        /////////////////////// ApproveAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentDto>> ApproveAsync(int commentId, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId, c => c.Blog);
            if (comment!=null)
            {
                comment.IsActive = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate=DateTime.Now;
                var updatedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.Approve(commentId), new CommentDto
                {
                    Comment = updatedComment
                });
            }
            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(false), null);
        }

        
        /////////////////////// AddAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto)
        {
            var blog = await UnitOfWork.Blogs.GetAsync(b => b.Id == commentAddDto.BlogId);
            if (blog==null)
            {
                return new DataResult<CommentDto>(ResultStatus.Error, Messages.Blog.NotFound(isPlural: false), null);
            }
            var comment = Mapper.Map<Comment>(commentAddDto);
            var addedComment = await UnitOfWork.Comments.AddAsync(comment);
            blog.CommentCount = await UnitOfWork.Comments.CountAsync(c => c.BlogId == blog.Id && !c.IsDeleted); //ilgili makaleye ait, silinmemis yorum sayisini getir
            await UnitOfWork.Blogs.UpdateAsync(blog);
            await UnitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Success,
                Messages.Comment.Add(commentAddDto.CreatedByName), new CommentDto
                {
                    Comment = addedComment,
                });
        }


        /////////////////////// UpdateAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentDto>> UpdateAsync(CommentUpdateDto commentUpdateDto,
            string modifiedByName)
        {
            var oldComment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentUpdateDto.Id);
            var comment = Mapper.Map<CommentUpdateDto, Comment>(commentUpdateDto, oldComment);
            comment.ModifiedByName = modifiedByName;
            var updatedComment = await UnitOfWork.Comments.UpdateAsync(comment);
            updatedComment.Blog = await UnitOfWork.Blogs.GetAsync(b => b.Id == updatedComment.BlogId);
            await UnitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.Update(comment.CreatedByName),
                new CommentDto
                {
                    Comment = updatedComment,
                });
        }


        /////////////////////// DeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentDto>> DeleteAsync(int commentId, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId, c=>c.Blog);
            if (comment != null)
            {
                var blog = comment.Blog;
                comment.IsDeleted = true;
                comment.IsActive = false;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                blog.CommentCount = await UnitOfWork.Comments.CountAsync(c => c.BlogId == blog.Id && !c.IsDeleted);
                await UnitOfWork.Blogs.UpdateAsync(blog);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Success,
                    Messages.Comment.Delete(deletedComment.CreatedByName), new CommentDto
                    {
                        Comment = deletedComment,
                    });
            }

            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false),
                new CommentDto
                {
                    Comment = null,
                });
        }


        /////////////////////// UndoDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\ Silinmisleri Geri Getir

        public async Task<IDataResult<CommentDto>> UndoDeleteAsync(int commentId, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId,c=>c.Blog);
            if (comment != null)
            {
                var blog = comment.Blog;
                comment.IsDeleted = false;
                comment.IsActive = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                blog.CommentCount = await UnitOfWork.Comments.CountAsync(c => c.BlogId == blog.Id && !c.IsDeleted);
                await UnitOfWork.Blogs.UpdateAsync(blog);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Success,
                    Messages.Comment.UndoDelete(deletedComment.CreatedByName), new CommentDto
                    {
                        Comment = deletedComment,
                    });
            }

            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false),
                new CommentDto
                {
                    Comment = null,
                });
        }


        /////////////////////// HardDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> HardDeleteAsync(int commentId)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId, c=>c.Blog);
            if (comment != null)
            {
                var blog = comment.Blog;
                await UnitOfWork.Comments.DeleteAsync(comment);
                blog.CommentCount = await UnitOfWork.Comments.CountAsync(c => c.BlogId==blog.Id && !c.IsDeleted);
                await UnitOfWork.Blogs.UpdateAsync(blog);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Comment.HardDelete(comment.CreatedByName));
            }

            return new Result(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false));
        }


        /////////////////////// CountAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentsCount = await UnitOfWork.Comments.CountAsync(); // tüm degerleri getir
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }


        /////////////////////// CountByNonDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var commentsCount =
                await UnitOfWork.Comments.CountAsync(c => !c.IsDeleted); // Silinmemis degerleri getir
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }
    }
}

