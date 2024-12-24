let addedDatesIndex = (document.getElementsByClassName('uploaded-eventdate').length == 0) ? document.getElementsByClassName('uploaded-eventdate').length + 1 : document.getElementsByClassName('uploaded-eventdate').length;
let newDatesIndex = 0;
let removingDatesIndex = 0;

let newFileIndex = 0;
let addedFileIndex = document.getElementsByClassName('uploaded-file').length;
let removingFileIndex = 0;

let newFieldIndex = 0;
let addedFieldIndex = document.getElementsByClassName('uploaded-field').length
let removingFieldIndex = 0;

const datePickerComponent = `
    <input class="date" type="date" />
    <input class="starttime" type="time" />
    <input class="endtime" type="time" />
    <input class="availableseats" type="number" placeholder="Количество мест" />
    <input class="id" type="hidden" />`;

const fieldComponent = `
    <input class="title" type="text" required />
    <select class="input-type-select" required>
        <option value="text">Текст</option>
        <option value="number">Число</option>
        <option value="date">Дата</option>
        <option value="file">Файл</option>
    </select>`;

$(document).ready(function () {
    const addDateButton = document.getElementById('add-date-button');
    const addFileButton = document.getElementById('add-file-button');
    const addFieldButton = document.getElementById('add-opt-field-button');

    addDateButton.addEventListener('click', addNewDateClick);
    addFileButton.addEventListener('click', addNewFileClick);
    addFieldButton.addEventListener('click', addNewFieldClick);

    // Установить минимальную дату при загрузке страницы для существующих полей
    setMinDateForExistingFields();

    // Добавить проверку даты на изменение
    $(document).on('change', '.date', function () {
        validateDate(this);
    });

    // Проверка значений starttime, endtime и availableseats
    $(document).on('change', '.starttime, .endtime, .availableseats', function () {
        const dateElement = $(this).closest('[id$="-date-element"]');

        const startTime = dateElement.find('.starttime').val();
        const endTime = dateElement.find('.endtime').val();
        const seats = dateElement.find('.availableseats').val();

        // Проверка времени
        if (startTime && endTime && startTime === endTime) {
            alert("Время начала и окончания не могут быть одинаковыми!");
            dateElement.find('.endtime').val(''); // Очистить endtime
        } else if (startTime && endTime && startTime > endTime) {
            alert("Время окончания не может быть раньше времени начала!");
            dateElement.find('.endtime').val(''); // Очистить endtime
        }

        // Проверка количества мест
        if (seats < 1) {
            alert("Количество мест не может быть меньше 1!");
            dateElement.find('.availableseats').val(1); // Установить значение по умолчанию
        }
    });

    const deleteFileButtons = document.getElementsByClassName('delete-file-button');
    const deleteEventDateButtons = document.getElementsByClassName('delete-eventdate-button');
    const uploadedEventDateDivs = document.getElementsByClassName('uploaded-eventdate');

    addRemovingAddedFileButton();
    addRemovingAddedDateButton();
    addRemovingAddedFieldButton();
})  

function removeEventDate() {
    const uploadedEventDate = document.getElementById(`${datesIndex - 1}-date-element-uploaded`);

    uploadedEventDate.remove();
    datesIndex--;
}

// Функция для установки минимальной даты
function setMinDate(input) {
    const today = new Date().toISOString().split('T')[0]; // Получить дату в формате YYYY-MM-DD
    input.setAttribute('min', today);
}

// Устанавливаем минимальную дату для существующих полей
function setMinDateForExistingFields() {
    document.querySelectorAll('.date').forEach(setMinDate);
}

// Проверка даты на изменение
function validateDate(input) {
    const today = new Date().toISOString().split('T')[0];
    if (input.value < today) {
        alert("Дата не может быть меньше сегодняшней!");
        input.value = today; // Сбрасываем на минимальную дату
    }
}

function removeAddedFieldClick() {
    if (addedFieldIndex > 0) addedFieldIndex--;

    const addedField = document.getElementById(`${addedFieldIndex}-added-field-element`);
    addedField.setAttribute('class', 'deleted-field');
    addedField.setAttribute('name', `DeletedOptionalFields[${removingFieldIndex}]`);
    addedField.setAttribute('id', `${addedFieldIndex}-deleted-field-element`);

    const title = addedField.getElementsByClassName('title')[0];
    const type = addedField.getElementsByClassName('input-type-select')[0];
    const id = addedField.getElementsByClassName('id')[0];

    title.name = `DeletedOptionalFields[${removingFieldIndex}][Title]`;
    type.name = `DeletedOptionalFields[${removingFieldIndex}][Type]`;
    id.name = `DeletedOptionalFields[${removingFieldIndex}][Id]`;

    addedField.style.display = 'none';

    removingFieldIndex++;

    if (!document.getElementById(`${addedFieldIndex - 1}-added-field-element`)) {
        document.getElementById('remove-added-field-button').remove();
    }
}

function addRemovingAddedFieldButton() {
    if (addedFieldIndex > 0) {
        const fieldAdder = document.getElementById('optional-field-adder');
        const removeAddedFieldButton = document.createElement('button');

        removeAddedFieldButton.setAttribute('type', 'button');
        removeAddedFieldButton.setAttribute('id', 'remove-added-field-button');
        removeAddedFieldButton.innerHTML = 'Удалить ранее добавленный слот';
        removeAddedFieldButton.style.marginBottom = '20px';

        fieldAdder.appendChild(removeAddedFieldButton);

        removeAddedFieldButton.addEventListener('click', removeAddedFieldClick);
    }
}

function removeNewFieldClick() {
    if (newFieldIndex > 0) newFieldIndex--;

    document.getElementById(`${newFieldIndex}-new-field-element`).remove();

    if (!document.getElementById(`${newFieldIndex - 1}-new-field-element`)) {
        document.getElementById('remove-opt-new-field-button').remove();
    }
}

function addRemovingNewFieldButton() {
    const fieldAdder = document.getElementById('optional-field-adder');
    const removeFieldButton = document.createElement('button');

    removeFieldButton.setAttribute('type', 'button');
    removeFieldButton.setAttribute('id', 'remove-opt-new-field-button');
    removeFieldButton.innerHTML = 'Удалить добавляемое поле';

    fieldAdder.appendChild(removeFieldButton);

    removeFieldButton.addEventListener('click', removeNewFieldClick);
}

function addNewFieldClick() {
    const fieldHolder = document.getElementById('field-holder');

    const fieldPickerDiv = document.createElement('div');
    fieldPickerDiv.innerHTML = fieldComponent;
    fieldPickerDiv.setAttribute('id', `${newFieldIndex}-new-field-element`);

    const titleInput = fieldPickerDiv.getElementsByClassName('title')[0];
    const selecterInput = fieldPickerDiv.getElementsByClassName('input-type-select')[0];

    titleInput.name = `NewOptionalFields[${newFieldIndex}][Title]`;
    selecterInput.name = `NewOptionalFields[${newFieldIndex}][Type]`;

    newFieldIndex++;

    if (!document.getElementById('remove-opt-new-field-button')) {
        addRemovingNewFieldButton();
    }

    fieldHolder.appendChild(fieldPickerDiv);
}

function removeAddedFileClick() {
    if (addedFileIndex > 0) addedFileIndex--;

    const addedFile = document.getElementById(`added-file-${addedFileIndex}`);
    addedFile.setAttribute('class', 'deleted-file');
    addedFile.setAttribute('name', `DeletedEventFiles[${removingFileIndex}]`);
    addedFile.setAttribute('id', `deleted-file-${removingFileIndex}`);

    const filePath = addedFile.getElementsByClassName(`filepath-${addedFileIndex}`)[0];
    const fileName = addedFile.getElementsByClassName(`filename-${addedFileIndex}`)[0];
    const fileId = addedFile.getElementsByClassName(`fileid-${addedFileIndex}`)[0];

    filePath.name = `DeletedEventFiles[${removingFileIndex}][FilePath]`;
    fileName.name = `DeletedEventFiles[${removingFileIndex}][FileName]`;
    fileId.name = `DeletedEventFiles[${removingFileIndex}][Id]`;

    addedFile.style.display = 'none';

    removingFileIndex++;

    if (!document.getElementById(`added-file-${removingFileIndex - 1}`)) {
        document.getElementById('remove-added-file-button').remove();
    }
}

function addRemovingAddedFileButton() {
    if (addedFileIndex > 0) {
        const fileLoader = document.getElementById('file-loader');
        const removeAddedFileButton = document.createElement('button');

        removeAddedFileButton.setAttribute('type', 'button');
        removeAddedFileButton.setAttribute('id', 'remove-added-file-button');
        removeAddedFileButton.innerHTML = 'Удалить ранее загруженный файл';
        removeAddedFileButton.style.marginBottom = '20px';

        fileLoader.appendChild(removeAddedFileButton);

        removeAddedFileButton.addEventListener('click', removeAddedFileClick);
    }
}

function removeNewFileClick() {
    if (newFileIndex > 0) newFileIndex--;

    document.getElementById(`new-file-${newFileIndex}`).remove();

    if (!document.getElementById(`new-file-${newFileIndex - 1}`)) {
        document.getElementById('remove-file-button').remove();
    }
}

function addRemovingNewFileButton() {
    const fileHolder = document.getElementById('file-holder');
    const removeNewFileButton = document.createElement('button');

    removeNewFileButton.setAttribute('type', 'button');
    removeNewFileButton.setAttribute('id', 'remove-file-button');
    removeNewFileButton.innerHTML = 'Удалить загружаемый файл';

    fileHolder.appendChild(removeNewFileButton);

    removeNewFileButton.addEventListener('click', removeNewFileClick);
}

function addNewFileClick() {
    const fileHolder = document.getElementById('file-holder');

    const fileInput = document.createElement('input');
    fileInput.setAttribute('id', `new-file-${newFileIndex}`);
    fileInput.setAttribute('class', 'loading-file');
    fileInput.setAttribute('type', 'file');
    fileInput.setAttribute('name', `NewEventFiles`);

    newFileIndex++;

    if (!document.getElementById('remove-file-button')) {
        addRemovingNewFileButton();
    }

    fileHolder.appendChild(fileInput);
}

function removeAddedDateClick() {
    if (addedDatesIndex > 0) addedDatesIndex--;

    const addedDate = document.getElementById(`${addedDatesIndex}-added-date-element`);
    addedDate.setAttribute('class', 'deleted-eventdate');
    addedDate.setAttribute('name', `DeletedEventDates[${removingDatesIndex}]`);
    addedDate.setAttribute('id', `${addedDatesIndex}-deleted-date-element`);

    const date = addedDate.getElementsByClassName('date')[0];
    const startTime = addedDate.getElementsByClassName('starttime')[0];
    const endTime = addedDate.getElementsByClassName('endtime')[0];
    const avialableSeats = addedDate.getElementsByClassName('availableseats')[0];
    const id = addedDate.getElementsByClassName('id')[0];

    date.name = `DeletedEventDates[${removingDatesIndex}][Date]`;
    startTime.name = `DeletedEventDates[${removingDatesIndex}][TimeSlot.TimeRange.StartTime]`;
    endTime.name = `DeletedEventDates[${removingDatesIndex}][TimeSlot.TimeRange.EndTime]`;
    avialableSeats.name = `DeletedEventDates[${removingDatesIndex}][TimeSlot.AvailableSeats]`;
    id.name = `DeletedEventDates[${removingDatesIndex}][Id]`;

    addedDate.style.display = 'none';

    removingDatesIndex++;

    if (!document.getElementById(`${addedDatesIndex - 2}-added-date-element`)) {
        document.getElementById('remove-added-date-button').remove();
    }
}

function addRemovingAddedDateButton() {
    if (addedDatesIndex > 1) {
        const dateAdder = document.getElementsByClassName('date-adder')[0];
        const removeAddedDateButton = document.createElement('button');

        removeAddedDateButton.setAttribute('type', 'button');
        removeAddedDateButton.setAttribute('id', 'remove-added-date-button');
        removeAddedDateButton.innerHTML = 'Удалить ранее добавленный слот';
        removeAddedDateButton.style.marginBottom = '20px';

        dateAdder.appendChild(removeAddedDateButton);

        removeAddedDateButton.addEventListener('click', removeAddedDateClick);
    }
}

function removeNewDateClick() {
    if (newDatesIndex > 0) newDatesIndex--;

    document.getElementById(`${newDatesIndex}-new-date-element`).remove();

    if (!document.getElementById(`${newDatesIndex - 1}-new-date-element`)) {
        document.getElementById('remove-new-date-button').remove();
    }
}

function addRemovingNewDateButton() {
    const dateHolder = document.getElementsByClassName('date-adder')[0];
    const removeDateButton = document.createElement('button');

    removeDateButton.setAttribute('type', 'button');
    removeDateButton.setAttribute('id', 'remove-new-date-button');
    removeDateButton.innerHTML = 'Удалить добавляемый слот';

    dateHolder.appendChild(removeDateButton);

    removeDateButton.addEventListener('click', removeNewDateClick);
}

function addNewDateClick() {
    const dateHolder = document.getElementById('date-holder');

    const datePickerDiv = document.createElement('div');
    datePickerDiv.innerHTML = datePickerComponent;
    datePickerDiv.setAttribute('id', `${newDatesIndex}-new-date-element`);

    const dateInput = datePickerDiv.getElementsByClassName('date')[0];
    const startTimeInput = datePickerDiv.getElementsByClassName('starttime')[0];
    const endTimeInput = datePickerDiv.getElementsByClassName('endtime')[0];
    const availableSeats = datePickerDiv.getElementsByClassName('availableseats')[0];
    const id = datePickerDiv.getElementsByClassName('id')[0];

    dateInput.name = `NewEventDates[${newDatesIndex}][Date]`;
    startTimeInput.name = `NewEventDates[${newDatesIndex}][TimeSlot.TimeRange.StartTime]`;
    endTimeInput.name = `NewEventDates[${newDatesIndex}][TimeSlot.TimeRange.EndTime]`;
    availableSeats.name = `NewEventDates[${newDatesIndex}][TimeSlot.AvailableSeats]`;
    id.name = `NewEventDates[${newDatesIndex}][Id]`;
    id.value = crypto.randomUUID();

    // Установить минимальную дату для нового поля
    setMinDate(dateInput);

    newDatesIndex++;

    if (!document.getElementById('remove-new-date-button')) {
        addRemovingNewDateButton();
    }

    dateHolder.appendChild(datePickerDiv);
}