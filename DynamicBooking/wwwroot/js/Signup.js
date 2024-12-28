let selectedEventDatesCount = 0;

$(document).ready(function () {
    AddSelectEventDateButton();
})

function AddSelectEventDateButton() {
    const eventDatesLoader = document.getElementById('event-dates-loader');

    const eventDatesHolders = eventDatesLoader.getElementsByClassName('eventdate-holder');

    for (var i = 0; i < eventDatesHolders.length; i++) {
        const uploadedEventDate = eventDatesHolders[i];

        const selectEventDateButton = uploadedEventDate.getElementsByClassName('select-event-date-button')[0];

        selectEventDateButton.onclick = () => SelectEventDateClick(uploadedEventDate);
    }
}

function SelectEventDateClick(uploadedEventDateHolder) {
    const uploadedEventDate = uploadedEventDateHolder.getElementsByClassName(`uploaded-event-date`)[0];
    const eventDateId = uploadedEventDate.getElementsByClassName('id')[0];
    
    eventDateId.name = `SelectedEventDatesIds[${selectedEventDatesCount}]`;

    selectedEventDatesCount += 1;

    const selectEventDateButton = uploadedEventDateHolder.getElementsByClassName('select-event-date-button')[0];
    selectEventDateButton.style.visibility = 'hidden'

    AddDeselectionEventDate(uploadedEventDateHolder);
}

function AddDeselectionEventDate(uploadedEventDateHolder) {
    if (selectedEventDatesCount > 0) {
        const deselectEventDateButton = document.createElement('button');

        deselectEventDateButton.setAttribute('type', 'button');
        deselectEventDateButton.setAttribute('class', 'deselect-event-date-button');
        deselectEventDateButton.innerHTML = 'Отменить выбор';

        uploadedEventDateHolder.appendChild(deselectEventDateButton);
        deselectEventDateButton.onclick = () => DeselectEventDateClick(uploadedEventDateHolder);
    }
}

function DeselectEventDateClick(uploadedEventDateHolder) {
    const eventDateId = uploadedEventDateHolder.getElementsByClassName('id')[0];
    eventDateId.name = ``;

    selectedEventDatesCount -= 1;

    const deselectEventDateButton = uploadedEventDateHolder.getElementsByClassName('deselect-event-date-button')[0];
    deselectEventDateButton.remove();

    const selectEventDateButton = uploadedEventDateHolder.getElementsByClassName('select-event-date-button')[0];
    selectEventDateButton.style.visibility = 'visible';
}