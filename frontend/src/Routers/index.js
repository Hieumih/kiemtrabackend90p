import { createMemoryHistory, createWebHistory, createRouter } from "vue-router";
import {Customer, Dashboard, Location } from '../components/pages/index.js'

const routes = [
    { path: '/', component:  Dashboard },
    { path: '/dashboard', component: Dashboard },
    { path: '/customer', component: Customer },
    { path: '/location', component: Location },
]

const router = createRouter({
    history: createWebHistory(),
    routes : routes,
})

export default router;