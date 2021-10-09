$(document).ready(function () {

    /* DataTables start here. */

   const dataTable = $('#deletedBlogsTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Blog/GetAllDeletedBlogs/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#deletedBlogsTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const blogResult = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(blogResult);
                            if (blogResult.Data.ResultStatus === 0) {
                                let categoriesArray = [];
                                $.each(blogResult.Data.Blogs.$values,
                                    function (index, blog) {
                                        const newBlog = getJsonNetObject(blog, blogResult.Data.Blogs.$values);
                                        let newCategory = getJsonNetObject(newBlog.Category, newBlog);
                                        if (newCategory !== null) {
                                            categoriesArray.push(newCategory);
                                        }
                                        if (newCategory === null) {
                                            newCategory = categoriesArray.find((category) => {
                                                return category.$id === newBlog.Category.$ref;
                                            });
                                        }
                                        const newTableRow = dataTable.row.add([
                                            newBlog.Id,
                                            newBlog.Name,
                                            newBlog.Title,
                                            `<img src="/img/${newBlog.Thumbnail}" alt="${newBlog.Title}" class="my-image-table" />`,
                                            `${convertToShortDate(newBlog.Date)}`,
                                            newBlog.ViewCount,
                                            newBlog.CommentCount,
                                            `${newBlog.IsActive ? "Evet" : "Hayır"}`,
                                            `${newBlog.IsDeleted ? "Evet" : "Hayır"}`,
                                            `${convertToShortDate(newBlog.CreatedDate)}`,
                                            newBlog.CreatedByName,
                                            `${convertToShortDate(newBlog.ModifiedDate)}`,
                                            newBlog.ModifiedByName,
                                            `
                                                <button class="btn btn-primary btn-sm btn-undo" data-id="${newBlog.Id}"><span class="fas fa-undo"></span></button>
                                                <button class="btn btn-danger btn-sm btn-delete" data-id="${newBlog.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `${newBlog.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#deletedBlogsTable').fadeIn(1400);
                            } else {
                                toastr.error(`${blogResult.Data.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#deletedBlogsTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
       language: {
           "emptyTable": "Tabloda herhangi bir veri mevcut değil",
           "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
           "infoEmpty": "Kayıt yok",
           "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
           "infoThousands": ".",
           "lengthMenu": "Sayfada _MENU_ kayıt göster",
           "loadingRecords": "Yükleniyor...",
           "processing": "İşleniyor...",
           "search": "Ara:",
           "zeroRecords": "Eşleşen kayıt bulunamadı",
           "paginate": {
               "first": "İlk",
               "last": "Son",
               "next": "Sonraki",
               "previous": "Önceki"
           },
           "aria": {
               "sortAscending": ": artan sütun sıralamasını aktifleştir",
               "sortDescending": ": azalan sütun sıralamasını aktifleştir"
           },
           "select": {
               "rows": {
                   "_": "%d kayıt seçildi",
                   "1": "1 kayıt seçildi"
               },
               "cells": {
                   "1": "1 hücre seçildi",
                   "_": "%d hücre seçildi"
               },
               "columns": {
                   "1": "1 sütun seçildi",
                   "_": "%d sütun seçildi"
               }
           },
           "autoFill": {
               "cancel": "İptal",
               "fillHorizontal": "Hücreleri yatay olarak doldur",
               "fillVertical": "Hücreleri dikey olarak doldur",
               "fill": "Bütün hücreleri <i>%d<\/i> ile doldur"
           },
           "buttons": {
               "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
               "colvis": "Sütun görünürlüğü",
               "colvisRestore": "Görünürlüğü eski haline getir",
               "copySuccess": {
                   "1": "1 satır panoya kopyalandı",
                   "_": "%ds satır panoya kopyalandı"
               },
               "copyTitle": "Panoya kopyala",
               "csv": "CSV",
               "excel": "Excel",
               "pageLength": {
                   "-1": "Bütün satırları göster",
                   "_": "%d satır göster"
               },
               "pdf": "PDF",
               "print": "Yazdır",
               "copy": "Kopyala",
               "copyKeys": "Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın."
           },
           "searchBuilder": {
               "add": "Koşul Ekle",
               "button": {
                   "0": "Arama Oluşturucu",
                   "_": "Arama Oluşturucu (%d)"
               },
               "condition": "Koşul",
               "conditions": {
                   "date": {
                       "after": "Sonra",
                       "before": "Önce",
                       "between": "Arasında",
                       "empty": "Boş",
                       "equals": "Eşittir",
                       "not": "Değildir",
                       "notBetween": "Dışında",
                       "notEmpty": "Dolu"
                   },
                   "number": {
                       "between": "Arasında",
                       "empty": "Boş",
                       "equals": "Eşittir",
                       "gt": "Büyüktür",
                       "gte": "Büyük eşittir",
                       "lt": "Küçüktür",
                       "lte": "Küçük eşittir",
                       "not": "Değildir",
                       "notBetween": "Dışında",
                       "notEmpty": "Dolu"
                   },
                   "string": {
                       "contains": "İçerir",
                       "empty": "Boş",
                       "endsWith": "İle biter",
                       "equals": "Eşittir",
                       "not": "Değildir",
                       "notEmpty": "Dolu",
                       "startsWith": "İle başlar"
                   },
                   "array": {
                       "contains": "İçerir",
                       "empty": "Boş",
                       "equals": "Eşittir",
                       "not": "Değildir",
                       "notEmpty": "Dolu",
                       "without": "Hariç"
                   }
               },
               "data": "Veri",
               "deleteTitle": "Filtreleme kuralını silin",
               "leftTitle": "Kriteri dışarı çıkart",
               "logicAnd": "ve",
               "logicOr": "veya",
               "rightTitle": "Kriteri içeri al",
               "title": {
                   "0": "Arama Oluşturucu",
                   "_": "Arama Oluşturucu (%d)"
               },
               "value": "Değer",
               "clearAll": "Filtreleri Temizle"
           },
           "searchPanes": {
               "clearMessage": "Hepsini Temizle",
               "collapse": {
                   "0": "Arama Bölmesi",
                   "_": "Arama Bölmesi (%d)"
               },
               "count": "{total}",
               "countFiltered": "{shown}\/{total}",
               "emptyPanes": "Arama Bölmesi yok",
               "loadMessage": "Arama Bölmeleri yükleniyor ...",
               "title": "Etkin filtreler - %d"
           },
           "thousands": ".",
           "datetime": {
               "amPm": [
                   "öö",
                   "ös"
               ],
               "hours": "Saat",
               "minutes": "Dakika",
               "next": "Sonraki",
               "previous": "Önceki",
               "seconds": "Saniye",
               "unknown": "Bilinmeyen",
               "weekdays": {
                   "6": "Paz",
                   "5": "Cmt",
                   "4": "Cum",
                   "3": "Per",
                   "2": "Çar",
                   "1": "Sal",
                   "0": "Pzt"
               },
               "months": {
                   "9": "Ekim",
                   "8": "Eylül",
                   "7": "Ağustos",
                   "6": "Temmuz",
                   "5": "Haziran",
                   "4": "Mayıs",
                   "3": "Nisan",
                   "2": "Mart",
                   "11": "Aralık",
                   "10": "Kasım",
                   "1": "Şubat",
                   "0": "Ocak"
               }
           },
           "decimal": ",",
           "editor": {
               "close": "Kapat",
               "create": {
                   "button": "Yeni",
                   "submit": "Kaydet",
                   "title": "Yeni kayıt oluştur"
               },
               "edit": {
                   "button": "Düzenle",
                   "submit": "Güncelle",
                   "title": "Kaydı düzenle"
               },
               "error": {
                   "system": "Bir sistem hatası oluştu (Ayrıntılı bilgi)"
               },
               "multi": {
                   "info": "Seçili kayıtlar bu alanda farklı değerler içeriyor. Seçili kayıtların hepsinde bu alana aynı değeri atamak için buraya tıklayın; aksi halde her kayıt bu alanda kendi değerini koruyacak.",
                   "noMulti": "Bu alan bir grup olarak değil ancak tekil olarak düzenlenebilir.",
                   "restore": "Değişiklikleri geri al",
                   "title": "Çoklu değer"
               },
               "remove": {
                   "button": "Sil",
                   "confirm": {
                       "_": "%d adet kaydı silmek istediğinize emin misiniz?",
                       "1": "Bu kaydı silmek istediğinizden emin misiniz?"
                   },
                   "submit": "Sil",
                   "title": "Kayıtları sil"
               }
           }
       }
    });

    /* DataTables end here */

    /* Ajax POST / HardDeleting a Blog starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            const blogTitle = tableRow.find('td:eq(2)').text();
            Swal.fire({
                title: 'Kalıcı olarak silmek istediğinize emin misiniz?',
                text: `${blogTitle} başlıklı makale kalıcı olarak silinicektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, kalıcı olarak silmek istiyorum.',
                cancelButtonText: 'Hayır, istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { blogId: id },
                        url: '/Admin/Blog/HardDelete/',
                        success: function (data) {
                            const blogResult = jQuery.parseJSON(data);
                            if (blogResult.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    `${blogResult.Message}`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${blogResult.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!");
                        }
                    });
                }
            });
        });

    /* Ajax POST / HardDeleting a Blog ends here */

    /* Ajax POST / UndoDeleting a Blog starts from here */

    $(document).on('click',
        '.btn-undo',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            let blogTitle = tableRow.find('td:eq(2)').text();
            Swal.fire({
                title: 'Arşivden geri getirmek istediğinize emin misiniz?',
                text: `${blogTitle} başlıklı makale arşivden geri getirilecektir!`,
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, arşivden geri getirmek istiyorum.',
                cancelButtonText: 'Hayır, istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { blogId: id },
                        url: '/Admin/Blog/UndoDelete/',
                        success: function (data) {
                            const blogUndoDeleteResult = jQuery.parseJSON(data);
                            console.log(blogUndoDeleteResult);
                            if (blogUndoDeleteResult.ResultStatus === 0) {
                                Swal.fire(
                                    'Arşivden Geri Getirildi!',
                                    `${blogUndoDeleteResult.Message}`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${blogUndoDeleteResult.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!");
                        }
                    });
                }
            });
        });
/* Ajax POST / UndoDeleting a Blog ends here */

});