let datesIndex = (document.getElementsByClassName('uploaded-eventdate').length == 0) ? document.getElementsByClassName('uploaded-eventdate').length + 1 : document.getElementsByClassName('uploaded-eventdate').length;
let fileIndex = 0;
let fieldIndex = 0;

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

    addDateButton.addEventListener('click', addDateClick);
    addFileButton.addEventListener('click', addFileClick);
    addFieldButton.addEventListener('click', addFieldClick);

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
});

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

function removeFieldClick() {
    if (fieldIndex > 0) fieldIndex--;

    document.getElementById(`${fieldIndex}-field-element`).remove();

    if (!document.getElementById(`${fieldIndex - 1}-field-element`)) {
        document.getElementById('remove-opt-field-button').remove();
    }
}

function addRemovingFieldButton() {
    const fieldAdder = document.getElementById('optional-field-adder');
    const removeFieldButton = document.createElement('button');

    removeFieldButton.setAttribute('type', 'button');
    removeFieldButton.setAttribute('id', 'remove-opt-field-button');
    removeFieldButton.innerHTML = 'Удалить поле';

    fieldAdder.appendChild(removeFieldButton);

    removeFieldButton.addEventListener('click', removeFieldClick);
}

function addFieldClick() {
    const fieldHolder = document.getElementById('field-holder');

    const fieldPickerDiv = document.createElement('div');
    fieldPickerDiv.innerHTML = fieldComponent;
    fieldPickerDiv.setAttribute('id', `${fieldIndex}-field-element`);

    const titleInput = fieldPickerDiv.getElementsByClassName('title')[0];
    const selecterInput = fieldPickerDiv.getElementsByClassName('input-type-select')[0];

    titleInput.name = `OptionalFields[${fieldIndex}][Title]`;
    selecterInput.name = `OptionalFields[${fieldIndex}][Type]`;

    fieldIndex++;

    if (!document.getElementById('remove-opt-field-button')) {
        addRemovingFieldButton();
    }

    fieldHolder.appendChild(fieldPickerDiv);
}

function removeFileClick() {
    if (fileIndex > 0) fileIndex--;

    document.getElementById(`${fileIndex}-file`).remove();

    if (!document.getElementById(`${fileIndex - 1}-file`)) {
        document.getElementById('remove-file-button').remove();
    }
}

function addRemovingFileButton() {
    const fileLoader = document.getElementById('file-loader');
    const removeFileButton = document.createElement('button');

    removeFileButton.setAttribute('type', 'button');
    removeFileButton.setAttribute('id', 'remove-file-button');
    removeFileButton.innerHTML = 'Удалить файл';

    fileLoader.appendChild(removeFileButton);

    removeFileButton.addEventListener('click', removeFileClick);
}

function addFileClick() {
    const fileHolder = document.getElementById('file-holder');

    const fileInput = document.createElement('input');
    fileInput.setAttribute('id', `${fileIndex}-file`);
    fileInput.setAttribute('class', 'loading-file');
    fileInput.setAttribute('type', 'file');
    fileInput.setAttribute('name', `EventFiles`);

    fileIndex++;

    if (!document.getElementById('remove-file-button')) {
        addRemovingFileButton();
    }

    fileHolder.appendChild(fileInput);
}

function removeDateClick() {
    if (datesIndex > 1) datesIndex--;

    document.getElementById(`${datesIndex}-date-element`).remove();

    if (!document.getElementById(`${datesIndex - 2}-date-element`)) {
        document.getElementById('remove-date-button').remove();
    }
}

function addRemovingDateButton() {
    const dateHolder = document.getElementsByClassName('date-adder')[0];
    const removeDateButton = document.createElement('button');

    removeDateButton.setAttribute('type', 'button');
    removeDateButton.setAttribute('id', 'remove-date-button');
    removeDateButton.innerHTML = 'Удалить слот';

    dateHolder.appendChild(removeDateButton);

    removeDateButton.addEventListener('click', removeDateClick);
}

function addDateClick() {
    const dateHolder = document.getElementById('date-holder');

    const datePickerDiv = document.createElement('div');
    datePickerDiv.innerHTML = datePickerComponent;
    datePickerDiv.setAttribute('id', `${datesIndex}-date-element`);

    const dateInput = datePickerDiv.getElementsByClassName('date')[0];
    const startTimeInput = datePickerDiv.getElementsByClassName('starttime')[0];
    const endTimeInput = datePickerDiv.getElementsByClassName('endtime')[0];
    const availableSeats = datePickerDiv.getElementsByClassName('availableseats')[0];
    const id = datePickerDiv.getElementsByClassName('id')[0];

    dateInput.name = `EventDates[${datesIndex}][Date]`;
    startTimeInput.name = `EventDates[${datesIndex}][TimeSlot.TimeRange.StartTime]`;
    endTimeInput.name = `EventDates[${datesIndex}][TimeSlot.TimeRange.EndTime]`;
    availableSeats.name = `EventDates[${datesIndex}][TimeSlot.AvailableSeats]`;
    id.name = `EventDates[${datesIndex}][Id]`;
    id.value = crypto.randomUUID();

    // Установить минимальную дату для нового поля
    setMinDate(dateInput);

    datesIndex++;

    if (!document.getElementById('remove-date-button')) {
        addRemovingDateButton();
    }

    dateHolder.appendChild(datePickerDiv);
}
