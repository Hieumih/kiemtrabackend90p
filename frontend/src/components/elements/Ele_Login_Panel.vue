<template>
    <div class="form-container">
        <form action="" @submit.prevent="login">
            <div class="form-column form-input">
                <i class="fa-solid fa-circle-user"></i>
                <input class="input" type="text" name="username" id="username" :placeholder="UsernameText" v-model="login_model.user">
            </div>
            <div class="form-column form-input">
                <i class="fa-solid fa-key"></i>
                <input class="input" type="password" name="password" id="password" :placeholder="PasswordText"
                    ref="passIptRef" v-model="login_model.pass">
                <i :class="{
                    'fa-solid fa-eye': !showPass,
                    'fa-solid fa-eye-slash': showPass
                }" ref="showPassRef"></i>  
            </div>
            <div class="form-column forgotPass">
                <small><a href="#"><i class="fa-solid fa-question-circle"></i> {{ forgotText }}</a></small>
            </div>
            <div class="form-column button">
                <div class="check-box">
                    <input type="checkbox" name="remember" id="remember" v-model="login_model.remember">
                    <label for="remember">{{ rememberText }}</label>
                </div>
                <button type="submit">{{ loginText }}</button>
            </div>
            <div class="form-column login-method">
                <div class="social-media">
                    <i class="fa-brands fa-google"></i>
                    <span>Google</span>
                </div>
                <!-- microsoft -->
                <div class="social-media">
                    <i class="fa-brands fa-microsoft"></i>
                    <span>Microsoft</span>
                </div>
            </div>
        </form>
    </div>
</template>

<script setup>
import { ref, onMounted, defineProps, defineExpose, defineEmits } from 'vue';

const emits = defineEmits(['login']);

const showPassRef = ref(null);
const passIptRef = ref(null);
const showPass = ref(false);
const login_model = ref({});

const props = defineProps({
    UsernameText: {
        type: String,
        default: 'Username'
    },
    PasswordText: {
        type: String,
        default: 'Password'
    },
    forgotText: {
        type: String,
        default: 'Forgot Password?'
    },
    rememberText: {
        type: String,
        default: 'Remember Me'
    },
    loginText: {
        type: String,
        default: 'Login'
    }
});

const login = () => {
    emits('login', login_model.value);
};

onMounted(() => {
    showPassRef.value.addEventListener('mousedown', () => {
        passIptRef.value.type = 'text';
        showPass.value = true;
    });

    showPassRef.value.addEventListener('mouseup', () => {
        passIptRef.value.type = 'password';
        showPass.value = false;
    });
});



</script>


<style scoped>
.form-container {
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.form-container form {
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.form-container .form-column {
    width: 80%;
    display: flex;
}

.form-container .form-input {
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 10px;
    border: 1px solid;
    border-radius: 25px;
    padding: 5px;
    overflow: hidden;
}

.form-container .form-input:focus-within {
    border: 1px solid #3f51b5;
}

.form-container .form-input i {
    font-size: 20px;
    margin: 0 10px;
}

.form-container .form-input .input {
    width: 100%;
    padding: 5px;
    border: 1px solid;
    border: none;
    outline: none;
    background: transparent;
}

.form-container .form-input .input:focus {
    outline: none;
}

/* small */
.form-container .forgotPass {
    /* margin: 10px; */
    align-items: center;
    justify-content: end;
}

.form-container .forgotPass small {
    font-size: 12px;
}

.form-container .forgotPass small a {
    text-decoration: none;
}

.form-container .button {
    align-items: center;
    justify-content: space-between;
    margin: 10px;
}

.form-container .button button {
    padding: 10px 10px;
    border: 1px solid;
    border-radius: 25px;
    background: #3f51b5;
    color: #fff;
    cursor: pointer;
    outline: none;
    min-width: 100px;
}

.form-container .button .check-box {
    display: flex;
    align-items: center;
    justify-content: center;
}

.form-container .button .check-box input[type="checkbox"] {
    width: 20px;
    height: 20px;
    margin-right: 10px;
}

/* .form-container .button .check-box label {
    font-size: 12px;
} */

.form-container .login-method {
    margin: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.form-container .login-method .social-media {
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 10px;
    padding: 5px;
    border: 1px solid;
    border-radius: 25px;
}

.form-container .login-method .social-media i {
    font-size: 20px;
    margin: 0 10px;
}

.form-container .login-method .social-media span {
    /* font-size: 12px; */
    margin-right: 10px;
}

/* transparent backgroud in auto filter like save password */
.form-container .form-input input:-webkit-autofill,
.form-container .form-input input:-webkit-autofill:hover,
.form-container .form-input input:-webkit-autofill:focus,
.form-container .form-input input:-webkit-autofill:active {
    transition: background-color 5000s ease-in-out 0s;
    -webkit-text-fill-color: #fff !important;
}
</style>