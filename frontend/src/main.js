import './assets/main.css';
import "jquery";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.min.js';
import 'bootstrap';
import '@fortawesome/fontawesome-free/css/all.css';
import 'animate.css';


import { createApp } from 'vue'
import App from './App.vue'
import router from './Routers'
import { getLangClient} from '@/common/common'





(async () => {
    console.log('region language: ', navigator.language);
    localStorage.getItem('lang') == null ? localStorage.setItem('lang', navigator.language) : null;
    await getLangClient(localStorage.getItem('lang'));
})()

const app = createApp(App)
app.use(router)
app.mount('#app')

