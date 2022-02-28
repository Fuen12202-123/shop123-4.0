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
                        $('.testzone').append('<input style="display:none" class="uploadImag" type="text" name="spuImg' + y + '"' + 'value="' + img + '"/>');
                    }
                }
                else {
                    alert('請至少上傳一張圖片');

                }
            });
    }
}
function selectB(item) {
    var index = item.options[item.selectedIndex].value;
    /* alert(index);*/


    if (index == 1) {
        $('#testC').empty();
        $('#testC').append(`
                        <option name="catalogBId" value="1">長袖</option>
                        <option name="catalogBId" value="2">短袖</option>
                        <option name="catalogBId" value="3">背心/小可愛</option>
                        <option name="catalogBId" value="4">襯衫</option>
                        <option name="catalogBId" value="5">洋裝</option>
                        <option name="catalogBId" value="6">外套</option>
                        <option name="catalogBId" value="7">長褲</option>
                        <option name="catalogBId" value="8">短褲</option>
                        <option name="catalogBId" value="9">裙子</option>
                        <option name="catalogBId" value="10">內褲</option>
                        <option name="catalogBId" value="34">鞋子</option>

                `)
    }
    if (index == 2) {
        $('#testC').empty();
        $('#testC').append(`
                        <option name="catalogBId" value="11">長袖</option>
                        <option name="catalogBId" value="12">短袖</option>
                        <option name="catalogBId" value="13">背心</option>
                        <option name="catalogBId" value="14">襯衫</option>
                        <option name="catalogBId" value="15">外套</option>
                        <option name="catalogBId" value="16">長褲</option>
                        <option name="catalogBId" value="17">短褲</option>
                        <option name="catalogBId" value="18">內褲</option>

                `)
    }
    if (index == 3) {
        $('#testC').empty();
        $('#testC').append(`
                        <option name="catalogBId" value="19">長袖</option>
                        <option name="catalogBId" value="20">短袖</option>
                        <option name="catalogBId" value="21">背心</option>
                        <option name="catalogBId" value="22">襯衫</option>
                        <option name="catalogBId" value="23">外套</option>
                        <option name="catalogBId" value="24">長褲</option>
                        <option name="catalogBId" value="25">短褲</option>
                        <option name="catalogBId" value="26">內褲</option>

                `)
    }
    if (index == 4) {
        $('#testC').empty();
        $('#testC').append(`
                         <option name="catalogBId" value="27">長袖</option>
                        <option name="catalogBId" value="28">短袖</option>
                        <option name="catalogBId" value="29">外套</option>
                        <option name="catalogBId" value="30">長褲</option>
                        <option name="catalogBId" value="31">短褲</option>

                `)
    }
    if (index == 5) {
        $('#testC').empty();
        $('#testC').append(`
                       <option name="catalogBId" value="35">其他</option>
                        <option name="catalogBId" value="36">其他</option>
                        <option name="catalogBId" value="37">其他</option>
                        <option name="catalogBId" value="38">其他</option>
                        <option name="catalogBId" value="39">其他</option>

                `)
    }
    /* alert(index);*/
    /* alert(item.options[item.selectedIndex].value);*/
}