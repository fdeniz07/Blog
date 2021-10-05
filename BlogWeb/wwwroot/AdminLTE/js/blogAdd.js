$(document).ready(function() {

    //Trumbowyg

    $('#text-editor').trumbowyg({
        btns: [
            ['viewHTML'],
            ['undo', 'redo'], // Only supported in Blink browsers
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['insertImage'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen'],
            ['foreColor', 'backColor'],
            ['emoji'],
            ['fontfamily'],
            ['fontsize'],
            ['indent'],
            ['outdent'],
            ['historyUndo', 'historyRedo'],
            ['lineheight'],
            ['specialChars']
        ],
        lang: "tr",
        plugins: {
            //colors: {
            //    foreColorList: [
            //        'ff0000', '00ff00', '0000ff', '77D970', 'FF0075', '1C0C5B', 'F0A500', '3F0713', '1DB9C3', '14279B', '150050', '152D35', 'FF0000', '3DB2FF', 'F6D167', '5C3D2E', '000000', 'FFD523', '0A1D37', 'FC5404', 'F21170', 'A799B7'
            //    ],
            //    backColorList: [
            //        '000', '333', '555'
            //    ],
            //    displayAsList: false
            //}
            fontsize: {
                sizeList: [
                    '12px',
                    '14px',
                    '16px'
                ]
            }
        }
    });

    //Trumbowyg

    //Select2

    $('#categoryList').select2({
        theme: 'bootstrap4',
        placeholder: "Lütfen bir kategori seçiniz...",
        allowClear: true
    });

    //Select2
})