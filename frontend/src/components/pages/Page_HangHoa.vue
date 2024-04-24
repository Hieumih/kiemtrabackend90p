<template>
    <div class="main">
        <Table_Manager @get-items="getItems" @edit-item="editItem" @add-item="addItem" @delete-item="deleteItem"
            @delete-items="deleteItems" @import-data="importData" :columns="columns"></Table_Manager>
    </div>
</template>


<script setup>
import { ref, defineProps, defineEmits, onMounted } from 'vue';
import { Table_Manager } from '@/components/elements/index.js'
import {
    getHangHoas as gets,
    editHangHoa as edit,
    addHangHoa as add,
    deleteHangHoa as remove,
    deleteHangHoas as removes,
    importHangHoas as import_data
} from '@/common/ajaxServer';
import { addNotification } from '@/common/common';

const columns = ref(
    [
        // { text: 'id', value: 'hhId   ', required: true, sortable: true },
        { text: 'Mã phân loại', value: 'hhNhhId', required: true, sortable: true },
        { text: 'Mã', value: 'hhMa' },
        { text: 'Tên', value: 'hhTen' },
        { text: 'Giá nhập', value: 'hhGianhap', type: 'number'},
        { text: 'Giá bán', value: 'hhGiaban', type: 'number'},
        

    ]
)


const getItems = async (option, search, items, length, loading) => {
    loading.value = true;
    await gets({
        take: option.value.rowsPerPage,
        skip: (option.value.page - 1) * option.value.rowsPerPage,
        search: search.value,
        sortBy: option.value.sortBy,
        sortType: option.value.sortType,
    }).then((res) => {
        console.log(res)
        items.value = res.data;
        length.value = res.recordsTotal;
    }).catch((err) => {
        console.log(err);
        addNotification("Load failed", "error");
    }).finally(() => {
        loading.value = false;
    });
};

const addItem = (item, done) => {
    add(item).then(() => {
        done();
    }).catch((err) => {
        console.log(err);
        addNotification("Add failed", "error");
    });
};

const editItem = (item, done) => {
    edit(item).then(() => {
        done();
    }).catch((err) => {
        console.log(err);
        addNotification("Edit failed", "error");
    });
};

const deleteItem = (item, done) => {
    remove(item).then(() => {
        done();
    }).catch((err) => {
        console.log(err);
        addNotification("Delete failed", "error");
    });
}

const deleteItems = (items, done) => {
    removes(items).then(() => {
        done();
    }).catch((err) => {
        console.log(err);
        addNotification("Delete failed", "error");
    });
}

const importData = (data, done) => {
    import_data(data).then(() => {
        done();
    }).catch((err) => {
        console.log(err);
        addNotification("Import failed", "error");
    });
}



</script>

<style scoped lang="scss">
.main {
    padding: 20px;
}
</style>