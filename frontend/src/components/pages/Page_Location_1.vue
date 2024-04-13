<template>
    <p>Manager Location</p>
    <!-- <Table_AddEditRemove :columns="columns" :data="data" @add="addItem" @refresh="getData" @edit="editItem"
        @remove="deleteItem" /> -->
    <CustomTable :columns="columns" :data="data" ref="table" v-if="data.length">
        <template #additionHeader>

        </template>
</CustomTable>



</template>

<script>
import { Table_AddEditRemove, Table as CustomTable } from '../elements/index'
import $ from 'jquery'





export default {
    name: 'Page_Location',
    components: {
        Table_AddEditRemove,
        CustomTable,

    },
    methods: {
        getData() {
            $.get('http://localhost:5139/api/Locations')
                .then(res => {
                    this.data = res
                })
                .catch(err => {
                    console.log(err)
                })
        },
        async addItem(item, funct) {
            var settings = {
                "url": "http://localhost:5139/api/Locations/add",
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
                funct(jqXHR.status === 200 ? true : false)
            }).fail(function (jqXHR, textStatus, errorThrown) {
                funct(false)
            });
            // funct(res.status === 200 ? true : false)
        },
        editItem(item, funct) {
            var settings = {
                "url": "http://localhost:5139/api/Locations/update",
                "method": "PUT",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "locationId": item.locationId,
                    "code": item.code,
                    "name": item.name,
                    "description": item.description
                }),
            };

            $.ajax(settings).done(function (data, textStatus, jqXHR) {
                funct(jqXHR.status === 200 ? true : false)
            }).fail(function (jqXHR, textStatus, errorThrown) {
                funct(false)
            });
        },
        async deleteItem(item, funct) {
            var settings = {
                "url": "http://localhost:5139/api/Locations/delete/?id=" + item.locationId,
                "method": "DELETE",
                "timeout": 0,
            };

            return await $.ajax(settings).done(function (response) {
                console.log(response);
                funct();
            });
        }
    },
    data() {
        return {
            columns: [
                { title: 'Mã', data: 'code' },
                { title: 'Tên Khu Vực', data: 'name' },
                { title: 'Mô tả', data: 'description' },
            ],
            data: []
        }
    },
    mounted() {
        //this.getData()


    },
}
</script>