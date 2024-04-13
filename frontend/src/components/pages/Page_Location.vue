<template>
    <Input_Modal ref="addModal" :columns="columns" @save="addItem" />

    <div>
        <div>
            <button @click="showAddModel">Add</button>
        </div>
        <div>
            <input type="text" v-model="searchValue" placeholder="Search...">
        </div>

        <EasyDataTable :headers="headers" :items="items" :rows-items="[5, 10, 20, 50, 100]" :rows-per-page="10"
            :loading="loading" :server-items-length="serverItemsLength" v-model:server-options="serverOptions"
            border-cell buttons-pagination show-index>
            <template #item-operation="item">
                <button @click="editItem(item)">Edit</button>
                <button @click="deleteItem(item)">Delete</button>
            </template>
        </EasyDataTable>
    </div>

</template>

<script setup>
import $ from "jquery";
import EasyDataTable from 'vue3-easy-data-table';
// import type { Header, Item, ServerOptions } from "vue3-easy-data-table";
import 'vue3-easy-data-table/dist/style.css';
import { ref, watch, onMounted } from "vue";
import { debounce } from "lodash";
import { Input_Modal } from "@/components/elements"
import {getAPIUrl} from '@/config.js'

const apiUrl = getAPIUrl();

const columns = [
    { text: 'Mã', value: 'code' },
    { text: 'Tên Khu Vực', value: 'name' },
    { text: 'Mô tả', value: 'description' },
]

const headers = columns.concat([{ text: 'Thao tác', value: 'operation' }]);

const items = ref([]);
const loading = ref(false);
const serverItemsLength = ref(0);
const searchValue = ref("");
const serverOptions = ref({
    page: 1,
    rowsPerPage: 10,
    sortBy: ["locationId"],
    sortType: ["asc"],
});

const addModal = ref(null);

const loadFromServer = async () => {
    loading.value = true;
    const {
        data,
        recordsTotal,
    } = await $.get(apiUrl + "Locations", {
        take: serverOptions.value.rowsPerPage,
        skip: (serverOptions.value.page - 1) * serverOptions.value.rowsPerPage,
        search: searchValue.value,
    })
    //console.log(data);
    items.value = data;
    serverItemsLength.value = recordsTotal;
    loading.value = false;
};

const cleanItem = (item) => {
    const { key, index, ...cleanedItem } = item;
    return cleanedItem;
};

const showAddModel = () => {
    addModal.value.modalToggle();
};

const addItem = (item, closeModal) => {
    console.log('Add', cleanItem(item));
    var settings = {
        "url": apiUrl + "Locations/add",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "code": item.code,
            "name": item.name,
            "description": item.description
        }),
    };
    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        closeModal();
        loadFromServer();
    })

};

const editItem = (item) => {
    console.log("Edit", cleanItem(item));
};

const deleteItem = (item) => {
    console.log("Delete", cleanItem(item));
};

const debouncedLoadFromServer = debounce(loadFromServer, 500);

onMounted(() => {
    console.log(serverOptions.value);
    loadFromServer();
    console.log(addModal.value);
});

watch(serverOptions, () => { loadFromServer(); }, { deep: true });
watch(searchValue, () => { debouncedLoadFromServer(); }, { deep: true });

</script>