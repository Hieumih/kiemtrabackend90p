import { defineAsyncComponent } from 'vue'
import { createMemoryHistory, createWebHistory, createRouter } from "vue-router";
import { Customer, Dashboard, Location, Location_Detail, Schedule } from '@/components/pages/index.js';
import { check_login } from '~common/ajaxServer.js';

export const Router_Home = () => import('./Router_Home.vue');
export const Router_Authorization = () => import('./Router_Authorization.vue');

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
                meta: {
                    enterActiveClass: 'animate__animated animate__fadeInDown',
                    leaveActiveClass: 'animate__animated animate__fadeOutDown',
                }
            },
            // {
            //     path: '/customer',
            //     component: Customer,
            // },
            {
                path: '/location',
                component: Location,
                meta: {
                    enterActiveClass: 'animate__animated animate__fadeInUp',
                    leaveActiveClass: 'animate__animated animate__fadeOutUp',
                }
            },
            {
                path: '/customer',
                component: Customer
            },
            // {
            //     path: '/location/:id',
            //     component: Location_Detail,
            // },
            {
                path: '/schedule',
                component: Schedule,
            },
            {
                path: '/:pathMatch(.*)*',
                redirect: '/dashboard',
            }
        ],
        meta: {
            requiresAuth: true,
        }
    },
    {
        path: '/auth',
        component: Router_Authorization,
        meta: {
            requiresAuth: false,
        }
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
})

router.beforeEach( async (to, from, next) => {
    const byPassLogin = localStorage.getItem('byPassLogin') === 'true' && import.meta.env.DEV;
    if (byPassLogin) { 
        return next();
    }

    const isLogin = await check_login();
    console.log('login', isLogin);
    if (to.meta.requiresAuth) {
        if (isLogin) {
            next();
        }
        else {
            next('/auth');
        }
    }
    else {
        
        if (isLogin) {
            next('/dashboard');
        }
        else {
            next();
        }
    }
});

export default router;