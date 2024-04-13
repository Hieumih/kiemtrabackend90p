import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './Routers'
import "jquery";
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'
import 'bootstrap'

// const getLang = async (lang) => {
//     await fetch('lang/' + lang + '.json')
//     .then(response => response.json())
//     .then(data => { 
//         localStorage.setItem('langData', JSON.stringify(data)),
//         console.log('data: ', data)
//     })
//     .catch(async error => {
//         console.error(error),
//         localStorage.setItem('lang', 'en-US'),
//         await getLang('en-US')
//     })
// }


// // Set default language to the client language
// console.log('region language: ', navigator.language),

// localStorage.getItem('lang') == null ? localStorage.setItem('lang', navigator.language) : null
// // get the language from local storage
// const lang = localStorage.getItem('lang')

// await getLang(lang).then(
//     console.log('client language: ', lang)
// )


const app = createApp(App)
app.use(router) 
app.mount('#app')

