<template>
    <div>
        <Input_Modal ref="addModal" :columns="columns" @save="addItem" :title="'Add'" />
        <Input_Modal ref="editModal" :columns="columns" @save="editItem" :title="'Edit'" />
        <Import_Modal ref="importModal" :columns="columns" @save="uploadItems" @fileError="fileError"
            :title="'Import'" />
        <Export_Modal ref="exportModal" :title="'Export'">
            <small>Note: Only export data in current page of this table. I will update in future</small>
        </Export_Modal>
        <Conform_Modal ref="conformModal" :title="'Delete'" />

    </div>
    <div>
        <button class="btn btn-primary  m-1" @click="showAddModal">Add</button>
        <button class="btn btn-primary  m-1" @click="showImportModal">Import</button>
        <button class="btn btn-primary  m-1" @click="showExportModal">Export</button>
        <button class="btn btn-primary  m-1" @click="getItems">Refresh</button>
        <button class="btn btn-primary  m-1" @click="deleteItems" :disabled="!itemsSelected.length">Delete Selected</button>
    </div>
    <div>
        <input type="text" v-model="searchValue" placeholder="Search...">
    </div>

    <EasyDataTable :headers="headers" :items="items" :rows-items="[5, 10, 15, 20, 50, 100]" :rows-per-page="10"
        :loading="loading" :server-items-length="serverItemsLength" v-model:server-options="serverOptions"
        v-model:items-selected="itemsSelected" border-cell buttons-pagination show-index>
        <template #item-name="{ code, name }">
            <RouterLink :to="`${useRouter().currentRoute.value.path}/${code}`">{{ name }}</RouterLink>
        </template>
        <template #item-operation="item">
            <button class="btn btn-warning m-1" @click="showEditModal(item)">Edit</button>
            <button class="btn btn-danger  m-1" @click="deleteItem(item)">Delete</button>
        </template>
    </EasyDataTable>

</template>

<script setup>
import { ref, watch, onMounted } from "vue";
import { useRouter } from "vue-router"
import { debounce } from "lodash";
import EasyDataTable from 'vue3-easy-data-table';
import { Input_Modal, Import_Modal, Export_Modal, Conform_Modal } from "@/components/elements";
import { getAPIUrl, childApiUrl } from '@/config.js'
import $ from "jquery";
import { EventBus } from '@/common'

import 'vue3-easy-data-table/dist/style.css';

const apiUrl = getAPIUrl() + childApiUrl.location.url;

const columns = [
    { text: 'Mã', value: 'code', required: true, sortable: true },
    { text: 'Tên Khu Vực', value: 'name', required: true, sortable: true },
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
    sortBy: "locationId",
    sortType: "desc",
});
const itemsSelected = ref([]);

const addModal = ref(null);
const editModal = ref(null);
const importModal = ref(null);
const exportModal = ref(null);
const conformModal = ref(null);

const cleanItem = (item) => {
    const { checkbox, key, index, ...cleanedItem } = item;
    return cleanedItem;
};

const showAddModal = () => {
    addModal.value.modalToggle();
};

const showEditModal = (item) => {
    editModal.value.modalToggle(item);
};

const showExportModal = () => {
    exportModal.value.modalToggle(items.value);
};

const showImportModal = () => {
    importModal.value.modalToggle();
};

const showConformModal = (do_function) => {
    conformModal.value.modalToggle(do_function);
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
            sortBy: serverOptions.value.sortBy,
            sortType: serverOptions.value.sortType,
        })
        //console.log(data);
        items.value = data;
        serverItemsLength.value = recordsTotal;
    }
    catch (error) {
        console.log(error);
        addNotification("Load failed", "error");
    }
    loading.value = false;
};

const addItem = (item, done, err) => {
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
        done();
        getItems();
        addNotification("Add success", "success");
    }).fail(function (jqXHR, textStatus, errorThrown) {
        err();
        addNotification("Add failed", "error");
    });

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
        addNotification("Edit success", "success");
    }).fail(function (jqXHR, textStatus, errorThrown) {
        addNotification("Edit failed", "error");
    });

};

const deleteItem = (item) => {
    console.log("Delete", item);
    showConformModal(() => {
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
            addNotification("Delete success", "success");
        }).fail(function (jqXHR, textStatus, errorThrown) {
            addNotification("Delete failed", "error");
        });
    });
};

const deleteItems = () => {
    console.log("Delete", itemsSelected.value);
    showConformModal(() => {
        console.log("Delete", itemsSelected.value);
        var settings = {
            "url": `${apiUrl}${childApiUrl.location.deleteMultiple}`,
            "method": "DELETE",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(itemsSelected.value),
        };
        $.ajax(settings).done(function (data, textStatus, jqXHR) {
            getItems();
            addNotification("Delete success", "success");
        }).fail(function (jqXHR, textStatus, errorThrown) {
            addNotification("Delete failed", "error");
        });
    });
};

const uploadItems = (items, done, err) => {
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
        addNotification("Upload success", "success");
        done();
    }).fail(function (jqXHR, textStatus, errorThrown) {
        err();
        addNotification("Upload failed", "error");
    });
};

const addNotification = (message, toast_type) => {
    EventBus.emit('addNotification', { message, toast_type });
}

const fileError = () => {
    addNotification("Invalid file format", "error");
};

const debouncedLoadFromServer = debounce(getItems, 500);


onMounted(() => {
    getItems();
});

watch(serverOptions, () => { getItems(); }, { deep: true });
watch(searchValue, () => { debouncedLoadFromServer(); }, { deep: true });

</script>