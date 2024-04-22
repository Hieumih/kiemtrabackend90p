<template>
    <div class="manager">
        <div class="toolbar">
            <div class="buttons">
                <Modal_Form ref="modal_add_ref" :columns="columns" @save="addItem">
                    <template #header>
                        add
                    </template>
                    <template #button-open="{ click }">
                        <button @click="click" class="btn-modal-open">
                            <i class="bi bi-plus-circle"></i>
                            <span>Add</span>
                        </button>
                    </template>

                </Modal_Form>
                <Modal_Form ref="modal_edit_ref" :columns="columns" @save="editItem">
                    <template #header>
                        edit
                    </template>
                    <template #button-open="{ click }">
                        <button @click="click" style="display: none;"></button>
                    </template>
                </Modal_Form>
                <Modal_Import ref="modal_import_ref" :columns="columns" @save="importData">
                    <template #header>
                        import
                    </template>
                    <template #button-open="{ click }">
                        <button @click="click" class="btn-modal-open">
                            <i class="bi bi-file-earmark-arrow-up"></i>
                            <span>Import</span>
                        </button>
                    </template>
                </Modal_Import>
                <Modal_Export ref="modal_export_ref" :items="items">
                    <template #header>
                        export
                    </template>
                    <template #button-open="{ click }">
                        <button @click="click" class="btn-modal-open">
                            <i class="bi bi-file-earmark-arrow-down"></i>
                            <span>Export</span>
                        </button>
                    </template>
                </Modal_Export>
                <Modal ref="modal_delete_ref">
                    <template #header>
                        delete
                    </template>
                    <template #body>
                        <p>Are you sure you want to delete selected item(s)?</p>
                    </template>
                    <template #footer="{ close }">
                        <button type="button" class="btn btn-secondary" @click="close">
                            Close
                        </button>
                        <button type="button" class="btn btn-danger" ref="delete_btn_ref">
                            Delete
                        </button>
                    </template>
                    <template #button-open="{ click }">
                        <button @click="click" style="display: none;">delete</button>
                    </template>
                </Modal>
                <div>
                    <button class="btn-modal-open" @click="getItems">
                        <i class="bi bi-arrow-clockwise"></i>
                        <span>Refresh</span>
                    </button>
                </div>
                <div>
                    <button class="btn-modal-open" @click="showDeleteModal(deleteItems)"
                        :disabled="!itemsSelected.length">
                        <i class="bi bi-recycle"></i>
                        <span>Delete Selected</span>

                    </button>
                </div>
            </div>
            <div class="searchField">
                <input type="text" v-model="searchValue" placeholder="Search...">
            </div>
        </div>
        <EasyDataTable :headers="headers" :items="items" :rows-items="[5, 10, 15, 20, 50, 100]" :rows-per-page="10"
            :loading="loading" :server-items-length="serverItemsLength" v-model:server-options="serverOptions"
            v-model:items-selected="itemsSelected"  buttons-pagination show-index class="custom-table"
        >
            <template #item-name="{ code, name }">
                <RouterLink :to="`${path}/${code}`">{{ name }}</RouterLink>
            </template>
            <template #item-operation="item">
                <button class="btn-modal-open" @click="showEditModal(item)">
                    <i class="bi bi-pencil"></i>
                    <span>Edit</span>
                </button>
                <button class="btn-modal-open" @click="showDeleteModal(() => { deleteItem(item) })">
                    <i class="bi bi-trash"></i>
                    <span>Delete</span>
                </button>
            </template>
        </EasyDataTable>
    </div>
</template>


<script setup>
import 'vue3-easy-data-table/dist/style.css';
import { ref, defineProps, defineEmits, onMounted, watch } from 'vue';
import { useRouter } from "vue-router"
import { Modal_Form, Modal_Import, Modal_Export, Modal } from '@/components/elements/index.js'
import EasyDataTable from 'vue3-easy-data-table';
import { debounce } from "lodash";

//#region declare props and emits
const props = defineProps({
    columns: {
        type: Array,
        required: false,
        default: () => [
            { text: 'id', value: 'id' },
        ]
    },
    disabled: {
        type: Boolean,
        required: false,
        default: false
    }
})
const emits = defineEmits(['getItems', 'addItem', 'editItem', 'deleteItem', 'deleteItems', 'importData']);
//#endregion declare props and emits

//#region ref item

const items = ref([]);
const itemsSelected = ref([]);
const loading = ref(false);
const serverItemsLength = ref(0);
const searchValue = ref("");
const serverOptions = ref({
    page: 1,
    rowsPerPage: 10, // get primary key
    sortType: "asc",
    sortBy: "",
});
const headers = ref(props.columns.concat([{ text: 'Thao tác', value: 'operation' }]));

const router = useRouter();
const path = router.currentRoute.value.path;

//#endregion items

//#region vue component
const modal_add_ref = ref(null);
const modal_edit_ref = ref(null);
const modal_import_ref = ref(null);
const modal_delete_ref = ref(null);
const delete_btn_ref = ref(null);



//#endregion vue component

//#region function
const getItems = () => {
    emits('getItems', serverOptions, searchValue, items, serverItemsLength, loading);
}

const addItem = (item, close) => {
    import.meta.env.DEV && console.log('add', item);
    emits('addItem', item, () => {
        close();
        getItems();
    });
}

const showEditModal = (item) => {
    modal_edit_ref.value.toggle();
    modal_edit_ref.value.data = item;
}

const editItem = (item, close) => {
    import.meta.env.DEV && console.log('edit', item);
    emits('editItem', item, () => {
        close();
        getItems();
    });
}

const showDeleteModal = (deleteItem) => {
    modal_delete_ref.value.toggle();
    delete_btn_ref.value.onclick = deleteItem;
}

const deleteItem = (item) => {
    import.meta.env.DEV && console.log('delete', item);
    emits('deleteItem', item, () => {
        getItems();
        modal_delete_ref.value.toggle();
        itemsSelected.value = [];
    });
}

const deleteItems = () => {
    import.meta.env.DEV && console.log('delete', itemsSelected.value);
    emits('deleteItems', itemsSelected.value, () => {
        getItems();
        modal_delete_ref.value.toggle();
        itemsSelected.value = [];
    });
}

const importData = (data, close) => {
    import.meta.env.DEV && console.log('import', data);
    emits('importData', data, () => {
        close();
        getItems();
    });
}

const debouncedLoadFromServer = debounce(getItems, 500);
//#endregion function

//#region last mounted


onMounted(() => {
    getItems();
    // serverOptions.value.sortBy = Object.keys(props.columns[0])[0];
})

watch(serverOptions, () => { getItems(); }, { deep: true });
watch(searchValue, () => { debouncedLoadFromServer(); }, { deep: true });

//#endregion
</script>

<style scoped lang="scss">
.toolbar {
    display: flex;
    justify-content: space-between;
    margin-bottom: 10px;

    .buttons {
        display: flex;
    }

    .searchField {
        input {
            width: 200px;
            padding: 5px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }
    }
}

.btn-modal-open {
    margin-right: 5px;
    padding: 5px 10px;
    border-radius: 5px;
    border: 1px solid #ccc;
    background-color: #f8f9fa;
    cursor: pointer;

    span {
        margin-left: 5px;
        font-weight: 500;
        font-size: 1rem;
    }

    i {
        font-size: 1.2rem;
    }
}

.btn-modal-open:hover {
    background-color: #e9ecef;
}

// .custom-table:deep() div table {
//     thead, tbody {
//         tr {
//             th, td {
//                 div.easy-checkbox {
//                     label:after {

//                     }
//                 }
//             }
//         }
//     }
// }
</style>