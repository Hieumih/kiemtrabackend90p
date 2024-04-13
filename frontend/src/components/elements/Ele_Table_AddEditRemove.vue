<template>
    <div>
        <form action="">
            
        </form>
    </div>

    <div>
        <button @click="showAddModal()" class="rounded-pill btn me-1 custom">
            <i class="bi bi-plus-lg"></i>
            Add
        </button>
        <button @click="refreshData()" ref="btnRefresh" class="rounded-pill btn me-1 custom">
            <i class="bi bi-arrow-clockwise"></i>
            Refresh
        </button>
        <button @click="showImportModel()" ref="btnImport" class="rounded-pill btn me-1 custom">
            <i class="bi bi-arrow-bar-up"></i>
            Import
        </button>
        <button @click="showExportModel()" ref="btnImport" class="rounded-pill btn me-1 custom">
            <i class="bi bi-arrow-bar-down"></i>
            Export
        </button>
    </div>

    <!-- add modal -->
    <div class="modal fade" id="modal" ref="AddModal" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="modalLabel">Thêm</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <form action="" @submit.prevent="addItem">
                    <div class="modal-body">
                        <template v-for="(column, index) in columns" :key="index">
                            <div class="mb-3">
                                <template v-if="!(column?.hidden ?? false)">
                                    <!-- <label :for="column.key" class="form-label">
                                        {{ column.title }}
                                    </label> -->
                                    <input :type="column?.type ?? 'text'" :placeholder="column.title"
                                        class="form-control" :required="column?.required ?? false" :id="column.key"
                                        v-model="addFormData[column.key]">
                                </template>

                            </div>
                        </template>
                        <!-- <input class="input-text mb-3" v-for="(column, index) in columns" :key="index" type="text" :placeholder="column.title"> -->
                        <p id="addNotice"></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @click="clearForm()"
                            data-bs-dismiss="modal">Close</button>
                        <button type="sumbit" class="btn btn-primary">Save changes</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- edit modal -->
    <div class="modal fade" id="modal" ref="EditModal" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="modalLabel">Sửa</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <form action="" @submit.prevent="editItem">
                    <div class="modal-body">
                        <template v-for="(column, index) in columns" :key="index">
                            <div class="mb-3">
                                <!-- <label :for="column.key" class="form-label">
                                        {{ column.title }}
                                    </label> -->
                                <input :type="column?.type ?? 'text'" v-if="!column.hidden" :placeholder="column.title"
                                    class="form-control" :required="column?.required ?? false" :id="column.key"
                                    v-model="editFormData[column.key]">
                            </div>
                        </template>
                        <!-- <input class="input-text mb-3" v-for="(column, index) in columns" :key="index" type="text" :placeholder="column.title"> -->
                        <p id="editNotice"></p>
                    </div>
                    <div class="modal-footer">
                        <!--  -->

                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"
                            @click="clearForm()">Cancel</button>
                        <button type="sumbit" class="btn btn-primary">Save changes</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="table-responsive">
        <table class="table table-hover mb-3 ">
            <thead>
                <tr>
                    <th v-for="(column, index) in columns" :key="index">{{ column.title }}</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in data" :key="index">
                    <template v-for="(column, colIndex) in columns" :key="colIndex">
                        <td>
                            {{ item[column.key] }}
                        </td>
                    </template>
                    <!-- <td v-for="(column, colIndex) in columns" :key="colIndex">
                        {{ item[column.key] }}
                    </td> -->
                    <td>

                        <div>
                            <button @click="showEditModel(item)" class="rounded-pill btn me-1 custom">
                                <i class="bi bi-pencil"></i>
                                Edit
                            </button>
                            <button @click="removeItem(item)" class="rounded-pill btn me-1 custom">
                                <i class="bi bi-x-lg"></i>
                                Remove
                            </button>
                        </div>

                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script>
import bootstrap from 'bootstrap/dist/js/bootstrap'
import 'bootstrap'

export default {
    emits: ['edit', 'remove', 'add', 'refresh', 'import'],
    props: {
        columns: {
            type: Array,
            required: true
        },
        data: {
            type: Array,
            required: true
        },
        // addItem: {
        //     type: Function,
        //     required: true
        // }
    },
    methods: {
        showAddModal() {
            // const model = new bootstrap.Modal(this.$refs.AddModal, {
            //     keyboard: false
            // });
            this.addModal.toggle();
            // this.$emit('add');
        },

        showEditModel(item) {
            this.EditModal.toggle();
            this.editFormData = { ...item };
        },

        showImportModel() {

        },

        showExportModel() {

        },

        async addItem() {
            // Emit an event or call a method to handle adding the item
            console.log('Add item:', this.addFormData);

            this.$emit('add', this.addFormData, (result) => {
                if (result) {

                    this.addFormData = {};
                    this.refreshData();
                    this.addModal.toggle();
                }
                else {
                    document.querySelector('#addNotice').innerHTML = 'Thêm thất bại';
                    // this.$refs.AddNotice = 'Thêm thất bại';
                }
            });


        },

        editItem() {
            // Emit an event or call a method to handle editing the item
            console.log('Edit item:', this.editFormData);
            this.$emit('edit', this.editFormData, (result) => {
                if (result) {

                    this.editFormData = {};
                    this.refreshData();
                    this.EditModal.toggle();
                }
                else {
                    document.querySelector('#editNotice').innerHTML = 'Sửa thất bại';

                }
            });
        },

        removeItem(item) {
            // Emit an event or call a method to handle removing the item
            console.log('Remove item:', item);
            this.$emit('remove', item, this.refreshData);

        },

        refreshData() {
            const btn = this.$refs.btnRefresh;
            btn.innerHTML = '<i class="bi bi-clock"></i> Refreshing...';
            btn.disabled = true;
            this.$emit('refresh');
            setTimeout(() => {
                btn.innerHTML = '<i class="bi bi-arrow-clockwise"></i> Refresh';
                btn.disabled = false;
            }, 1000);
        },

        importData() {
            this.$emit('import');
        },

        clearForm() {
            this.addFormData = {};
            this.editFormData = {};
        }
    },
    data() {
        return {
            addFormData: {},
            editFormData: {},
            addModal: null,
            EditModal: null
        }
    },
    mounted() {
        this.addModal = new bootstrap.Modal(this.$refs.AddModal);
        this.EditModal = new bootstrap.Modal(this.$refs.EditModal);
    }
};
</script>


<style>
button.custom {
    border-color: #D20062 !important;
    border-width: 2px !important;
    color: #D20062 !important;
    /* font-size: 10px !important; */
    font-weight: bold !important;
}

button.custom:hover {
    border-color: #C4E4FF !important;
    border-width: 2px !important;
    color: #C4E4FF !important;
    /* font-size: 10px !important; */
    font-weight: bold !important;
}


.table-responsive td:last-child div {
    width: 200px !important;
    /* Adjust the width as needed */
}
</style>