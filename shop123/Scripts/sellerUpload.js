var count = 0;
Dropzone.options.dropzoneform = {
    addRemoveLinks: true,
    autoProcessQueue: false,
    maxFiles: 5,
    maxFilesize: 5,//MB
    dictDefaultMessage: '請點擊或將圖片拖曳至此區塊',
    init: function () {
        var zone = this;
        this.on('maxfilesexceeded', function (file) {
            this.removeFile(file);

        });
        this.on('removedfile', function () {
            count--;

        })
        this.on('addedfile', function () {
            count++;
        }),
            $('#save').on('click', function () {
                if (count != 0) {
                    let y = 0;
                    zone.processQueue();
                    for (let i = 0; i < count; i++) {
                        y++;
                        var img = zone.files[i].name;
                        $('.testzone').append('<input class="uploadImag" type="text" name="spuImg' + y + '"' + 'value="' + img + '"/>');
                    }
                }
                else {
                    alert('請至少上傳一張圖片');

                }
            });
    }
}
