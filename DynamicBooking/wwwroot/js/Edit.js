
$(document).ready(function () {
    const deleteFileButtons = document.getElementsByClassName('delete-file-button');
    const deleteEventDateButtons = document.getElementsByClassName('delete-eventdate-button');

    for (var i = 0; i < deleteFileButtons.length; i++) {
        const deleteFileButton = deleteFileButtons[i];

        const index = deleteFileButton.getAttribute('data-index');
        const fileId = deleteFileButton.getAttribute('data-fileId');
        deleteFileButton.onclick = () => deleteFileClick(index, fileId);
    }

    for (var i = 0; i < deleteEventDateButtons.length; i++) {
        const deleteEventDateButton = deleteEventDateButtons[i];

        const index = deleteEventDateButton.getAttribute('data-index');
        const editEventId = deleteEventDateButton.getAttribute('data-editEventId');
        deleteEventDateButton.onlick() = () => deleteEventDateClick(index, editEventId);
    }
})  

function deleteEventDateClick(index, editEventId) {
    $.ajax({
        ulr: '',
        method: 'post',
        dataType: 'json',
        data: { index: index, editEventId: editEventId },
        success: () => removeEventDate(index),
    });
}

function removeEventDate(index) {
    const uploadedEventDate = document.getElementById(`${index}-date-element`);

    uploadedEventDate.remove();
}

function deleteFileClick(index, fileId) {
    $.ajax({
        url: 'file/delete',
        method: 'post',
        dataType: 'json',
        data: { fileId: fileId },
        success: () => removeFile(index),
    });
}

function removeFile(index) {
    const uploadedFile = document.getElementsByName(`UploadedFile[${index}]`)[0];

    uploadedFile.remove();
}