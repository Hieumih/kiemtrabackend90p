<template>
    <div class="main">
        <div class="panel">
            <Time_Date_Panel class="time-panel" />
        </div>
        <div class="panel">
            <div class="logo-container">
                <img :src="`./img/loginLogo.svg`" alt="" class="logo">

            </div>
            <Login_Panel @login="login" class="auth_panel" />
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
            console.log('redirect:', redirectedFrom)
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

<style lang="scss" scoped>
$currentColor : white ;


.main {
    /* background: #e4e9f7; */
    background-image: url('./img/login_background.jpg');
    width: 100%;
    height: 100vh;
    overflow: hidden;
    display: flex;
    align-items: center;
    justify-content: center;

    .panel {
        width: 40%;
        min-height: 500px;
        display: flex;
        align-items: center;
        justify-content: center;
        margin: 3px;
        overflow: hidden;
        flex-direction: column;
        background-color: rgba(7, 5, 5, 0.192);

        .logo-container {
            height: 150px;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 10px;
            overflow: hidden;

            .logo {
                width: 250px;
                height: 250px;
                object-fit: cover;
                /* white */
                /* filter: invert(100%); */
            }
        }
    }
}


.time-panel:deep() {
    color: $currentColor ;
    border-color: $currentColor ;

    .date-time .date.month {
        border-left: 1px solid $currentColor  ;
        border-right: 1px solid $currentColor  ;
    }

    .time-zone .select-zone {
        border: 1px solid $currentColor  ;
        color: $currentColor ;
    }
}

.auth_panel:deep() {
    color: $currentColor ;

    .form-input {
        border-color: $currentColor ;

        .input {
            color: $currentColor ;
            caret-color: rgb(250, 250, 250);
        }

        .input::placeholder {
            color: rgba(255, 255, 255, 0.445);
        }
    }

    .forgotPass small a {
        color: $currentColor ;
    }
}
</style>
