import { defineAsyncComponent } from 'vue'
import { createMemoryHistory, createWebHistory, createRouter } from "vue-router";
import { Dashboard, HangHoa, NhomHangHoa } from '@/components/pages/index.js';
import { check_login } from '~common/ajaxServer.js';

export const Router_Home = () => import('./Router_Home.vue');

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
            {
                path: '/nhom-hang-hoa',
                component: NhomHangHoa,
            },
            {
                path: '/hang-hoa',
                component: HangHoa,
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
]

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
})

// router.beforeEach( async (to, from, next) => {
//     const byPassLogin = localStorage.getItem('byPassLogin') === 'true' && import.meta.env.DEV;
//     if (byPassLogin) { 
//         return next();
//     }

//     const isLogin = await check_login();
//     console.log('login', isLogin);
//     if (to.meta.requiresAuth) {
//         if (isLogin) {
//             next();
//         }
//         else {
//             next('/auth');
//         }
//     }
//     else {
        
//         if (isLogin) {
//             next('/dashboard');
//         }
//         else {
//             next();
//         }
//     }
// });

export default router;