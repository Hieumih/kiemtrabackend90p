<template>
    <div class="calendar" ref="calenderDivRef">
        <div class="calendar-navigator row">
            <div class="title col">
                {{ dateRef }}
            </div>
            <div class="button-list col justify-content-center view">
                <ul ref="viewRef">
                    <li><button class="start" @click="viewMouth">month</button></li>
                    <li><button class="mid" @click="viewWeek">week</button></li>
                    <li><button class="mid" @click="viewDay">day</button></li>
                    <li><button class="end" @click="viewList">list</button></li>
                </ul>
            </div>
            <div class="button-list col justify-content-end">
                <ul class="pagination">
                    <li class=""><button class="start" @click="prev">prev</button></li>
                    <li class=""><button class="mid" @click="today" :disabled="isToday">today</button></li>
                    <li class=""><button class="end" @click="next">next</button></li>
                </ul>
            </div>
        </div>
        <div class="calendar-container">
            <FullCalendar :options="option" ref="calendar">
                <template v-slot:eventContent='arg'>
                    <b>{{ arg.event.title }}</b>
                </template>
            </FullCalendar>
        </div>
    </div>
</template>


<script setup>
import { ref, onMounted, watch } from 'vue';
import FullCalendar from '@fullcalendar/vue3';
import DayGridPlugin from '@fullcalendar/daygrid';
import TimeGridPlugin from '@fullcalendar/timegrid';
import ListPlugin from '@fullcalendar/list';
import ScorllGridPlugin from '@fullcalendar/scrollgrid';
import InteractionPlugin, { Draggable } from '@fullcalendar/interaction';
import $ from 'jquery';

const option = ref({
    // schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
    plugins: [
        DayGridPlugin,
        TimeGridPlugin,
        InteractionPlugin,
        ListPlugin,
        // ScorllGridPlugin,
    ],
    height: "auto",
    initialView: 'timeGridWeek',
    // headerToolbar: {
    //     left: 'title',
    //     center: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek',
    //     right: 'prev,today,next',
    // },
    headerToolbar: false,
    events: [
        // { title: 'event 2', date: '2024-05-02' },
    ],
    selectable: true,
    select: (info) => {
        option.value.events = option.value.events.concat({
            title: 'test',
            start: info.startStr,
            end: info.endStr,
        });
    },
    editable: true,
    droppable: true,
    expandRows: true,

});

const calenderDivRef = ref(null);
const calendar = ref(null);
const viewRef = ref(null);
const dateRef = ref();
let calendarApi = ref();
let isToday = ref(true);


const next = () => {
    isToday.value = false;
    calendarApi.value.next();
    dateRef.value = calendarApi.value.view.title;
};

const prev = () => {
    isToday.value = false;
    calendarApi.value.prev();
    dateRef.value = calendarApi.value.view.title;
};

const today = () => {
    isToday.value = true;
    calendarApi.value.today();
    dateRef.value = calendarApi.value.view.title;
};

const viewActve = (e) => {
    console.log(e);
    const buttons = viewRef.value.querySelectorAll('button');
    buttons.forEach((button) => {
        button.classList.remove('active');
    });
    e.target.classList.add('active');
    dateRef.value = calendarApi.value.view.title;
    
};

const viewMouth = (e) => {
    viewActve(e);
    calendarApi.value.changeView('dayGridMonth');
};

const viewWeek = (e) => {
    viewActve(e);
    calendarApi.value.changeView('timeGridWeek');
};

const viewDay = (e) => {
    viewActve(e);
    calendarApi.value.changeView('timeGridDay');
};

const viewList = (e) => {
    viewActve(e);
    calendarApi.value.changeView('listWeek');
}

onMounted(() => {
    if (calendar.value) {
        calendarApi.value = calendar.value.getApi();
        dateRef.value = calendarApi.value.view.title;
    }
});

// watch calenderDivRef resize then resize calendar
watch(calenderDivRef, (newVal, oldVal) => {
    if (newVal) {
        const resizeObserver = new ResizeObserver(() => {
            calendarApi.value.updateSize();
        });
        resizeObserver.observe(newVal);
    }
});

</script>

<style scoped>
.calendar {
    margin: 16px;
    width: 80%;
    overflow: hidden;
    height: calc(100% - 16px * 2);
}

.calendar-container {
    max-height: calc(90% - 16px * 2);
    overflow-y: auto;

}

.calendar-navigator {
    /* display: c; */
    margin-bottom: 16px;
}

.title {
    font-size: 24px;
}

.button-list {
    display: flex;
    justify-content: center;
    align-items: center;
    list-style: none;
}

.button-list ul {
    display: flex;
    justify-content: center;
    align-items: center;
    list-style: none;
    margin: 0;
    padding: 0;
}

.button-list ul li button {
    /* margin: 0 8px; */
    padding: 8px 16px;
    border: 1px solid #000;
    /* border-radius: 4px; */
    background-color: #fff;
    cursor: pointer;
}

.button-list ul li button:hover {
    background-color: #f0f0f0;

}

.button-list ul li button.start {
    /* border-right-color: transparent; */
    border-top-left-radius: 4px;
    border-bottom-left-radius: 4px;
}

.button-list ul li button.mid {
    border-left-color: transparent;
    /* border-right-color: transparent; */
}

/* .button-list ul li button.mid:focus {
    pointer-events: none;
} */

.button-list ul li button.end {
    border-left-color: transparent;
    border-top-right-radius: 4px;
    border-bottom-right-radius: 4px;
}

.button-list.view ul li button.active {
    background-color: #f0f0f0;
}
</style>
