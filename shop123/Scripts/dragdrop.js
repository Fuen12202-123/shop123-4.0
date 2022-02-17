const dropArea = document.querySelector(".drag-area"),
    dragText = dropArea.querySelector("header"),
    button = dropArea.querySelector("button"),
    input = dropArea.querySelector("input");
const btn = document.querySelector(".button")

let file;

button.onclick = () => {
    input.click();
    return false;
}

btn.onclick = () => {
    input.click();
    return false;
}

input.addEventListener("change", function () {
    file = this.files[0];
    showFile();
    dropArea.classList.add("active");
})



dropArea.addEventListener("dragover", (event) => {   // dragover:拖曳進dropArea觸發
    event.preventDefault();
    //console.log("File is over DragArea");
    dropArea.classList.add("active");        // 觸發時，新增class名為active
    dragText.textContent = "放下即可";
});

dropArea.addEventListener("dragleave", () => {   // dragleave: 拖曳離開dropArea觸發
    //console.log("File is outside from DragArea");
    dropArea.classList.remove("active");      // 觸發時，移除class名為active
    dragText.textContent = "拖曳或點擊";
});

dropArea.addEventListener("drop", (event) => {  // drop: 拖曳放置時觸發
    event.preventDefault();
    //console.log("File is dropped on DropArea");
    file = event.dataTransfer.files[0];    // dataTransfer.file: 保存托放操作的資料  拖動中表示文件列表，若操作中不包含文件，則列表為空
    showFile();
});

function showFile() {
    let fileType = file.type;              // 上傳檔案的格式
    //console.log(fileType);
    let validExtensions = ["image/jpeg", "image/jpg", "image/png"];  // 限制格式
    if (validExtensions.includes(fileType)) {    // include 會判斷傳入的檔案格式是否與上面陣列有相同，給予true或false
        //console.log("This is an Image file");
        let fileReader = new FileReader();
        fileReader.onload = () => {
            let fileURL = fileReader.result;
            //console.log(fileURL);
            let imgTag = `<img src="${fileURL}" alt="">`;
            dropArea.innerHTML = imgTag;
        }
        fileReader.readAsDataURL(file);
    } else {
        alert("This is not an Image file!!!");
        dropArea.classList.remove("active");
        dragText.textContent = "Drag & Drop to Upload File";
    }
}