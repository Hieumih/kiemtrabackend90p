<template>
    <li>
        <div class="card-dashboard">
                <div class="card-dashboard-info">
                    <div class="card-dashboard-title">
                        <slot name="title"></slot>
                    </div>

                    <div class="card-dashboard-body">
                        <slot name="body"></slot>
                    </div>
                </div>
                <div class="card-dashboard-icon">
                    <slot name="icon"></slot>
                </div>
            </div>
    </li>
</template>


<script setup>
import { defineProps, onMounted, defineEmits } from 'vue';

const props = defineProps({

});


</script>


<style scoped>
li {
    list-style: none;
}


.card-dashboard {
    display: block;
    background-color: #fff;
    border-radius: 10px;
    /* box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); */
    /* margin: 10px; */
    /* padding: 10px; */
    width: 300px;
    height: 150px;
    overflow: hidden;
    /* list-style: none; */
    margin-bottom: 5px;
    margin-right: 5px;

}

.card-dashboard-info {
    z-index: 1;
    position: relative;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100%;
}

.card-dashboard-title {
    font-size: 24px;
    font-weight: bold;
    padding: 10px;
}

.card-dashboard-body {
    padding: 10px;
}

.card-dashboard-icon {
    position: sticky;
    /* right: 10px;
    top: 10px; */
    font-size: 100px;
    bottom: 0px;
    display: flex;
    justify-content: flex-end;
    color: #f0f0f0;
}

.ghost .card-dashboard {
    /* background: #f0f0f0;
    border: 1px solid #e0e0e0;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); */
    visibility: hidden;
    opacity: 1;
    /* height: 100%; */
}

.ghost {
    background-color: lightgrey;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    /* transform: rotate(10deg); */
    color: red;

}


.drag .card-dashboard {
    /* background-color: #f9f9f9; */
    /* border: 1px solid #ddd; */
    /* box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); */
    opacity: 1;
    /* animation: shake 2s infinite; */
}

/* li > div {
    rotate: 90deg;
} */






@keyframes shake {
    0% {
        transform: translate(1px, 1px) rotate(0deg);
    }

    10% {
        transform: translate(-1px, -2px) rotate(-5deg);
    }

    20% {
        transform: translate(-3px, 0px) rotate(5deg);
    }

    30% {
        transform: translate(3px, 2px) rotate(0deg);
    }

    40% {
        transform: translate(1px, -1px) rotate(5deg);
    }

    50% {
        transform: translate(-1px, 2px) rotate(-5deg);
    }

    60% {
        transform: translate(-3px, 1px) rotate(0deg);
    }

    70% {
        transform: translate(3px, 1px) rotate(-5deg);
    }

    80% {
        transform: translate(-1px, -1px) rotate(5deg);
    }

    90% {
        transform: translate(1px, 2px) rotate(0deg);
    }

    100% {
        transform: translate(1px, -2px) rotate(-5deg);
    }
}
</style>