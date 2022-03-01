const DdropArea1 = document.querySelector(".Ddrag-area"),
dragbtn = DdropArea1.querySelector(".dragbtn"),
button = DdropArea1.querySelector("button"),
Dinput = DdropArea1.querySelector("input");

let file;



button.onclick = () => {
    Dinput.click();
    return false;
}

Dinput.addEventListener("change", function () {
    file = this.files[0];
    showFile(this);
    DdropArea1.classList.add("active");
})



DdropArea1.addEventListener("dragover", (event) => {   // dragover:拖曳進dropArea觸發
    event.preventDefault();
    //console.log("File is over DragArea");
    DdropArea1.classList.add("active");        // 觸發時，新增class名為active
    
});

DdropArea1.addEventListener("dragleave", () => {   // dragleave: 拖曳離開dropArea觸發
    //console.log("File is outside from DragArea");
    DdropArea1.classList.remove("active");      // 觸發時，移除class名為active
    
});

DdropArea1.addEventListener("drop", (event) => {  // drop: 拖曳放置時觸發
    event.preventDefault();
    //console.log("File is dropped on DropArea");
    file = event.dataTransfer.files[0];    // dataTransfer.file: 保存托放操作的資料  拖動中表示文件列表，若操作中不包含文件，則列表為空
    showFile();
});

function showFile(input) {
    //let fileType = file.type;              // 上傳檔案的格式
    ///*console.log(fileType);*/
    //let validExtensions = ["image/jpeg", "image/jpg", "image/png"];  // 限制格式
    //if (validExtensions.includes(fileType)) {    // include 會判斷傳入的檔案格式是否與上面陣列有相同，給予true或false
    //    /*console.log("This is an Image file");*/
        let fileReader = new FileReader();
        fileReader.onload = () => {
            let fileURL = fileReader.result;
            /*console.log(fileURL);*/
            let imgTag = `<img src="${fileURL}" alt="">`;
            DdropArea1.innerHTML = imgTag;
        }
    fileReader.readAsDataURL(input.file);
    //} else {
    //    alert("This is not an Image file!!!");
    //    DdropArea1.classList.remove("active");
        
    //}
}