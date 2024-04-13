import { createMemoryHistory, createWebHistory, createRouter } from "vue-router";
import { Customer, Dashboard, Location, Location_Detail } from '../components/pages/index.js';
import Router_Home from './Router_Home.vue';

const routes = [
    {
        path: '/',
        component: Router_Home,
        children: [
            {
                path: '',
                redirect: '/dashboard',
            },
            {
                path: '/dashboard',
                component: Dashboard,
            },
            {
                path: '/customer',
                component: Customer,
            },
            {
                path: '/location',
                component: Location,
            },
            {
                path: '/location/:id',
                component: Location_Detail,
            },
        ]
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
})

export default router;