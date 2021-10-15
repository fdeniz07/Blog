$(document).ready(function () {
    $(function () {
        $(document).on('click', '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-comment-add');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    const commentAddAjaxModel = jQuery.parseJSON(data);
                    console.log(commentAddAjaxModel);
                    const newFormBody = $('.form-card', commentAddAjaxModel.CommentAddPartial);
                    const cardBody = $('.form-card');
                    cardBody.replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid)
                    {
                        const newSingleComment = `
                            <div class="media">
                               <img class="d-flex mr-3 rounded-circle" src="https://randomuser.me/api/portraits/men/34.jpg" alt="" />
                                     <div class="media-body">
                                        <h5 class="mt-0">
                                              <i class="far fa-user"></i>${commentAddAjaxModel.CommentDto.Comment.CreatedByName}
                                         </h5>
                                        /*<i class="far fa-clock"></i> ${commentAddAjaxModel.CommentDto.Comment.CreatedByName.CreatedDate}*/
                                        <br />
                                        <br />
                                        ${commentAddAjaxModel.CommentDto.Comment.Content}
                                    </div>
                             </div>
                            `;
                        const newSingleCommentObject = $(newSingleComment);
                        newSingleCommentObject.hide();
                        $('#comments').append(newSingleCommentObject);
                        newSingleCommentObject.fadeIn(3000);
                        toastr.success(
                            `Sayın ${commentAddAjaxModel.CommentDto.Comment.CreatedByName} yorumunuz başarıyla eklenmiştir. Bir örneği sizler için karşınıza gelecek. Fakat; yorumunuz onaylandıktan sonra görünür olacaktır.`);
                        $('#btnSave').prop('disabled', true);
                        setTimeout(function () {
                            $('#btnSave').prop('disabled', false);
                        }, 15000);
                    }
                    else
                    {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function ()
                            {
                                let text = $(this).text();
                                summaryText += `*${text}\n`;
                            });
                        toastr.warning(summaryText);
                    }
                }).fail(function (error)
                    {
                        console.error(error);
                    });
            });
    });
});