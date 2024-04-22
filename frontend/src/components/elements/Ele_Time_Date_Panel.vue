<template>
    <div class="time">
        <div class="clock">
            <span class="hour" ref="hourRef">00</span> :
            <span class="minute" ref="minuteRef">00</span> :
            <span class="second" ref="secondRef">00</span>
        </div>
        <div class="date-time mb-4 " ref="date-Time">
            <div class="date day">
                <div class="text">{{ dayText }}</div>
                <div class="number" ref="dayRef">00</div>
            </div>
            <div class="date month">
                <div class="text">{{ monthText }}</div>
                <div class="number" ref="monthRef">00</div>
            </div>
            <div class="date year">
                <div class="text">{{ yearText }}</div>
                <div class="number" ref="yearRef">0000</div>
            </div>
        </div>
        <div class="time-zone">
            <select name="" id="" v-model="timeZoneSelected" class=" select-zone">
                <!-- <option value="">Select Timezone</option> -->
                <template v-for="(item, index) in timezones" :key="index">
                    <option :value="item.value">{{ item.text }}</option>
                </template>
            </select>
        </div>
    </div>
</template>


<script setup>
import { ref, onMounted, defineProps } from 'vue';
import { formatTimeZoneJsonList } from "~common/common"
import moment from 'moment-timezone';

const hourRef = ref(null);
const minuteRef = ref(null);
const secondRef = ref(null);
const dayRef = ref(null);
const monthRef = ref(null);
const yearRef = ref(null);

const props = defineProps({
    dayText: {
        type: String,
        default: 'Day'
    },
    monthText: {
        type: String,
        default: 'Month'
    },
    yearText: {
        type: String,
        default: 'Year'
    }
});

const timezones = ref(formatTimeZoneJsonList());
const timeZoneSelected = ref(moment.tz.guess());

const change_clockTime = () => {
    // use time zone
    const time = moment().tz(timeZoneSelected.value);
    if (hourRef.value) hourRef.value.innerText = time.format('HH');
    if (minuteRef.value) minuteRef.value.innerText = time.format('mm');
    if (secondRef.value) secondRef.value.innerText = time.format('ss');
    if (dayRef.value) dayRef.value.innerText = time.format('DD');
    if (monthRef.value) monthRef.value.innerText = time.format('MM');
    if (yearRef.value) yearRef.value.innerText = time.format('YYYY');
}

onMounted(() => {
    setInterval(() => {
        change_clockTime();
    }, 1000);

});
</script>


<style scoped lang="scss">
.time {
    /* background: #fff; */
    width: 100%;
    height: 400px;
    margin-right: 2px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    .clock {
        font-size: 70px;
        font-weight: 450;
    }

    .date-time {
        display: flex;
        justify-content: space-around;
        width: 100%;
        margin-top: 20px;

        .date {
            display: flex;
            flex-direction: column;
            align-items: center;
            width: 33.33%;

            .text {
                font-size: 20px;
                font-weight: 400;
            }
        }

        .number {
            font-size: 50px;
            font-weight: 450;
        }

        .date.month {
            border-left: 1px solid;
            border-right: 1px solid;
        }
    }

    .time-zone {
        margin-top: 20px;

        .select-zone {
            width: 100%;
            padding: 10px;
            border: 1px solid;
            border-radius: 5px;
            background: transparent;
            cursor: pointer;
            outline: none;
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            text-align: center;

            option {
                padding: 10px;
                border: 1px solid;
                border-radius: 5px;
                background: #fff;
                cursor: pointer;
                outline: none;
                appearance: none;
                -webkit-appearance: none;
                -moz-appearance: none;
                text-align: center;
                color: #000;

            }
        }

    }
}
</style>