const eye = document.querySelector("#eye");
const input = document.querySelector(".eyepassword");

var flag = 0;
eye.onclick = function () {
    if (flag == 0) {
        input.type = 'text';
        eye.src = "../Images/openeye.png";
        flag = 1;
    } else {
        input.type = 'password';
        eye.src = "../Images/closeeye.png";
        flag = 0;
    }
}