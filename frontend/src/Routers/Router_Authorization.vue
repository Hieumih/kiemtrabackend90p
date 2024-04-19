<template>
    <div class="main">
        <div class="panel">
            <Time_Date_Panel />
        </div>
        <div class="panel">
            <h2>CRM APP</h2><br>
            <Login_Panel @login="login" />
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, defineEmits } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { login as import_login, check_login } from '~common/ajaxServer.js';
import { Time_Date_Panel, Login_Panel } from '@/components/elements/index.js'


const route = useRoute();
const router = useRouter();

const login = (data) => {
    import_login(data).then(result => {
        if (result) {
            // go back history if the page defined in router
            const redirectedFrom = route?.redirectedFrom?.fullPath ?? false;
            console.log('redirect:',redirectedFrom)
            if (redirectedFrom) {
                router.push(redirectedFrom);
            } else {
                console.log('test')
                router.push('/');
            }


        }
    })
}

</script>

<style scoped>
.main {
    background: #e4e9f7;
    width: 100%;
    height: 100vh;
    overflow: hidden;
    display: flex;
    align-items: center;
    justify-content: center;
}

.main .panel {
    background: #fff;
    width: 40%;
    min-height: 500px;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 3px;
    overflow: hidden;
    flex-direction: column;
}
</style>
