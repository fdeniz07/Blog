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
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /////////////////////// GetAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentDto>> GetAsync(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId);
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
            var result = await _unitOfWork.Comments.AnyAsync(c => c.Id == commentId);
            if (result)
            {
                var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId);
                var commentUpdateDto = _mapper.Map<CommentUpdateDto>(comment);
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
            var comments = await _unitOfWork.Comments.GetAllAsync();
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
            var comments = await _unitOfWork.Comments.GetAllAsync(c => c.IsDeleted);
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
            var comments = await _unitOfWork.Comments.GetAllAsync(c => !c.IsDeleted);
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
            var comments = await _unitOfWork.Comments.GetAllAsync(c => !c.IsDeleted && c.IsActive);
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


        /////////////////////// AddAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto)
        {
            var comment = _mapper.Map<Comment>(commentAddDto);
            var addedComment = await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
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
            var oldComment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentUpdateDto.Id);
            var comment = _mapper.Map<CommentUpdateDto, Comment>(commentUpdateDto, oldComment);
            comment.ModifiedByName = modifiedByName;
            var updatedComment = await _unitOfWork.Comments.UpdateAsync(comment);
            await _unitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.Update(comment.CreatedByName),
                new CommentDto
                {
                    Comment = updatedComment,
                });
        }


        /////////////////////// DeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CommentDto>> DeleteAsync(int commentId, string modifiedByName)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId);
            if (comment != null)
            {
                comment.IsDeleted = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await _unitOfWork.Comments.UpdateAsync(comment);
                await _unitOfWork.SaveAsync();
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


        /////////////////////// HardDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> HardDeleteAsync(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId);
            if (comment != null)
            {
                await _unitOfWork.Comments.DeleteAsync(comment);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Comment.HardDelete(comment.CreatedByName));
            }

            return new Result(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false));
        }


        /////////////////////// CountAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync(); // tüm degerleri getir
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
                await _unitOfWork.Comments.CountAsync(c => !c.IsDeleted); // Silinmemis degerleri getir
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

