<template>
    <div>
        <div class="card-container container-dashboard">
            <draggable :list="dashboardItems" group="dashboardItems" item-key="id" class="list" v-bind="dragOptions"
                @start="isDrag = true" @end="isDrag = false" drag-class="drag" ghost-class="ghost" tag="ul">
                <transition-group name="flip-list">
                    <Dashboard_Card :class="`card-dashboard ${element.class}`" v-for="element in dashboardItems"
                        :key="element.id">
                        <template #title>
                            <h1>{{ element.body }}</h1>
                        </template>
                        <template #body>
                            {{ element.title }}
                        </template>
                        <template #icon>
                            <i :class="element.icon"></i>
                        </template>
                    </Dashboard_Card>
                </transition-group>
            </draggable>
        </div>
        <div class="recent-container container-dashboard">
            <h3>Recent activity</h3>
            <div class="table_container">
                <EasyDataTable :headers="headers" :items="[]" border-cell buttons-pagination show-index :hide-footer="true"
                >
                    <template #item-title="{ title, activity_type }">
                        <strong>{{ title }}</strong>
                    </template>
                </EasyDataTable>
            </div>
        </div>
    </div>
</template>


<script setup>
import { ref, onMounted } from 'vue';
import { Dashboard_Card } from '@/components/elements/index'
import { VueDraggableNext as draggable } from 'vue-draggable-next'
import EasyDataTable from 'vue3-easy-data-table';

import 'vue3-easy-data-table/dist/style.css';



const dashboardItems = ref([
    {
        id: 1,
        title: 'Product',
        icon: 'bi bi-box',
        body: '200000',
        class: 'product'
    },
    {
        id: 2,
        title: 'Order',
        icon: 'bi bi-person-badge',
        body: '200000',
        class: 'order'
    },
    {
        id: 3,
        title: 'Customer',
        icon: 'bi bi-people-fill',
        body: '200000',
        class: 'customer'
    },
    {
        id: 4,
        title: 'Account',
        icon: 'bi bi-person-badge',
        body: '200000',
        class: 'account'
    },
    {
        id: 5,
        title: 'Account',
        icon: 'bi bi-person-badge',
        body: '200000',
        class: 'account'
    }
]);


const headers = ref([
    { text: 'Title', value: 'title_name' },
    { text: 'Date', value: 'date' },
    { text: 'Description', value: 'description' }
])



const isDrag = ref(false)

const dragOptions = () => {
    return {
        animation: 200,
        group: "description",
        disabled: false,
        ghostClass: "ghost"
    }
}

onMounted(() => {
})

</script>

<style scoped lang="scss">
.container-dashboard {
    padding: 10px;
}

.card-container .list {
    display: flex;
    flex-wrap: wrap;
    justify-content: flex-start;
    list-style: none;
    padding: 0;
}

.card-container:deep() .card-dashboard .card-dashboard-icon {
    color: #000;
}

.card-container:deep() li.product .card-dashboard {
    background: lightblue;
}

.card-container:deep() li.customer .card-dashboard {
    background: lightgreen;
}

.card-container:deep() li.account .card-dashboard {
    background: lightcoral;
}

.card-container:deep() li.order .card-dashboard {
    background: lightcoral;
}

.flip-list-move {
    transition: transform 0.5s;
}

.no-move {
    transition: transform 0s;
}
</style>