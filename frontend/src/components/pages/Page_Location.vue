<template>
    <div>
        <Input_Modal ref="addModal" :columns="columns" @save="addItem" :title="'Add'" />
        <Input_Modal ref="editModal" :columns="columns" @save="editItem" :title="'Edit'" />
        <Import_Modal ref="importModal" :columns="columns" @save="uploadItems" :title="'Import'" />
        <Export_Modal ref="exportModal" :title="'Export'" />
    </div>
    <div>
        <button class="btn btn-primary  m-1" @click="showAddModel">Add</button>
        <button class="btn btn-primary  m-1" @click="showImportModel">Import</button>
        <button class="btn btn-primary  m-1" @click="showExportModel">Export</button>
        <button class="btn btn-primary  m-1" @click="getItems">Refresh</button>
    </div>
    <div>
        <input type="text" v-model="searchValue" placeholder="Search...">
    </div>

    <EasyDataTable :headers="headers" :items="items" :rows-items="[5, 10, 20, 50, 100]" :rows-per-page="10"
        :loading="loading" :server-items-length="serverItemsLength" v-model:server-options="serverOptions" border-cell
        buttons-pagination show-index>
        <template #item-name="{ code, name }">
            <RouterLink :to="`${useRouter().currentRoute.value.path}/${code}`">{{ name }}</RouterLink>
        </template>
        <template #item-operation="item">
            <button class="btn btn-warning m-1" @click="showEditModel(item)">Edit</button>
            <button class="btn btn-danger  m-1" @click="deleteItem(item)">Delete</button>
        </template>
    </EasyDataTable>

</template>

<script setup>
import { ref, watch, onMounted } from "vue";
import { useRouter } from "vue-router"
import { debounce } from "lodash";
import EasyDataTable from 'vue3-easy-data-table';
import { Input_Modal, Import_Modal, Export_Modal } from "@/components/elements";
import { getAPIUrl, childApiUrl } from '@/config.js'
import $ from "jquery";


import 'vue3-easy-data-table/dist/style.css';

const apiUrl = getAPIUrl() + childApiUrl.location.url;

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
const editModal = ref(null);
const importModal = ref(null);
const exportModal = ref(null);




const cleanItem = (item) => {
    const { key, index, ...cleanedItem } = item;
    return cleanedItem;
};

const showAddModel = () => {
    addModal.value.modalToggle();
};

const showEditModel = (item) => {
    editModal.value.modalToggle(item);
};

const showExportModel = () => {
    exportModal.value.modalToggle(items.value);
};

const showImportModel = () => {
    importModal.value.modalToggle();
};

const getItems = async () => {
    loading.value = true;
    try {
        const {
            data,
            recordsTotal,
        } = await $.get(apiUrl, {
            take: serverOptions.value.rowsPerPage,
            skip: (serverOptions.value.page - 1) * serverOptions.value.rowsPerPage,
            search: searchValue.value,
        })
        //console.log(data);
        items.value = data;
        serverItemsLength.value = recordsTotal;
    }
    catch (error) {
        console.log(error);
    }
    loading.value = false;
};

const addItem = (item, closeModal) => {
    console.log('Add', cleanItem(item));
    var settings = {
        "url": apiUrl + childApiUrl.location.add,
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(item),
    };
    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        closeModal();
        getItems();
    })

};

const editItem = (item, closeModal) => {
    console.log("Edit", cleanItem(item));
    var settings = {
        "url": apiUrl + childApiUrl.location.update,
        "method": "PUT",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(item),
    };
    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        getItems();
        closeModal();
    })

};

const deleteItem = (item) => {
    console.log("Delete", item);
    var settings = {
        "url": `${apiUrl}${childApiUrl.location.delete}?id=${item[childApiUrl.location.primarykey]}`,
        "method": "DELETE",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        // "data": JSON.stringify(item),
    };
    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        getItems();
    })
};

const uploadItems = (items, doneFunct, ErrorFunct) => {
    console.log("Upload", items);
    var settings = {
        "url": apiUrl + childApiUrl.location.upload,
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(items),
    };
    $.ajax(settings).done(function (data, textStatus, jqXHR) {
        getItems();
        doneFunct();
    }).fail(function (jqXHR, textStatus, errorThrown) {
        ErrorFunct();
    });
};

const debouncedLoadFromServer = debounce(getItems, 500);

onMounted(() => {
    getItems();
    //const router = useRouter();
    //console.log(router.currentRoute.value.path);
});

watch(serverOptions, () => { getItems(); }, { deep: true });
watch(searchValue, () => { debouncedLoadFromServer(); }, { deep: true });

</script>